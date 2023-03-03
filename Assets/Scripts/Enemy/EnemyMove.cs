using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyMove
{
    [Tooltip("敵の移動速度")]
    [SerializeField]
    private float _moveSpeed = 1f;

    [Tooltip("SoundEffectが出る間隔")]
    [SerializeField]
    private float _soundInterval = 3f;

    private EnemyController _enemy;

    private PlayerController _player;

    private NavMeshAgent _navMesh;

    private SoundEffectPool _soundEffectPool;

    private float _timer = 0f;

    public void Initialize(EnemyController enemy, PlayerController player, NavMeshAgent navMesh, SoundEffectPool soundEffectPool)
    {
        _enemy = enemy;
        _player = player;
        _navMesh = navMesh;
        _soundEffectPool = soundEffectPool;

        _timer = _soundInterval;
    }

    public void Move(float deltaTime)
    {
        _navMesh
            .SetDestination(
            _player.gameObject.transform.position);

        if (_navMesh.speed != _moveSpeed) _navMesh.speed = _moveSpeed;

        //一定時間ごとにサウンドエフェクトを生成
        _timer += deltaTime;

        if (_timer > _soundInterval)
        {
            SoundEffect soundEffect = _soundEffectPool.Pool.Get();
            soundEffect.Initialize(_soundEffectPool, SoundEffectType.Danger, _enemy.transform.position);

            _timer = 0;
        }
    }
}
