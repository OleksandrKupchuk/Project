using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void Execute();
    void Enter(EnemyNinja enemyNinja);
    void Exit();
    void OnTriggerEnter2D(Collider2D other);
}
