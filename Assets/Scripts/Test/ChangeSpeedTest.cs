using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpeedTest : MonoBehaviour
{
    [SerializeField]
    private SpeedController _speed;

    public void NonPlayer(float speed)
    {
        _speed.SpeedChange(speed, ChangeSpeedType.NonPlayer);
    }
}
