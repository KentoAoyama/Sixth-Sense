using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : IEnemyState
{
    EnemyController _enemy;

    public SearchState(EnemyController enemy)
    {
        _enemy = enemy;
    }
}
