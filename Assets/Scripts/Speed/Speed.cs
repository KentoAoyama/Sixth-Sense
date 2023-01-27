using System;
using UnityEngine;
using UniRx;

/// <summary>
/// 速度に関するFloat型の値を取り扱うためのクラス
/// </summary>
public class Speed
{
    // 値の変更を購読できるようReactivePropertyで定義
    private ReactiveProperty<float> _speed = new(1f);

    /// <summary>
    /// スピードのReactiveProperty
    /// </summary>
    public IReadOnlyReactiveProperty<float> SpeedRp => _speed;

    /// <summary>
    /// 現在のスピードの値
    /// </summary>
    public float CurrentSpeed => _speed.Value;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Speed()
    {
        //現在のスピードの値を適用
        _speed.Value = SpeedManager.CurrentSpeed;
        SpeedManager.Subscribe(this);
    }

    public void ChangeValue(float speed)
    {
        _speed.Value = speed;
    }

    /// <summary>
    /// SpeedManagerクラスのListから、このクラスの登録を削除する
    /// </summary>
　　public void Unsubscribe()
    {
        SpeedManager.Unsubscribe(this);
    }
}
