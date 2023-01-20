using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerShoot
{
    [Tooltip("Rayの最大の長さ")]
    [SerializeField]
    private float _rayLength = 100f;

    [SerializeField]
    private LayerMask _layer;

    [SerializeField]
    private float _shootInterval = 1f;

    [Tooltip("弾のプレハブ")]
    [SerializeField]
    private GameObject _bullet;

    [Tooltip("銃口の位置")]
    [SerializeField]
    private Transform _muzzle;

    [Tooltip("クロスヘアのImage")]
    [SerializeField]
    private Image _crassHair;

    Vector3 _shootPos = new (0, 0, 0);

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

        if (isShoot && _shootInterval < _shootIntervalTimer)
        {
            Ray ray = Camera.main.ScreenPointToRay(_crassHair.rectTransform.position);
            var bullet = Object.Instantiate(_bullet, _muzzle.position, default);

            if (Physics.Raycast(ray, out RaycastHit hit, _rayLength))
            {             
                bullet.transform.forward = hit.point - _muzzle.transform.position;
            }
            else
            {
                bullet.transform.forward = _transform.forward * _rayLength - _muzzle.transform.position;
            }

            _shootIntervalTimer = 0f;
        }
    }
}
