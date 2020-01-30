using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    EnemyNinja enemyPatrolNinja;

    float patrolTimer;
    float patrolDuration = 0f;

    public void Enter(EnemyNinja enemyNinja)
    {
        patrolTimer = UnityEngine.Random.Range(2f, 10f);
        enemyPatrolNinja = enemyNinja;
    }

    public void Execute()
    {
        //Debug.Log("Enemy Patrol");
        Patrol();

        enemyPatrolNinja.Move();

        if(enemyPatrolNinja.TargetForEnemy != null && enemyPatrolNinja.InCastFireBallRange)
        {
            enemyPatrolNinja.ChangeState(new RangeState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player_FireBall"))
        {
            enemyPatrolNinja.TargetForEnemy = PlayerController.Instance.gameObject;
        }
    }

    void Patrol()
    {
        patrolTimer -= Time.deltaTime;
        //Debug.Log("Patrol - Until " + patrolTimer);

        if (patrolTimer <= patrolDuration)
        {
            enemyPatrolNinja.ChangeState(new IdleState());
        }
    }
}
