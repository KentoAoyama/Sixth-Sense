using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStateMachine
{
    private IEnemyState _currentState;
    public IEnemyState CurrentState => _currentState;

    //Stateのクラス
    private SearchState _search;
    public SearchState Search => _search;

    private AttackState _attack;
    public AttackState Attack => _attack;

    private DeadState _dead;
    public DeadState Dead => _dead;

    public EnemyStateMachine(EnemyController enemy)
    {
        _search = new SearchState(enemy);
    }

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
