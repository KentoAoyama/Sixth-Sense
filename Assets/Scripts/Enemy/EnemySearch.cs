using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySearch
{
    [Tooltip("索敵範囲")]
    [SerializeField]
    private float _searchLength = 10f;

    private EnemyController _enemy;

    private PlayerController _player;

    public void Initialize(PlayerController player)
    {
        _player = player;
    }


    /// <summary>
    /// プレイヤーを探す処理、距離とRayの２つで行う
    /// </summary>
    /// <returns>プレイヤーを見つけているか</returns>
    public bool PlayerSearch()
    {
        bool _isSearch = false;
        float distance = (_enemy.transform.position - _player.transform.position).sqrMagnitude;

        //プレイヤーが近くにいたら
        if (distance < _searchLength * _searchLength)
        {
            //Rayを撃ち、当たったオブジェクトがプレイヤーか判定
            _isSearch = Physics.Raycast(_enemy.transform.position,
                _player.transform.position - _enemy.transform.position,
                out RaycastHit hit, _searchLength)
                && hit.collider.gameObject.GetComponent<PlayerController>();
        }

        return _isSearch;
    }
}
