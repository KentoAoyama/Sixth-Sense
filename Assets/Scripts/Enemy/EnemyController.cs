using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [Tooltip("NavmeshAgentコンポーネント")]
    [SerializeField]
    private NavMeshAgent _navMesh;

    [SerializeField]
    private float _gravity = 9.8f;

    [SerializeField, Range(0f, 0.02f)]
    private float _fixedUpdate = 0.02f;


    [Header("機能ごとのクラス")]

    [SerializeField]
    private EnemyAttack _attacker;

    [SerializeField]
    private EnemyMove _mover;

    private PlayerController _player;

    private EnemyStateMachine _stateMachine;
    public EnemyStateMachine StateMachine => _stateMachine;

    private void Start()
    {
        //仮置きでFindする
        _player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();

        //機能ごとのクラスを初期化
        _attacker.Initialize(_player);
        _mover.Initialize(_player, _navMesh);

        //StateMachineを初期化し、Stateを設定
        _stateMachine = new (this);
        _stateMachine.Initialized(_stateMachine.Search);
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;

        _stateMachine.Update(deltaTime);
    }

    public void Move()
    {
        _mover.Move();
    }
}
