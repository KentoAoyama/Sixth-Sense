using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

[System.Serializable]
public class EnemyDeath
{
    [Header("頭の当たり判定")]
    [SerializeField]
    private Collider _head;


    [Header("右の二の腕の当たり判定")]
    [SerializeField]
    private Collider _rightArm;

    [Header("右腕の当たり判定")]
    [SerializeField]
    private Collider _rightForeArm;

    [Header("右手の当たり判定")]
    [SerializeField]
    private Collider _rightHand;


    [Header("左の二の腕の当たり判定")]
    [SerializeField]
    private Collider _leftArm;

    [Header("左腕の当たり判定")]
    [SerializeField]
    private Collider _leftForeArm;

    [Header("左手の当たり判定")]
    [SerializeField]
    private Collider _leftHand;


    [Header("右上脚の当たり判定")]
    [SerializeField]
    private Collider _rightUpLeg;

    [Header("右脚の当たり判定")]
    [SerializeField]
    private Collider _rightLeg;


    [Header("左上脚の当たり判定")]
    [SerializeField]
    private Collider _leftUpLeg;

    [Header("左脚の当たり判定")]
    [SerializeField]
    private Collider _leftLeg;


    [Header("被攻撃時のパラメーター")]
    [Tooltip("ノックバックの際に加える力")]
    [SerializeField]
    private float _knockBackPower = 10f;

    [Tooltip("設定するRigidBodyのangularDragの値")]
    [SerializeField]
    private float _angularDrag = 30f;

    [Tooltip("死亡してからプールに戻すまでの時間")]
    [SerializeField]
    private float _interval = 5f;

    private float _timer = 0f;

    private List<Collider> _colliders = new();
    private Transform[] _childrenObjects;
    //private Transform[] _copyChildrenObjects;
    private Rigidbody[] _rbs;

    private Animator _animator;
    private NavMeshAgent _navMesh;
    private ObjectPoolsController _objectPool;
    private GameObject _enemy;
    private Speed _speed;
    private ScoreController _scoreController;

    /// <summary>
    /// 現在消しているコライダー
    /// </summary>
    private Collider _deletedCollider;

    public void Initialize(
        Animator animator,
        NavMeshAgent navMesh,
        ObjectPoolsController objectPool, 
        Speed speed,
        ScoreController scoreController,
        GameObject enemy)
    {
        _animator = animator;
        _navMesh = navMesh;
        _objectPool = objectPool;
        _speed = speed;
        _scoreController = scoreController;

        //layerを変更するため全てのオブジェクトを取得
        _childrenObjects = enemy.gameObject.transform.GetComponentsInChildren<Transform>();

        //ラグドール時の挙動調整のためRigidBodyをすべて取得
        _rbs = enemy.transform.GetComponentsInChildren<Rigidbody>();
        foreach (var rb in _rbs)
        {
            rb.angularDrag = _angularDrag;
            rb.useGravity = false;
        }

        //部位破壊を行うパーツをListに追加
        _colliders.Add(_head);
        _colliders.Add(_rightArm);
        _colliders.Add(_rightForeArm);
        _colliders.Add(_rightHand);
        _colliders.Add(_leftArm);
        _colliders.Add(_leftArm);
        _colliders.Add(_leftForeArm);
        _colliders.Add(_rightUpLeg);
        _colliders.Add(_rightLeg);
        _colliders.Add(_leftUpLeg);
        _colliders.Add(_leftLeg);
    }

    public void KnockBack(Collider collider, Vector3 dir)
    {
        Debug.Log("KnockBack");

        //弾が当たった部位のコライダーに力を加える
        Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(dir * _knockBackPower, ForceMode.Impulse);

        BodyBreak(collider);
    }

    private void BodyBreak(Collider collider)
    {
        //部位破壊可能なオブジェクトだった場合行う
        if (_colliders.Contains(collider))
        {
            collider.gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
            //後で戻すため破壊したオブジェクトを取っておく
            _deletedCollider = collider;
        }
    }

    public void Dead()
    {
        //弾とプレイヤーに接触しないようにレイヤ―を変更する
        foreach (Transform child in _childrenObjects)
        {
            child.gameObject.layer = 8;
        }

        //アニメーターとNavMeshを切ってラグドール化させる
        _animator.enabled = false;
        _navMesh.enabled = false;

        _scoreController.AddScore();
    }

    public void Revive()
    {
        _timer = 0f;

        //破壊している部位がある場合元に戻す
        if (_deletedCollider != null)
        {
            _deletedCollider.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            _deletedCollider = null;
        }
        
        //変えていたレイヤーを元に戻す
        foreach (Transform child in _childrenObjects)
        {
            child.gameObject.layer = 6;
        }
        //アニメーターとNavMeshを起動する
        _navMesh.enabled = true;
        _animator.enabled = true;
    }

    public void DeadUpdate(float deltaTime, EnemyController enemyController)
    {
        _timer += deltaTime;

        //重力を手動で加える
        foreach (var rb in _rbs)
        {
            rb.AddForce(Physics.gravity * _speed.CurrentSpeed * deltaTime * 200);
        }

        //一定時間がたったらプールにオブジェクトを戻し、Stateを変更する
        if (_timer > _interval)
        {
            _objectPool.EnemyPool.Pool.Release(enemyController);
            enemyController.StateMachine.TransitionState(new IdleState());
        }
    }
}
