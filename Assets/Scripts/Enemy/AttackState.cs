using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーを見つけて攻撃を行う状態
/// </summary>
public class AttackState : IEnemyState
{
    private EnemyController _enemy;

    public AttackState(EnemyController enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        _enemy.ChangeIKWeight();
        Debug.Log("Enter:AttackState");
    }

    public void Update(float deltaTime)
    {
        _enemy.Move();
    }

    public void Exit() 
    {
        
    }
}
