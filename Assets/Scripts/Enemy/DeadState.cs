using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 死亡している状態
/// </summary>
public class DeadState : IEnemyState
{
    EnemyController _enemy;

    public DeadState(EnemyController enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        
    }

    public void Update(float deltaTime)
    {
        
    }

    public void Exit() 
    {
        
    }
}
