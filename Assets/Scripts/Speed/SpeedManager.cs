using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager
{
    private static SpeedManager _instance = new();
    public static SpeedManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError($"Error! Please correct!");
            }
            return _instance;
        }
    }
    private SpeedManager() { }

    /// <summary>
    /// ˆ—‚ğs‚¤SpeedƒNƒ‰ƒX‚ÌList
    /// </summary>
    private readonly List<Speed> _speedList = new();

    /// <summary>
    /// SpeedƒNƒ‰ƒX‚Ì“o˜^
    /// </summary>
    public void Subscribe(Speed speed)
    {
        _speedList.Add(speed);
    }

    /// <summary>
    /// SpeedƒNƒ‰ƒX‚Ì“o˜^‰ğœ
    /// </summary>
    public void Unsubscribe(Speed speed)
    {
        _speedList.Remove(speed);
    }

    public void ChangeSpeed(float value)
    {
        foreach (var speed in _speedList)
        {
            speed.Change(value);
        }
    }
}
