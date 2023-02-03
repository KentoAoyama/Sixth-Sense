using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyMove
{
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
    }
}
