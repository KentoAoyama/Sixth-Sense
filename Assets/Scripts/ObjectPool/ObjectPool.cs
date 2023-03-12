using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// �I�u�W�F�N�g�v�[�������ۂɎg�p���钊�ۃN���X
/// </summary>
/// <typeparam name="T">�쐬����I�u�W�F�N�g�v�[���̃N���X</typeparam>
[System.Serializable]
public class ObjectPool<T> where T : MonoBehaviour
{
    [Tooltip("�v�[���̃f�t�H���g�̗e��")]
    [SerializeField]
    private int _poolCapacity = 20;

    [Tooltip("�v�[���̍ő�T�C�Y")]
    [SerializeField]
    private int _poolMaxSize = 50;

    [Tooltip("��������v���n�u")]
    [SerializeField]
    private T _prefab;

    private readonly bool _collectionCheck = true;

    private IObjectPool<T> _pool;
    public IObjectPool<T> Pool => _pool;


    public ObjectPool()
    {
        //�I�u�W�F�N�g�v�[�����쐬
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
    /// �������鏈��
    /// </summary>
    private T CreateBullet()
    {
        T objectInstance = GameObject.Instantiate(_prefab);

        return objectInstance;
    }

    /// <summary>
    /// �v�[������g�p����Ƃ��̏���
    /// </summary>
    public virtual void OnGetFromPool(T poolObject)
    {
        poolObject.gameObject.SetActive(true);
    }

    /// <summary>
    /// �v�[���ɖ߂��Ƃ��̏���
    /// </summary>
    public virtual void OnReleaseToPool(T poolObject)
    {
        poolObject.gameObject.SetActive(false);
    }

    /// <summary>
    /// �v�[������j������Ƃ��̏���
    /// </summary>
    private void OnDestroyPooledObject(T poolObject)
    {
        GameObject.Destroy(poolObject.gameObject);
    }
}