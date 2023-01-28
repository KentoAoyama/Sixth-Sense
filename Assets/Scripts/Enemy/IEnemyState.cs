using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    /// <summary>
    /// このStateに変更された際に実行する
    /// </summary>
    void Enter() { }

    /// <summary>
    /// このStateのUpdate処理
    /// </summary>
    void Update() { }

    /// <summary>
    /// このStateのUpdate処理
    /// </summary>
    /// <param name="deltaTime"></param>
    void Update(float deltaTime) { }

    /// <summary>
    /// このStateから別のStateに変更された際に実行する
    /// </summary>
    void Exit() { }
}
