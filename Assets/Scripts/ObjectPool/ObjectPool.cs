using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// オブジェクトプールを作る際に使用する抽象クラス
/// </summary>
/// <typeparam name="T">作成するオブジェクトプールのクラス</typeparam>
[System.Serializable]
public class ObjectPool<T> where T : MonoBehaviour
{
    [Tooltip("プールのデフォルトの容量")]
    [SerializeField]
    private int _poolCapacity = 20;

    [Tooltip("プールの最大サイズ")]
    [SerializeField]
    private int _poolMaxSize = 50;

    [Tooltip("生成するプレハブ")]
    [SerializeField]
    private T _prefab;

    private readonly bool _collectionCheck = true;

    private IObjectPool<T> _pool;
    public IObjectPool<T> Pool => _pool;


    public ObjectPool()
    {
        //オブジェクトプールを作成
        _pool = new UnityEngine.Pool.ObjectPool<T>(
            CreateBullet,
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPooledObject,
            _collectionCheck,
            _poolCapacity,
            _poolMaxSize);
    }

    /// <summary>
    /// 生成する処理
    /// </summary>
    private T CreateBullet()
    {
        T objectInstance = GameObject.Instantiate(_prefab);

        return objectInstance;
    }

    /// <summary>
    /// プールから使用するときの処理
    /// </summary>
    public virtual void OnGetFromPool(T poolObject)
    {
        poolObject.gameObject.SetActive(true);
    }

    /// <summary>
    /// プールに戻すときの処理
    /// </summary>
    public virtual void OnReleaseToPool(T poolObject)
    {
        poolObject.gameObject.SetActive(false);
    }

    /// <summary>
    /// プールから破棄するときの処理
    /// </summary>
    private void OnDestroyPooledObject(T poolObject)
    {
        GameObject.Destroy(poolObject.gameObject);
    }
}
