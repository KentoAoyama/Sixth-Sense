using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public void SpeedChange(float speed)
    {
        SpeedManager.Instance.ChangeSpeed(speed);
    }
}
