using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySearch
{
    [Tooltip("索敵範囲")]
    [SerializeField]
    private float _searchLength = 10f;

    [Tooltip("Rayを撃つ始点の座標")]
    [SerializeField]
    private Transform _head;

    [Tooltip("Rayが接触するLayer")]
    [SerializeField]
    private LayerMask _layer;

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
        Vector3 _dir = _player.transform.position - _head.transform.position;
        float distance = (_dir).sqrMagnitude;
        Ray ray = new (_head.transform.position, _dir);

        //プレイヤーが近くにいたら
        if (distance < _searchLength * _searchLength)
        {
            //Rayを撃ち、当たったオブジェクトがプレイヤーか判定
            _isSearch = Physics.Raycast(ray, out RaycastHit hit, _searchLength, _layer)
                && hit.collider.gameObject.GetComponent<PlayerController>();
        }

        return _isSearch;
    }
}
