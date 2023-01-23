using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    private SpeedManager _speedManager = new();

    public void SpeedChange(float speed, ChangeSpeedType type)
    {
        _speedManager.ChangeSpeed(speed, type);
    }

    public void Pause()
    {
        _speedManager.Pause();
    }

    public void Resume()
    {
        _speedManager.Resume();
    }

    private void OnDisable()
    {
        _speedManager.Reset();
    }
}
