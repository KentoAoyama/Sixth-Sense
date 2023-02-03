using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SlowDownTest : MonoBehaviour
{
    [SerializeField] Transform _root;

    private Rigidbody[] _rbs;

    bool _isWorking = false;

    private SpeedManager _speedManager = new();
    private Speed _speed = new();


    private void Start()
    {
        _rbs = _root.GetComponentsInChildren<Rigidbody>();

        foreach (var rb in _rbs)
        {
            rb.angularDrag = 10f;
        }
    }

    /// <summary>
    /// 機能を発動する
    /// </summary>
    public void Slow(float speed)
    {
        if (_isWorking) return;
        _isWorking = true;

        SppedChange(speed);
    }

    /// <summary>
    /// 機能を元に戻す
    /// </summary>
    public void Resume()
    {
        if (!_isWorking) return;
        SppedChange(1f);
        _isWorking = false;
    }


    private void SppedChange(float spped)
    {
        _speedManager.ChangeSpeed(spped, ChangeSpeedType.All);

        foreach (var rb in _rbs)
        {
            rb.velocity *= _speed.CurrentSpeed;
            rb.angularVelocity *= _speed.CurrentSpeed;
        }

        Physics.gravity = new Vector3(0f, -9.8f, 0f) * _speed.CurrentSpeed;
    }
}
