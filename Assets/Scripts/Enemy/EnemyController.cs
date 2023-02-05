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

    [Tooltip("Animatorコンポーネント")]
    [SerializeField]
    private Animator _animator;

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
        _attacker.Initialize(_player, _animator);
        _mover.Initialize(_player, _navMesh);

        //StateMachineを初期化し、Stateを設定
        _stateMachine = new (this);
        _stateMachine.Initialized(_stateMachine.Attack);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        _attacker.SetIK();
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

    public void ChangeIKWeight()
    {
        _attacker.ChangeIKWeight();
    }

    public void Attack()
    {
        _attacker.Attack();
    }
}
