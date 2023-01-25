using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyStateMachine _stateMachine;
    public EnemyStateMachine StateMachine => _stateMachine;

    private void Start()
    {
        _stateMachine = new (this);
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;

        _stateMachine.Update(deltaTime);
    }
}
