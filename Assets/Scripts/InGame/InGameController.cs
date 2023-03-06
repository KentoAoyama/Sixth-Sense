using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;
using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;

public class InGameController : MonoBehaviour
{
    [Tooltip("プレイヤーのクラス")]
    [SerializeField]
    private PlayerController _player;

    [Tooltip("敵の生成を行うクラス")]
    [SerializeField]
    private EnemyGenerator _enemyGenerator;

    [Tooltip("ゲーム内のスピードを管理するクラス")]
    [SerializeField]
    private SpeedController _speedController;

    [Tooltip("弾のオブジェクトプール")]
    [SerializeField]
    private NormalBulletPool _bulletPool;

    [Tooltip("敵のオブジェクトプール")]
    [SerializeField]
    private EnemyPool _enemyPool;

    [Tooltip("SoundEffectのオブジェクトプール")]
    [SerializeField]
    private SoundEffectPool _soundEffectPool;

    [Tooltip("GUIの管理をするクラス")]
    [SerializeField]
    private GUIPresenter _gui;

    [Tooltip("Fadeの管理をするクラス")]
    [SerializeField]
    private FadeSystem _fade;

    private ScoreController _scoreController = new();

    /// <summary>
    /// 入力を受け取るインターフェース
    /// </summary>
    [Inject]
    private readonly IInputProvider _input;

    private InGameState _gameState = InGameState.WaitStart;
    public InGameState GameState { get => _gameState; set => _gameState = value; }

    private void Start()
    {
        ShowStart(default).Forget();
    }

    private async UniTask ShowStart(CancellationToken token)
    {
        _player.Initialize(_bulletPool, _soundEffectPool, _speedController, _input);
        _enemyGenerator.Initialize(_player, _bulletPool, _enemyPool, _soundEffectPool, _scoreController);
        _gui.Initialize(_player, _input, this, _speedController, _scoreController);

        //プレイヤーの被弾を監視する
        _player.ObserveEveryValueChanged(h => _player.IsHit)
            .Skip(1)
            .Where(h => h == true)
            .Subscribe(_ => GameOver())
            .AddTo(gameObject);

        ButtonInit();
        CursorInit();

        await _fade.StartFadeIn(token);

        Debug.Log("Game開始");
        _gameState = InGameState.InGame;
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;     

        _gui.ManualUpdate();

        //ゲーム中でないなら実行しない
        if (_gameState != InGameState.InGame) return;

        if (Cursor.visible) CursorInit();
        _player.ManualUpdate(deltaTime);
        _enemyGenerator.ManualUpdate(deltaTime);
    }

    private void ButtonInit()
    {
        _gui.Help.RestartButton.OnPointerClickAsObservable()
            .Subscribe(_ => Restart())
            .AddTo(gameObject);

        _gui.Help.MainMenuButton.OnPointerClickAsObservable()
            .Subscribe(_ => MainMenu())
            .AddTo(gameObject);

        _gui.Result.RestartButton.OnPointerClickAsObservable()
            .Subscribe(_ => Restart())
            .AddTo(gameObject);

        _gui.Result.MainMenuButton.OnPointerClickAsObservable()
            .Subscribe(_ => MainMenu())
            .AddTo(gameObject);
    }

    private void GameOver()
    {
        Cursor.visible = false;
        _speedController.Pause();
        _gui.Result.OpenResult(_scoreController.Score.Value);
        _enemyGenerator.DestroyEnemy();
        _gameState = InGameState.Finish;
    }

    private void Restart()
    {
        _fade.StartFadeOut("InGameScene");
    }

    private void MainMenu()
    {
        _fade.StartFadeOut("MainMenu");
    }

    private void CursorInit()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
}
