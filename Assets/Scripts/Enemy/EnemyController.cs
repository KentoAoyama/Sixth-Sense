using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading;
using Cysharp.Threading.Tasks;

public class EnemyController : MonoBehaviour
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
    private EnemySearch _searcher;

    private PlayerController _player;

    private readonly EnemyStateMachine _stateMachine = new();
    public EnemyStateMachine StateMachine => _stateMachine;

    /// <summary>
    /// UniTaskキャンセル用のTokenSource
    /// </summary>
    //private readonly CancellationTokenSource _tokenSource = new();

    public void Initialize(PlayerController player, NormalBulletPool bulletPool)
    {
        _player = player;

        //機能ごとのクラスを初期化
        _attacker.Initialize(_player, _animator, bulletPool);
        _mover.Initialize(_player, _navMesh);
        _searcher.Initialize(_player);

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

    public void Move()
    {
        //NavMeshを使用して移動させる
        _mover.Move();
    }

    public bool PlayerSearch()
    {
        return _searcher.PlayerSearch();
    }
}
