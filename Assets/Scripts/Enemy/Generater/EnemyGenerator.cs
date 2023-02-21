using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [Header("生成に関する設定")]

    [Tooltip("生成する座標")]
    [SerializeField]
    private Transform[] _generatePos;

    [Tooltip("どの順番でどの座標から生成するか指定する配列")]
    [Range(0, MAX_POS_LENGTH)]
    [SerializeField]
    private int[] _generatePosIndex;

    [Tooltip("生成のインターバル")]
    [SerializeField]
    private float _interval = 5f;

    [Tooltip("生成を行う回数")]
    [SerializeField]
    private int _generateTime = 5;

    private const int MAX_POS_LENGTH = 10;

    private float _timer = 0;
    private int _indexCount = 0;
    private int _generateCount = 0;

    private PlayerController _player;
    private NormalBulletPool _bulletPool;
    private EnemyPool _enemyPool;

    /// <summary>
    /// 生成した敵を保持するリスト
    /// </summary>
    private List<EnemyController> _enemys = new();

    public void Initialize(PlayerController player, NormalBulletPool bulletPool, EnemyPool enemyPool)
    {
        //配列の長さがオーバーしていないかチェック
        if (_generatePos.Length > MAX_POS_LENGTH)
        {
            Debug.LogError($"指定した座標の配列が{MAX_POS_LENGTH}より長くなっています");
        }

        //指定したインデックスの要素があるか判定
        foreach (int n in _generatePosIndex)
        {
            if (n > _generatePos.Length)
                Debug.LogError("GeneratePosIndexの要素が、配列の要素外を指定しています");
        }

        //InGameControllerから参照を受け取る
        _player = player;
        _bulletPool = bulletPool;
        _enemyPool = enemyPool;
    }

    public void ManualUpdate(float deltaTime)
    {
        Generate(deltaTime);
        EnemysUpdate(deltaTime);
    }

    private void Generate(float deltaTime)
    {
        //指定した回数以上は生成しない
        if (_generateCount >= _generateTime) return;

        _timer += deltaTime;

        if (_timer > _interval)
        {
            //敵を生成
            EnemyController enemy = _enemyPool.Pool.Get();
            //まだ追加されていなければListに追加、初期化処理
            if (!_enemys.Contains(enemy)) 
            {
                _enemys.Add(enemy);
                enemy.Initialize(_player, _bulletPool, _enemyPool);
            }
            //既に追加されていたらStateを変更する
            else
            {
                enemy.StateMachine.TransitionState(new SearchState(enemy));
            }
            //指定した座標に移動
            enemy.transform.position =
                _generatePos[_generatePosIndex[_indexCount % _generatePosIndex.Length]].position;

            _indexCount++;
            _generateCount++;
            _timer = 0;
        }
    }

    private void EnemysUpdate(float deltaTime)
    {
        if (_enemys.Count > 0)
        {
            foreach (var enemy in _enemys)
            {
                enemy.ManualUpdate(deltaTime);
            }
        }
    }
}
