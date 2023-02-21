using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading;
using Cysharp.Threading.Tasks;

public class EnemyController : MonoBehaviour, IKnockBackable
{
    [Tooltip("NavmeshAgentコンポーネント")]
    [SerializeField]
    private NavMeshAgent _navMesh;

    [Tooltip("Animatorコンポーネント")]
    [SerializeField]
    private Animator _animator;

    [Header("機能ごとのクラス")]

    [SerializeField]
    private EnemyAttack _attacker;

    [SerializeField]
    private EnemyMove _mover;

    [SerializeField]
    private EnemyDeath _death;

    [SerializeField]
    private EnemySearch _searcher;

    private PlayerController _player;

    private readonly EnemyStateMachine _stateMachine = new();
    public EnemyStateMachine StateMachine => _stateMachine;

    private Speed _speed = new();

    //private readonly CancellationTokenSource _tokenSource = new();

    public void Initialize(PlayerController player, NormalBulletPool bulletPool, EnemyPool enemyPool)
    {
        _player = player;

        //機能ごとのクラスの初期化を実行
        _attacker.Initialize(_player, _animator, bulletPool);
        _mover.Initialize(_player, _navMesh);
        _searcher.Initialize(_player);
        _death.Initialize(_animator, _navMesh, enemyPool, this, _speed);

        //StateMachineを初期化し、Stateを設定
        _stateMachine.Initialized(new SearchState(this));
    }

    private void OnAnimatorIK(int layerIndex)
    {
        _attacker.SetIK();
    }

    public void ManualUpdate(float deltaTime)
    {
        //現在StateのUpdateを実行
        _stateMachine.Update(deltaTime);
    }

    public void ChangeIKWeight(float weight)
    {
        _attacker.ChangeIKWeight(weight);
    }

    /// <summary>
    /// 攻撃を行う、Updateで実行するメソッド
    /// </summary>
    public void Attack(float deltaTime)
    {
        _attacker.Attack(deltaTime);
    }

    public void AttackStop()
    {
        _attacker.AttackStop();
    }

    public void Move()
    {
        //NavMeshを使用して移動させる
        _mover.Move();
    }

    public bool PlayerSearch()
    {
        return _searcher.PlayerSearch();
    }

    /// <summary>
    /// IKnockBackインターフェースによって実装されるノックバック処理
    /// </summary>
    public void KnockBack(Collider collider, Vector3 dir)
    {
        _death.Dead();
        _death.KnockBack(collider, dir);
        _stateMachine.TransitionState(new DeadState(this));
    }
    

    /// <summary>
    /// 再生時の処理
    /// </summary>
    public void Revive()
    {
        _death.Revive();
    }

    /// <summary>
    /// 死亡中に実行するUpdate処理
    /// </summary>
    public void DeadUpdate(float deltaTime)
    {
        _death.DeadUpdate(deltaTime);
    }
}


    /// <summary>
    /// 攻撃を行う、Stateの遷移時に実行するメソッド
    ///// </summary>
    //public void AttackStart()
    //{
    //    //攻撃を一定時間ごとに行うUniTaskを実行し、Forget()で警告を無視
    //    _attacker.Attack(_tokenSource.Token).Forget();
    //}

    ///// <summary>
    ///// 攻撃処理の実行を止める
    ///// </summary>
    //public void AttackStop()
    //{
    //    //実行しているUniTaskの攻撃処理をキャンセルする
    //    _tokenSource.Cancel();
    //}
