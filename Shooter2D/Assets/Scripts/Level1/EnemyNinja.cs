using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNinja : CharacterBaseFunctional
{
    LevelUp level;
    IEnemyState currentState;

    /// <summary>
    /// Target for enemy it is player
    /// </summary>
    public GameObject TargetForEnemy { get; set; }

    [SerializeField] float meleeRange;

    [SerializeField] float castFireBallRange;

    [SerializeField] Transform spawnEnemy;

    [SerializeField] Transform leftEdge;
    [SerializeField] Transform rightEdge;

    [SerializeField] int experienceForEnemy;

    bool dropItem = true;

    Canvas healthCanvas;
    public bool InMeleeRange
    {
        get
        {
            if(TargetForEnemy != null)
            {
                return Vector2.Distance(transform.position, TargetForEnemy.transform.position) <= meleeRange;
            }

            return false;
        }
    }

    public bool InCastFireBallRange
    {
        get
        {
            if (TargetForEnemy != null)
            {
                return Vector2.Distance(transform.position, TargetForEnemy.transform.position) <= castFireBallRange;
            }

            return false;
        }
    }

    public override bool IsDead
    {
        get
        {
            return healthStat.CurrentVal <= 0;
        }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        PlayerController.Instance.DeadEvent += new DeadEventHandler(RemoveTarget);
        ChangeState(new IdleState());
        level = FindObjectOfType<LevelUp>();

        healthCanvas = transform.GetComponentInChildren<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsDead)
        {
            if (!TakingDamage)
            {
                currentState.Execute();
            }

            LookAtTarget();
        }
    }

    /// <summary>
    /// Enemy forget player when he die
    /// </summary>
    public void RemoveTarget()
    {
        Debug.Log("RemoveTarget");
        TargetForEnemy = null;
        ChangeState(new PatrolState());
    }

    /// <summary>
    /// 
    /// </summary>
    void LookAtTarget()
    {
        if (TargetForEnemy != null)
        {
            float xDir = TargetForEnemy.transform.position.x - transform.position.x;

            if (xDir < 0 && objectBoolFasingRight || xDir > 0 && !objectBoolFasingRight)
            {
                ObjectChangeDirection();
            }
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }


    /// <summary>
    /// Move enemy
    /// </summary>
    public void Move()
    {
        if (!ObjectAttack)
        {
            if((GetDirection().x > 0 && transform.position.x < rightEdge.position.x) || (GetDirection().x < 0 && transform.position.x > leftEdge.position.x))
            {
                ObjectAnimator.SetFloat("isWalk", 1);

                //Move enemy 
                transform.Translate(GetDirection() * (playerSpeed * Time.deltaTime));
            }

            else if(currentState is PatrolState)
            {
                ObjectChangeDirection();
            }

            else if(currentState is RangeState)
            {
                TargetForEnemy = null;
                ChangeState(new IdleState());
            }
        }
    }

    /// <summary>
    /// Flip enemy
    /// </summary>
    /// <returns></returns>
    public Vector3 GetDirection()
    {
        return objectBoolFasingRight ? Vector2.right : Vector2.left;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        currentState.OnTriggerEnter2D(collision);
    }

    public override IEnumerator TakeDamage()
    {
        if (!healthCanvas.isActiveAndEnabled)
        {
            healthCanvas.enabled = true;
        }

        healthStat.CurrentVal -= 10;
        if(!IsDead)
        {
            ObjectAnimator.SetTrigger("animatorTakeDamage");
        }

        else
        {
            if (dropItem)
            {
                GameObject gems = Instantiate(GameManager.InstanceGameManager.GemsPrefab, new Vector3(transform.position.x, transform.position.y + 2), Quaternion.identity);
                Physics2D.IgnoreCollision(gems.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
                dropItem = false;
            }
            ObjectAnimator.SetTrigger("animatorDie");
            yield return null;
        }
    }

    public override void Death()
    {
        dropItem = true;
        ObjectAnimator.ResetTrigger("animatorDie");
        ObjectAnimator.SetTrigger("animatorIdle");
        healthStat.CurrentVal = healthStat.MaxVal;
        level.PlayerLevelUp(experienceForEnemy);
        transform.position = spawnEnemy.position;
        healthCanvas.enabled = false;
    }
}
