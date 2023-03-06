using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    private readonly SpeedManager _speedManager = new();

    public void SpeedChange(float speed)
    {
        _speedManager.ChangeSpeed(speed, ChangeSpeedType.All);
    }

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
        _speedManager.ChangeSpeed(1f, ChangeSpeedType.All);
        _speedManager.Reset();
    }
}
