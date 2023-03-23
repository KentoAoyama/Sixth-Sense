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
    [Tooltip("�v���C���[�̃N���X")]
    [SerializeField]
    private PlayerController _player;

    [Tooltip("�G�̐������s���N���X")]
    [SerializeField]
    private EnemyGenerator _enemyGenerator;

    [Tooltip("�Q�[�����̃X�s�[�h���Ǘ�����N���X")]
    [SerializeField]
    private SpeedController _speedController;

    [Tooltip("�I�u�W�F�N�g�v�[��")]
    [SerializeField]
    private ObjectPoolsController _objectPool;

    [Tooltip("GUI�̊Ǘ�������N���X")]
    [SerializeField]
    private GUIPresenter _gui;

    [Tooltip("Fade�̊Ǘ�������N���X")]
    [SerializeField]
    private FadeSystem _fade;

    private ScoreController _scoreController = new();

    /// <summary>
    /// ���͂��󂯎��C���^�[�t�F�[�X
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
        _player.Initialize(_objectPool, _speedController, _input);
        _enemyGenerator.Initialize(_player, _objectPool, _scoreController);
        _gui.Initialize(_player, _input, _speedController, _scoreController);

        //�v���C���[�̔�e���Ď�����
        _player.ObserveEveryValueChanged(h => _player.IsHit)
            .Skip(1)
            .Where(h => h)
            .Subscribe(_ => GameOver())
            .AddTo(gameObject);

        ButtonInit();
        CursorInit();

        //Fade���I���܂ŃQ�[�����n�߂Ȃ�
        await _fade.StartFadeIn(token);

        //�Q�[�����J�n
        _gameState = InGameState.InGame;
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;

        _gui.ManualUpdate(_gameState);

        //�Q�[�����łȂ��Ȃ���s���Ȃ�
        if (_gameState != InGameState.InGame) return;

        //if (Cursor.visible) CursorInit();
        _player.ManualUpdate(deltaTime);
        _enemyGenerator.ManualUpdate(deltaTime);
    }

    /// <summary>
    /// �{�^���̋@�\��ǉ�����
    /// </summary>
    private void ButtonInit()
    {
        _gui.Help.RestartButton.OnPointerClickAsObservable()
            .Subscribe(_ => Restart())
            .AddTo(gameObject);

        _gui.Help.MainMenuButton.OnPointerClickAsObservable()
            .Subscribe(_ => MainMenu())
            .AddTo(gameObject);

        _gui.Help.CloseButton.OnPointerClickAsObservable()
            .Subscribe(_ =>
            {
                _gui.Help.CloseHelp(_gameState);
                CursorInit();
            })
            .AddTo(gameObject);

        _gui.Result.RestartButton.OnPointerClickAsObservable()
            .Subscribe(_ => Restart())
            .AddTo(gameObject);

        _gui.Result.MainMenuButton.OnPointerClickAsObservable()
            .Subscribe(_ => MainMenu())
            .AddTo(gameObject);
    }

    /// <summary>
    /// �Q�[���I�[�o�[���ɌĂяo����鏈��
    /// </summary>
    private void GameOver()
    {
        Cursor.visible = true;
        _speedController.Pause();
        _gui.Result.OpenResult(_scoreController.Score.Value);
        _enemyGenerator.DestroyEnemy();
        _gameState = InGameState.Finish;
    }

    /// <summary>
    /// ���X�^�[�g���ɌĂяo����鏈�� TODO:�V�[�����ēǂݍ��݂����A���̃V�[���̂܂܂ŏ��������s��
    /// </summary>
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
