using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GUIPresenter : MonoBehaviour
{
    [Tooltip("クロスヘアを管理するクラス")]
    [SerializeField]
    private CrossHairController _crossHair;

    [Tooltip("HelpMenuを管理するクラス")]
    [SerializeField]
    private HelpController _help;

    [Tooltip("ScoreのTextを管理するクラス")]
    [SerializeField]
    private ScoreTextController _scoreTextController;

    [Tooltip("Resultのパネルを制御するクラス")]
    [SerializeField]
    private ResultController _result;
    public ResultController Result => _result; //TODO:プロパティの存在を忘れてガッツリ相互参照が出来てるので、修正する


    /// <summary>
    /// Scoreの値を管理するクラス
    /// </summary>
    private ScoreController _scoreController;

    public HelpController Help => _help;

    private PlayerController _player;

    private readonly Speed _speed = new (SpeedType.UI);

    /// <summary>
    /// 入力を受け取るインターフェース
    /// </summary>
    private IInputProvider _input;

    public void Initialize(
        PlayerController player, 
        IInputProvider input, 
        SpeedController speedController,
        ScoreController scoreController)
    {
        _player = player;
        _input = input;
        _scoreController = scoreController;

        _scoreTextController.Initialize();
        _crossHair.Initialize(_player.Shooter.ShootInterval);
        _help.Initialized(speedController);
        _result.Initialize();

        //射撃時に実行されるイベントに登録する
        _player.Shooter.OnBulletShoot += _crossHair.RotateCrossHair;

        _speed.SpeedRp
            .Subscribe(TweenTimeChange)
            .AddTo(gameObject);

        _scoreController.Score
            .Skip(1)
            .Subscribe(_scoreTextController.SetScoreText)
            .AddTo(gameObject);
    }

    private void TweenTimeChange(float speed)
    {
        _crossHair.TweenTimeChange(speed);
    }

    public void ManualUpdate(InGameState gameState)
    {
        if (_input.GetEscape() && gameState != InGameState.Finish)
        {
            if (gameState != InGameState.Pause)
            {
                _help.OpenHelp(gameState);
            }
            else
            {
                _help.CloseHelp(gameState);
            }
        }
    }

    private void OnDisable()
    {
        _player.Shooter.OnBulletShoot -= _crossHair.RotateCrossHair;
    }
}
