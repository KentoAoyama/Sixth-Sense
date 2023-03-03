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

    [Tooltip("SoundEffectのオブジェクトプール")]
    [SerializeField]
    private SoundEffectPool _soundEffectPool;

    [Tooltip("GUIの管理をするクラス")]
    [SerializeField]
    private GUIPresenter _gui;

    private void Start()
    {
        CursorInit();
        _player.Initialize(_bulletPool, _soundEffectPool);
        _enemyGenerator.Initialize(_player, _bulletPool, _enemyPool, _soundEffectPool);
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
