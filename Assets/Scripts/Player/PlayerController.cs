using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [Header("コンポーネント")]

    [Tooltip("プレイヤーのRigidBody")]
    [SerializeField]
    private Rigidbody _rb;


    [Header("機能ごとのクラス")]

    [SerializeField]
    private PlayerMove _mover;

    [SerializeField]
    private PlayerShoot _shooter;

    /// <summary>
    /// 入力を受け取るインターフェース
    /// </summary>
    [Inject]
    private IInputProvider _input;


    public void Initialize()
    {
        _mover.Initialize(_rb, transform);
    }

    public void ManualUpdate(float deltaTime)
    {
        _mover.Move(_input.GetMoveDir());
        _shooter.BulletShoot(_input.GetFire(), deltaTime);
    }
}
