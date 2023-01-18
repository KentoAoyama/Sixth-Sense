using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Tooltip("弾のスピード")]
    [SerializeField]
    private float _bulletSpeed = 1000f;

    [SerializeField]
    private Rigidbody _rb;

    private Speed _speed = new();

    void Start()
    {
        _rb.velocity = transform.forward * _bulletSpeed * _speed.CurrentSpeed * Time.deltaTime;
    }
}
