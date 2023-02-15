using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBreak : MonoBehaviour, IHittable
{
    [Tooltip("その部位ごとのコライダー")]
    [SerializeField]
    Collider _collider;

    private bool _isHit;
    public bool IsHit => _isHit;

    /// <summary>
    /// 攻撃命中時に行う処理
    /// </summary>
    public void Hit()
    {
        _isHit = true;
    }
}
