using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Tooltip("NavmeshAgentコンポーネント")]
    [SerializeField]
    private NavMeshAgent _navMesh;

    private PlayerController _player;
    public PlayerController Player => _player;

    private EnemyStateMachine _stateMachine;
    public EnemyStateMachine StateMachine => _stateMachine;

    private void Start()
    {
        //仮置きでFindする
        _player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();

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
        _navMesh
            .SetDestination(
            Player.gameObject.transform.position);
    }
}
