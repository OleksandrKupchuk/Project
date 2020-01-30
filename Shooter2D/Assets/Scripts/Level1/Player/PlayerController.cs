using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DeadEventHandler();

public class PlayerController : CharacterBaseFunctional
{
    LevelUp levelP;
    Trip trip;
    static PlayerController instance;

    public event DeadEventHandler DeadEvent;

    IUsable usable;

    public static PlayerController Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<PlayerController>();
            }
            return instance;
        }
    }

    bool immortal = false;
    [SerializeField] float immortalTime;

    [SerializeField] float climbSpeed;

    [SerializeField] SpriteRenderer spriteRenderer;

    public Rigidbody2D PlayerRigidbody2D { get; set; }

    public bool OnLadder { get; set; }

    public bool IsFalling
    {
        get
        {
            return PlayerRigidbody2D.velocity.y < 0;
        }
    }

    public bool PlayerSlide { get; set; }

    public bool PlayerJump { get; set; }

    public bool PlayerOnGround { get; set; }

    [SerializeField] Transform startPosition;

    public override bool IsDead
    {
        get
        {
            if (healthStat.CurrentVal <= 0)
            {
                OnDead();
            }

            return healthStat.CurrentVal <= 0;
        }
    }

    public float jumpForce;

    [SerializeField] bool boolPlayerAirControl;
    
    public Transform[] groundPointsOnPlayer;
    public float groundRadiusOnGrundPointOnPlayer;
    public LayerMask whatIsGroundLayerMask;

    InteractionPlayerWithObjects interactionPlayerWithObjects;

    // Start is called before the first frame update
    public override void Start()
    {
        trip = FindObjectOfType<Trip>();
        OnLadder = false;
        base.Start();
        PlayerRigidbody2D = GetComponent<Rigidbody2D>();
        interactionPlayerWithObjects = GameObject.Find("Canvas").GetComponent<InteractionPlayerWithObjects>();
        levelP = FindObjectOfType<LevelUp>();
    }

    private void Update()
    {
        if (!TakingDamage && !IsDead)
        {
            if(transform.position.y <= -14f)
            {
                Death();
            }
            PlayerHandleInput();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!TakingDamage && !IsDead)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            PlayerOnGround = IsGrounded();

            PlayerMovementRightOrLeft(horizontal, vertical);

            FlipPlayer(horizontal);

            PlayerHandleLayersAnimator();
        }
    }

    void PlayerHandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !OnLadder && !IsFalling)
        {
            ObjectAnimator.SetTrigger("animatorJump");
            //PlayerJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ObjectAnimator.SetTrigger("animatorAttack");
            PlayerRigidbody2D.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ObjectAnimator.SetTrigger("animatorSlide");
        }

        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            ObjectAnimator.SetTrigger("animatorCastFireBall");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Use();
        }
    }

    // flip player righ or left
    void FlipPlayer(float inFlipPlayerMovementLeftOrRight)
    {
        if (inFlipPlayerMovementLeftOrRight > 0 && !objectBoolFasingRight || inFlipPlayerMovementLeftOrRight < 0 && objectBoolFasingRight)
        {
            ObjectChangeDirection();
        }
    }

    public override void OnTriggerEnter2D(Collider2D otherPlayer)
    {
        if (otherPlayer.gameObject.CompareTag("Usable"))
        {
            usable = otherPlayer.GetComponent<IUsable>();
        }

        //if(otherPlayer.gameObject.CompareTag("Trip") && trip.tripIsDown != true)
        //{
        //    StartCoroutine(TakeDamage());
        //}

        base.OnTriggerEnter2D(otherPlayer);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Usable"))
        {
            usable = null;
        }
    }

    private void PlayerDead()
    {
        Destroy(gameObject);
    }

    public void OnDead()
    {
        if(DeadEvent != null)
        {
            DeadEvent();
        }
    }

    void PlayerMovementRightOrLeft(float horizontal, float vertical)
    {
        if(IsFalling)
        {
            gameObject.layer = 13;
            ObjectAnimator.SetBool("animatorAirDown", true);
        }

        if(!ObjectAttack && !PlayerSlide && (PlayerOnGround || boolPlayerAirControl))
        {
            PlayerRigidbody2D.velocity = new Vector2(horizontal * playerSpeed, PlayerRigidbody2D.velocity.y);
        }

        if(PlayerJump && PlayerRigidbody2D.velocity.y == 0 && !OnLadder)
        {
            PlayerRigidbody2D.AddForce(new Vector2(0, jumpForce));
        }

        if (OnLadder)
        {
            ObjectAnimator.speed = vertical !=0 ? Mathf.Abs(vertical) : Mathf.Abs(horizontal);
            PlayerRigidbody2D.velocity = new Vector2(horizontal * climbSpeed, vertical * climbSpeed);
        }

        ObjectAnimator.SetFloat("isWalk", Mathf.Abs(horizontal));
    }

    bool IsGrounded()
    {
        if(PlayerRigidbody2D.velocity.y <= 0)
        {
            foreach(Transform bottomPointOnPlayer in groundPointsOnPlayer)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(bottomPointOnPlayer.position, groundRadiusOnGrundPointOnPlayer, whatIsGroundLayerMask);

                for(int i = 0; i < colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    void PlayerHandleLayersAnimator()
    {
        if (!PlayerOnGround)
        {
            ObjectAnimator.ResetTrigger("animatorSlide");
            ObjectAnimator.SetLayerWeight(1, 1f);
            //ObjectAnimator.SetLayerWeight(0, 0f);
        }

        else
        {
            ObjectAnimator.SetLayerWeight(1, 0f);
            //ObjectAnimator.SetLayerWeight(0, 1f);
        }
    }

    public override void ObjectThrowThing(int value)
    {
        if (!PlayerOnGround && value == 1 || PlayerOnGround && value == 0)
        {
            base.ObjectThrowThing(value);
        }
    }

    //Блимання персонажа, під час блимання персонаж не отримує шкоди
    IEnumerator IndicateImmortal()
    {
        while (immortal)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }

    public override IEnumerator TakeDamage()
    {
        if (!immortal)
        {
            healthStat.CurrentVal -= 10;

            if (!IsDead)
            {
                ObjectAnimator.SetTrigger("animatorTakeDamage");
                immortal = true;

                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);

                immortal = false;
            }

            else
            {
                ObjectAnimator.SetLayerWeight(1, 0);
                ObjectAnimator.SetTrigger("animatorDie");
            }
        }
    }

    public override void Death()
    {
        PlayerRigidbody2D.velocity = Vector2.zero;
        ObjectAnimator.SetTrigger("animatorIdle");
        healthStat.CurrentVal = healthStat.MaxVal;
        transform.position = startPosition.position;
    }

    void Use()
    {
        if(usable != null)
        {
            usable.UseObject();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Trip trip = GetComponent<Trip>();
        if (other.gameObject.CompareTag("Gems"))
        {
            GameManager.InstanceGameManager.CollectedGems++;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Trip"))
        {
            if (trip.TripIsGround == false)
            {
                StartCoroutine(TakeDamage());
            }
        }
    }
}
