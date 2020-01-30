using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBaseFunctional : MonoBehaviour
{
    [SerializeField] protected Stat healthStat;

    //protected Animator objectAnimator;
    //[SerializeField] protected int health;

    public abstract bool IsDead { get; }

    public Animator ObjectAnimator { get; private set; }

    [SerializeField] protected Transform fireBallStartPosition;
    [SerializeField] protected GameObject fireBallPrefab;

    public bool ObjectAttack { get; set; }

    public bool TakingDamage { get; set; }

    [SerializeField] protected float playerSpeed;

    protected bool objectBoolFasingRight;

    [SerializeField] EdgeCollider2D swordCollider;

    public EdgeCollider2D SwordCollider
    {
        get
        {
            return swordCollider;
        }
    }

    [SerializeField] List<string> damageSource;

    // Start is called before the first frame update
    public virtual void Start()
    {
        objectBoolFasingRight = true;
        ObjectAnimator = GetComponent<Animator>();
        healthStat.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Death();

    public void ObjectChangeDirection()
    {
        objectBoolFasingRight = !objectBoolFasingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    public virtual void ObjectThrowThing(int value)
    {
        if (objectBoolFasingRight)
        {
            GameObject fireBall = Instantiate(fireBallPrefab, fireBallStartPosition.position, Quaternion.identity);
            fireBall.GetComponent<FireBall>().FireBallInitialization(Vector2.right);
        }

        else
        {
            GameObject fireBall = Instantiate(fireBallPrefab, fireBallStartPosition.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            fireBall.GetComponent<FireBall>().FireBallInitialization(Vector2.left);
        }
    }

    public abstract IEnumerator TakeDamage();

    public void AttackSword()
    {
        SwordCollider.enabled = true;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (damageSource.Contains(other.tag))
        {
            StartCoroutine(TakeDamage());
        }
    }
}
