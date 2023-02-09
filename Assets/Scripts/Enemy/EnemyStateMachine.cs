using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStateMachine
{
    private IEnemyState _currentState;
    public IEnemyState CurrentState => _currentState;

    public void Initialized(IEnemyState state)
    {
        _currentState = state;
        state.Enter();
    }

    /// <summary>
    /// Stateを変更する際に呼びだすメソッド
    /// </summary>
    /// <param name="nextState">変更するState</param>
    public void TransitionState(IEnemyState nextState)
    {
        _currentState.Exit();
        _currentState = nextState;
        nextState.Enter();
    }

    /// <summary>
    /// 現在のStateのUpdate処理を行うメソッド
    /// </summary>
    public void Update(float deltaTime)
    {
        if (_currentState != null)
        {
            _currentState.Update(deltaTime);
        }
    }
}
