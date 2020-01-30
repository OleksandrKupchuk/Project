using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeState : IEnemyState
{
    EnemyNinja enemyRangeNinja;

    float enemyCastFireBallTimer;
    float enemyTimeDelayCastFireBall = 3f;
    bool canThrow = true;

    public void Enter(EnemyNinja enemyNinja)
    {
        enemyRangeNinja = enemyNinja;
    }

    public void Execute()
    {
        CastFireBall();

        if (enemyRangeNinja.InMeleeRange)
        {
            enemyRangeNinja.ChangeState(new MeleeState());
        }

        else if(enemyRangeNinja.TargetForEnemy != null)
        {
            enemyRangeNinja.Move();
        }

        else
        {
            enemyRangeNinja.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    void CastFireBall()
    {
        enemyCastFireBallTimer += Time.deltaTime;
        if(enemyCastFireBallTimer >= enemyTimeDelayCastFireBall)
        {
            canThrow = true;
            enemyCastFireBallTimer = 0;
        }

        if (canThrow)
        {
            canThrow = false;
            enemyRangeNinja.ObjectAnimator.SetTrigger("animatorCastFireBall");
        }
    }
}
