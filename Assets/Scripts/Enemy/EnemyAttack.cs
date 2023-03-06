using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;

[System.Serializable]
public class EnemyAttack
{
    [Header("銃のプロパティ")]

    [Tooltip("弾のプレハブ")]
    [SerializeField]
    private GameObject _bullet;

    [Tooltip("銃口のTransform")]
    [SerializeField]
    private Transform _muzzle;

    [Tooltip("攻撃のインターバル")]
    [SerializeField]
    private float _interval = 3f;


    [Header("IKの設定")]

    [Tooltip("手の Position に対するウェイト")]
    [SerializeField, Range(0f, 1f)] 
    private float _handPositionWeight = 0;

    [Tooltip("手の Rotation に対するウェイト")]
    [SerializeField, Range(0f, 1f)]
    private float _handRotationWeight = 0;

    private PlayerController _player;

    private Animator _animator;

    private SoundEffectPool _soundEffectPool;

    private Speed _speed;

    private float _timer = 0;

    /// <summary>
    /// 弾のオブジェクトプール
    /// </summary>
    private NormalBulletPool _normalBulletPool;

    public void Initialize(PlayerController player,
        Animator animator, 
        NormalBulletPool bulletPool,
        SoundEffectPool soundEffectPool,
        Speed speed)
    {
        _player = player;
        _animator = animator;
        _normalBulletPool = bulletPool;
        _soundEffectPool = soundEffectPool;
        _speed = speed;

        _timer = _interval / 2;
    }

    public void SetIK()
    {
        _animator.SetIKPosition(
            AvatarIKGoal.RightHand,
            _player.transform.position);

        _animator.SetIKRotation(
            AvatarIKGoal.RightHand,
            _player.transform.rotation);

        _animator.SetIKPositionWeight(
            AvatarIKGoal.RightHand,
            _handPositionWeight);

        _animator.SetIKRotationWeight(
            AvatarIKGoal.RightHand, 
            -_handRotationWeight);         
    }

    public void ChangeIKWeight(float weight)
    {
        _handPositionWeight = weight;
        _handRotationWeight = weight;
    }

    public void Attack(float deltaTime)
    {
        //射撃のインターバル
        _timer += deltaTime * _speed.CurrentSpeed;

        if (_timer > _interval)
        {
            //場合に応じた弾を生成
            NormalBulletController bulletController = _normalBulletPool.Pool.Get();

            GameObject bullet = bulletController.gameObject;
            bullet.transform.position = _muzzle.position;
            bullet.transform.forward = _player.transform.position - _muzzle.transform.position;

            //弾を動かす
            bullet.GetComponent<NormalBulletController>()
                .MoveStart(_normalBulletPool, _soundEffectPool);

            //音のエフェクトを生成
            SoundEffect soundEffect = _soundEffectPool.Pool.Get();
            soundEffect.Initialize(_soundEffectPool, SoundEffectType.Danger2, _muzzle.position);

            _timer = 0;
        }
    }

    public void AttackStop()
    {
        _timer = 0;
    }

    ///// <summary>
    ///// 一定時間ごとに攻撃を行う
    ///// </summary>
    //public async UniTask Attack(CancellationToken token)
    //{
    //    token.ThrowIfCancellationRequested();

    //    while (true)
    //    {
    //        Debug.Log("Delay開始");

    //        射撃のインターバル
    //        await UniTask.Delay(TimeSpan.FromSeconds(_interval), cancellationToken: token);

    //        Debug.Log("弾を撃つ");
    //        場合に応じた弾を生成
    //        NormalBulletController bulletController = _normalBulletPool.Pool.Get();

    //        GameObject bullet = bulletController.gameObject;
    //        bullet.transform.position = _muzzle.position;
    //        bullet.transform.forward = _player.transform.position - _muzzle.transform.position;

    //        弾を動かす
    //        bullet.GetComponent<NormalBulletController>().MoveStart(_normalBulletPool.Pool);
    //    }
    //}
}
