using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyMove
{
    [Tooltip("“G‚ÌˆÚ“®‘¬“x")]
    [SerializeField]
    private float _moveSpeed = 1f;

    private PlayerController _player;

    private NavMeshAgent _navMesh;

    public void Initialize(PlayerController player, NavMeshAgent navMesh)
    {
        _player = player;
        _navMesh = navMesh;
    }

    public void Move()
    {
        _navMesh
            .SetDestination(
            _player.gameObject.transform.position);

        if (_navMesh.speed != _moveSpeed) _navMesh.speed = _moveSpeed;
    }
}
