using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolsController : MonoBehaviour
{
    [Header("弾のオブジェクトプール")]
    [SerializeField]
    private ObjectPool<NormalBulletController> _bulletPool;
    /// <summary>
    /// 弾のオブジェクトプール
    /// </summary>
    public ObjectPool<NormalBulletController> BulletPool => _bulletPool;

    [Header("敵のオブジェクトプール")]
    [SerializeField]
    private ObjectPool<EnemyController> _enemyPool;
    /// <summary>
    /// 弾のオブジェクトプール
    /// </summary>
    public ObjectPool<EnemyController> EnemyPool => _enemyPool;

    [Header("サウンドエフェクトのオブジェクトプール")]
    [SerializeField]
    private ObjectPool<SoundEffect> _soundEffectPool;
    /// <summary>
    /// サウンドエフェクトのオブジェクトプール
    /// </summary>
    public ObjectPool<SoundEffect> SoundEffectPool => _soundEffectPool;
}
