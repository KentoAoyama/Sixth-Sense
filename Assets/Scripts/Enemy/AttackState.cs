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
        Debug.Log("Enter:AttackState");

        _enemy.ChangeIKWeight(1f);
        _enemy.AttackStart();
    }

    public void Update(float deltaTime)
    {
        _enemy.Move();

        if (!_enemy.PlayerSearch())
        {
            //プレイヤーを目視していなければSearchStateに変更する
            _enemy.StateMachine.TransitionState(_enemy.StateMachine.Search);
        }
    }

    public void Exit() 
    {
        Debug.Log("Exit:AttackState");

        _enemy.AttackStop();
        _enemy.ChangeIKWeight(0f);
    }
}
