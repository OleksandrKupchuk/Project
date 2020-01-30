using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : IEnemyState
{
    EnemyNinja enemyMeleeNinja;

    float enemyAttackTimer;
    float enemyTimeDelayAttack = 1f;
    bool canAttack = true;

    public void Enter(EnemyNinja enemyNinja)
    {
        enemyMeleeNinja = enemyNinja;
    }

    public void Execute()
    {
        Attack();

        if(!enemyMeleeNinja.InMeleeRange && enemyMeleeNinja.InCastFireBallRange)
        {
            enemyMeleeNinja.ChangeState(new RangeState());
        }

        else if(enemyMeleeNinja.TargetForEnemy == null)
        {
            enemyMeleeNinja.ChangeState(new IdleState());
        }
        
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
       
    }

    void Attack()
    {
        enemyAttackTimer += Time.deltaTime;
        if (enemyAttackTimer >= enemyTimeDelayAttack)
        {
            canAttack = true;
            enemyAttackTimer = 0;
        }

        if (canAttack)
        {
            canAttack = false;
            enemyMeleeNinja.ObjectAnimator.SetTrigger("animatorAttack");
        }
    }
}
