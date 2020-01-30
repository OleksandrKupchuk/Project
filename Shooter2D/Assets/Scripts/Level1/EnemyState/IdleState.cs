using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    EnemyNinja enemyIdleNinja;

    private float idleTimer;

    private float idleDuration;

    public void Enter(EnemyNinja enemyNinja)
    {
        idleDuration = UnityEngine.Random.Range(1f, 10f);
        enemyIdleNinja = enemyNinja;
    }

    public void Execute()
    {
        //Debug.Log("IdleExecute");
        Idle();

        if(enemyIdleNinja.TargetForEnemy != null)
        {
            enemyIdleNinja.ChangeState(new PatrolState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player_FireBall"))
        {
            enemyIdleNinja.TargetForEnemy = PlayerController.Instance.gameObject;
        }
    }

    void Idle()
    {
        enemyIdleNinja.ObjectAnimator.SetFloat("isWalk", 0);

        idleTimer += Time.deltaTime;

        if(idleTimer >= idleDuration)
        {
            enemyIdleNinja.ChangeState(new PatrolState());
        }
    }
}
