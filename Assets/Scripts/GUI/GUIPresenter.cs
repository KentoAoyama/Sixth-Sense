using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIPresenter : MonoBehaviour
{
    [Tooltip("クロスヘアを管理するクラス")]
    [SerializeField]
    private CrossHairController _crossHair;

    private PlayerController _player;

    public void Initialize(PlayerController player)
    {
        _player = player;
        _crossHair.Initialize(_player.Shooter.ShootInterval);
        //射撃時に実行されるイベントに登録する
        _player.Shooter.OnBulletShoot += _crossHair.RotateCrossHair;
    }

    private void OnDisable()
    {
        _player.Shooter.OnBulletShoot -= _crossHair.RotateCrossHair;
    }
}
