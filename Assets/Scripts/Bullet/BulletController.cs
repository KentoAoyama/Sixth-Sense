using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletController : MonoBehaviour, IBullet
{
    [Tooltip("弾のスピード")]
    [SerializeField]
    private float _bulletSpeed = 1000f;

    [Tooltip("弾が破棄されるまでの時間")]
    [SerializeField]
    private int _destroyInterval = 10;

    [SerializeField]
    private Rigidbody _rb;

    /// <summary>
    /// 何かに接触しているか判定する用の変数
    /// </summary>
    bool _isHit;

    private Speed _speed = new();

    private IObjectPool<IBullet> _objectPool;

    // 弾にObjectPoolへの参照を与えるパブリックプロパティ
    public IObjectPool<IBullet> ObjectPool { set => _objectPool = value; }

    Coroutine _delayCoroutine;

    /// <summary>
    /// 生成時に呼び出すメソッド
    /// </summary>
    public void BulletMove()
    {
        _rb.velocity = transform.forward * _bulletSpeed * _speed.CurrentSpeed * Time.deltaTime;

        _delayCoroutine = StartCoroutine(DestoryInterval());
    }

    private IEnumerator DestoryInterval()
    {
        yield return new WaitForSeconds(5);

        Release();
    }

    private void OnTriggerEnter(Collider other)
    {
        _isHit = true;
    }

    private void Release()
    {
        if (_delayCoroutine != null) StopCoroutine(_delayCoroutine);

        // 動きをリセット
        _rb.velocity = new Vector3(0f, 0f, 0f);

        _objectPool.Release(this);
    }
}
