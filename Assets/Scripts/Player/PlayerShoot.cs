using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerShoot
{
    [Tooltip("Rayの最大の長さ")]
    [SerializeField]
    private float _rayLength = 100f;

    [SerializeField]
    private float _shootInterval = 1f;

    [Tooltip("弾のプレハブ")]
    [SerializeField]
    private GameObject _bullet;

    [Tooltip("銃口の位置")]
    [SerializeField]
    private Transform _muzzle;

    private float _shootIntervalTimer = 0f;

    Transform _transform;

    public void Initialize(Transform transform)
    {
        _transform = transform;
    }

    /// <summary>
    /// プレイヤーの射撃処理
    /// </summary>
    /// <param name="isShoot">射撃を行うかどうか</param>
    public void BulletShoot(bool isShoot, float deltaTime)
    {
        _shootIntervalTimer += deltaTime;

        if (!isShoot && _shootInterval < _shootIntervalTimer) return;

        Physics.Raycast(new Ray(_transform.position, _transform.forward), out RaycastHit hit, _rayLength);

        var bullet = GameObject.Instantiate(_bullet, _muzzle.position, default);
        bullet.transform.forward = hit.transform.position - _muzzle.transform.position;

        _shootIntervalTimer = 0f;
    }
}
