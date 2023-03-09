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

    private PlayerController _player;

    private NavMeshAgent _navMesh;

    private ObjectPoolsController _objectPool;

    private Speed _speed;

    private float _timer = 0f;

    public void Initialize(
        PlayerController player, 
        NavMeshAgent navMesh, 
        ObjectPoolsController objectPool,
        Speed speed)
    {
        _player = player;
        _navMesh = navMesh;
        _objectPool = objectPool;
        _speed = speed;

        _timer = _soundInterval;
    }

    public void Move(float deltaTime, Vector3 soundCreatePos)
    {
        _navMesh
            .SetDestination(
            _player.gameObject.transform.position);

        if (_navMesh.speed != _moveSpeed * _speed.CurrentSpeed)
            _navMesh.speed = _moveSpeed * _speed.CurrentSpeed;

        //一定時間ごとにサウンドエフェクトを生成
        _timer += deltaTime * _speed.CurrentSpeed;

        if (_timer > _soundInterval)
        {
            SoundEffect soundEffect = _objectPool.SoundEffectPool.Pool.Get();
            soundEffect.Initialize(_objectPool.SoundEffectPool.Pool, SoundEffectType.Danger1, soundCreatePos);

            _timer = 0;
        }
    }
}
