using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using Cinemachine;
using DG.Tweening;

public class HelpController : MonoBehaviour
{
    [SerializeField]
    private GameObject _helpObject;

    [SerializeField]
    private Button _restartButton;
    public Button RestartButton => _restartButton;

    [SerializeField]
    private Button _mainMenuButton;
    public Button MainMenuButton => _mainMenuButton;

    [SerializeField]
    private Button _closeButton;

    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private CinemachineVirtualCamera _cinemachine;

    private CinemachinePOV _pov;

    private InGameController _gameController;

    private SpeedController _speedController;

    public void Initialized(InGameController gameController, SpeedController speedController)
    {
        _gameController = gameController;
        _speedController = speedController;

        _pov = _cinemachine.GetCinemachineComponent<CinemachinePOV>();

        _slider.minValue = 0f;
        _slider.maxValue = 2f;
        ChangeSensitivity(_slider.value);

        _closeButton.OnPointerClickAsObservable()
            .Subscribe(_ => CloseHelp())
            .AddTo(gameObject);

        _helpObject.transform.localScale = new(0f, 0f, 0f);
    }

    public void OpenHelp()
    {
        _speedController.Pause();
        _pov.m_VerticalAxis.m_MaxSpeed = 0f;
        _pov.m_HorizontalAxis.m_MaxSpeed = 0f;
        _helpObject.transform
            .DOScale(new Vector3(1f, 1f, 1f), 0.1f)
            .OnComplete(() => _gameController.GameState = InGameState.Pause);
    }

    public void CloseHelp()
    {
        _speedController.Resume();

        _helpObject.transform
            .DOScale(new Vector3(0f, 0f, 0f), 0.1f)
            .OnComplete(() => 
            { 
                _gameController.GameState = InGameState.InGame;
                ChangeSensitivity(_slider.value);
            });
    }

    private void ChangeSensitivity(float value)
    {
        _pov.m_VerticalAxis.m_MaxSpeed = value * 300f;
        _pov.m_HorizontalAxis.m_MaxSpeed = value * 300f;
    }
}