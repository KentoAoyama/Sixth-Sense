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

    private InGameController _gameController;

    private readonly Speed _speed = new (SpeedType.UI);

    /// <summary>
    /// 入力を受け取るインターフェース
    /// </summary>
    private IInputProvider _input;

    public void Initialize(
        PlayerController player, 
        IInputProvider input, 
        InGameController gameController,
        SpeedController speedController,
        ScoreController scoreController)
    {
        _player = player;
        _input = input;
        _gameController = gameController;
        _scoreController = scoreController;

        _scoreTextController.Initialize();
        _crossHair.Initialize(_player.Shooter.ShootInterval);
        _help.Initialized(gameController, speedController);
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

    public void ManualUpdate()
    {
        if (_input.GetEscape() && _gameController.GameState != InGameState.Finish)
        {
            if (_gameController.GameState != InGameState.Pause)
            {
                _help.OpenHelp();
            }
            else
            {
                _help.CloseHelp();
            }
        }
    }

    private void OnDisable()
    {
        _player.Shooter.OnBulletShoot -= _crossHair.RotateCrossHair;
    }
}
