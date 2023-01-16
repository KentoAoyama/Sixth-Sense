using System;
using UnityEngine;

/// <summary>
/// 速度に関するFloat型の値を取り扱うためのクラス
/// </summary>
public class Speed
{
    private float _speed;

    /// <summary>
    /// 現在のスピードの値
    /// </summary>
    public float CurrentSpeed => _speed;

    /// <summary>
    /// スピードが変更された際に実行するデリゲート
    /// </summary>
    public Action<float> OnSpeedChange;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Speed(float spped)
    {
        _speed = spped;
        SpeedManager.Instance.Subscribe(this);
    }

    public void Change(float speed)
    {
        _speed = speed;
        OnSpeedChange?.Invoke(speed);
    }

    /// <summary>
    /// SpeedManagerクラスのListから、このクラスの登録を削除する
    /// </summary>
　　public void Unsubscribe()
    {
        SpeedManager.Instance.Unsubscribe(this);
    }
}
