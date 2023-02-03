using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAttack
{
    private PlayerController _player;

    public void Initialize(PlayerController player)
    {
        _player = player;
    }

    public void Attack()
    {

    }
}
