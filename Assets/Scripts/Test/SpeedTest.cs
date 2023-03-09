using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SpeedTest : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private SpeedType _type;

    /// <summary>
    /// 機能ごとに固有のスピードを保持させる
    /// </summary>
    private Speed _speed;

    private void Start()
    {
        _speed = new(_type);

        //スピードが変わったら一度リセット
        _speed.SpeedRp
            .Subscribe(_ => _rb.velocity = new Vector3(0f, 0f, 0f))
            .AddTo(gameObject);
    }

    private void FixedUpdate()
    {
        _rb.AddForce(new Vector3(0f, -9.8f * _speed.CurrentSpeed, 0));
    }
}
