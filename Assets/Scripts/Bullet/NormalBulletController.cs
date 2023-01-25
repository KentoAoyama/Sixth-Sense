using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class NormalBulletController : MonoBehaviour
{
    [Tooltip("弾のスピード")]
    [SerializeField]
    private float _bulletSpeed = 1000f;

    [Tooltip("弾が破棄されるまでの時間")]
    [SerializeField]
    private int _destroyInterval = 10;

    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private TrailRenderer _trail;

    private readonly Speed _speed = new();

    private IObjectPool<NormalBulletController> _objectPool;

    Coroutine _delayCoroutine;

    /// <summary>
    /// 生成時に呼び出すメソッド
    /// </summary>
    public void MoveStart(IObjectPool<NormalBulletController> objectPool)
    {
        //オブジェクトプールの参照を渡す
        if (_objectPool == null) _objectPool = objectPool;
        //正面方向にスピードを設定
        _rb.velocity = transform.forward * _bulletSpeed * Time.deltaTime * _speed.CurrentSpeed;
        //破棄を行うコルーチンを実行
        _delayCoroutine = StartCoroutine(DestoryInterval());
    }

    private IEnumerator DestoryInterval()
    {
        yield return new WaitForSeconds(5);

        _delayCoroutine = null;
        Release();
    }

    private void OnTriggerEnter(Collider other)
    {
        Release();
    }

    private void Release()
    {
        if (_delayCoroutine != null) StopCoroutine(_delayCoroutine);

        // 動きをリセット
        _rb.velocity = new Vector3(0f, 0f, 0f);
        _trail.Clear();

        _objectPool.Release(this);
    }
}
