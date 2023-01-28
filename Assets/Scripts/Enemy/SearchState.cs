using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ƒvƒŒƒCƒ„[‚ğ‚Ü‚¾Œ©‚Â‚¯‚Ä‚¢‚È‚¢ó‘Ô
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
    }

    public void Exit() 
    {
        
    }
}
