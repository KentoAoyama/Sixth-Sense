using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Rigidbody の速度を遅くする
/// </summary>
public class Slowdown : MonoBehaviour
{
    /// <summary>ここで指定したオブジェクトの子である Rigidbody すべてに対して影響を与える</summary>
    [SerializeField] Transform _root;
    /// <summary>指定した回数につき１回だけ Physics を計算する。例えば 4 と指定した場合は 1 回物理演算をした後、3 回は演算をスキップする。</summary>
    [SerializeField] int _physicsInterval = 4;
    Dictionary<Rigidbody, (Vector3, Vector3)> _dataStore = new Dictionary<Rigidbody, (Vector3, Vector3)>();
    bool _isWorking = false;
    int _counter = 0;

    void FixedUpdate()
    {
        // 指定した _physicsInterval に応じて Physics の演算を止めたり再開したりする
        if (_isWorking)
        {
            if (_counter % _physicsInterval == 0)
            {
                SkipPhysics();
            }
            else if (_counter % _physicsInterval == _physicsInterval - 1)
            {
                ResumePhysics();
            }

            _counter = (_counter + 1) % _physicsInterval;
        }
    }

    /// <summary>
    /// 現在の速度を保存して動きを止める
    /// </summary>
    void SkipPhysics()
    {
        foreach (var keyRb in _dataStore.Keys.ToArray())
        {
            _dataStore[keyRb] = (keyRb.velocity, keyRb.angularVelocity);
            keyRb.Sleep();
        }
    }

    /// <summary>
    /// 止める前の動きに戻す
    /// </summary>
    void ResumePhysics()
    {
        foreach (var keyRb in _dataStore.Keys)
        {
            var value = _dataStore[keyRb];
            keyRb.velocity = value.Item1;
            keyRb.angularVelocity = value.Item2;
        }
    }

    /// <summary>
    /// 機能を発動する
    /// </summary>
    public void Slow()
    {
        if (_isWorking) return;
        _isWorking = true;
        var allRigidbodies = _root.GetComponentsInChildren<Rigidbody>();

        foreach (var rb in allRigidbodies)
        {
            _dataStore.Add(rb, (rb.velocity, rb.angularVelocity));
        }
    }

    /// <summary>
    /// 機能を元に戻す
    /// </summary>
    public void Resume()
    {
        if (!_isWorking) return;
        ResumePhysics();
        _dataStore.Clear();
        _isWorking = false;
    }
}

