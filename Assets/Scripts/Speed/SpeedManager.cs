using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager
{
    /// <summary>
    /// 処理を行うSpeedクラスのList
    /// </summary>
    private static List<Speed> _speedList = new();

    /// <summary>
    /// 現在のスピードを保存しておく用のList
    /// </summary>
    private List<float> _currentSpeedList = new();

    private static float _currentSpeed = 1f;
    public static float CurrentSpeed => _currentSpeed;

    /// <summary>
    /// Speedクラスの登録を行うstaticメソッド
    /// </summary>
    public static void Subscribe(Speed speed)
    {
        _speedList.Add(speed);
    }

    /// <summary>
    /// Speedクラスの登録解除を行うstaticメソッド
    /// </summary>
    public static void Unsubscribe(Speed speed)
    {
        _speedList.Remove(speed);
    }

    /// <summary>
    /// ポーズ処理
    /// </summary>
    public void Pause()
    {
        _speedList.ForEach(s => _currentSpeedList.Add(s.CurrentSpeed));
        ChangeSpeed(0f, ChangeSpeedType.All);
    }

    /// <summary>
    /// ポーズの解除処理
    /// </summary>
    public void Resume()
    {
        for (int i = 0; i < _speedList.Count; i++)
        {
            _speedList[i].ChangeValue(_currentSpeedList[i]);
        }
    }

    public void ChangeSpeed(float value, ChangeSpeedType type)
    {
        _currentSpeed = value;

        foreach (var speed in _speedList)
        {
            speed.ChangeValue(value);
        }
    }

    public void Reset()
    {
        _speedList.Clear();
    }
}