using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoyClass : EnemyBaseClass
{
    public Transform[] moveSpots;
    public float closeDistance;
    int randomSpots;
    public float waitTimeOnPoint;
    float startWaitTimeOnPoint;
    bool isFasingRightEnemy;

    Vector3 currentPosition;

    public Animator animatorEnemyBoy;
    // Start is called before the first frame update
    void Start()
    {
        currentPosition = transform.position;
        startWaitTimeOnPoint = waitTimeOnPoint;
        randomSpots = Random.Range(0, moveSpots.Length);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpots].position, enemySpeed * Time.deltaTime);

        animatorEnemyBoy.SetBool("isWalk", true);


        if(Vector2.Distance(transform.position, moveSpots[randomSpots].position) < closeDistance)
        {
            if(waitTimeOnPoint <= 0)
            {
                randomSpots = Random.Range(0, moveSpots.Length);
                waitTimeOnPoint = startWaitTimeOnPoint;
            }

            else
            {
                waitTimeOnPoint -= Time.deltaTime;
                animatorEnemyBoy.SetBool("isWalk", false);
            }
        }
        
    }

    public void TakeDamage()
    {
        enemyHealth--;
        if (enemyHealth <= 0)
        {
            DeadEnemy();
        }
    }

    void DeadEnemy()
    {
        Destroy(gameObject);
    }

    public void FlipEnemy()
    {
        isFasingRightEnemy = !isFasingRightEnemy;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
