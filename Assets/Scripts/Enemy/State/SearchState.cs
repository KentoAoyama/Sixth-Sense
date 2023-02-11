using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーをまだ見つけていない状態
/// </summary>
public class SearchState : IEnemyState
{
    private EnemyController _enemy;

    public SearchState(EnemyController enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        
    }

    public void Update(float deltaTime)
    {
        _enemy.Move();

        if (_enemy.PlayerSearch())
        {
            //プレイヤーを目視していたらSearchStateに変更する
            _enemy.StateMachine.TransitionState(new AttackState(_enemy));
        }
    }

    public void Exit() 
    {
        
    }
}
