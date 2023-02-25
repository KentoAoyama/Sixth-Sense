using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : MonoBehaviour
{
    [Tooltip("プレイヤーのクラス")]
    [SerializeField]
    private PlayerController _player;

    [Tooltip("敵の生成を行うクラス")]
    [SerializeField]
    private EnemyGenerator _enemyGenerator;

    [Tooltip("弾のオブジェクトプール")]
    [SerializeField]
    private NormalBulletPool _bulletPool;

    [Tooltip("敵のオブジェクトプール")]
    [SerializeField]
    private EnemyPool _enemyPool;

    [Tooltip("GUIの管理をするクラス")]
    [SerializeField]
    private GUIPresenter _gui;

    void Start()
    {
        CursorInit();
        _player.Initialize(_bulletPool);
        _enemyGenerator.Initialize(_player, _bulletPool, _enemyPool);
        _gui.Initialize(_player);
    }

    private void CursorInit()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;

        _player.ManualUpdate(deltaTime);
        _enemyGenerator.ManualUpdate(deltaTime);
    }
}
