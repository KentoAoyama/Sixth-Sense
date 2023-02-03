using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            rb.angularDrag = 30f;
            rb.useGravity = false;
        }
    }

    private void FixedUpdate()
    {
        if (_isWorking)
        {
            foreach (var rb in _rbs)
            {
                rb.AddForce(Physics.gravity * _speed.CurrentSpeed);
            }
        }
    }

    public void Slow(float speed)
    {
        if (_isWorking) return;
        _isWorking = true;

        SppedChange(speed);
    }

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
    }
}
