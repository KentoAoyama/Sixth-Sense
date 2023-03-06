using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTest : MonoBehaviour
{
    [SerializeField]
    private EnemyGenerator _generator;

    [SerializeField]
    private NormalBulletPool _bulletPool;

    [SerializeField]
    private EnemyPool _enemyPool;

    [SerializeField]
    private Transform _muzzle;

    void Start()
    {
        _generator.Initialize(default, _bulletPool, _enemyPool, default, default);
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;

        _generator.ManualUpdate(deltaTime);
    }

    public void CreateBullet()
    {
        NormalBulletController bulletController =  _bulletPool.Pool.Get();
        GameObject bullet = bulletController.gameObject;
        bullet.transform.position = _muzzle.position;

        bullet.GetComponent<NormalBulletController>().MoveStart(_bulletPool, default);
    }
}
