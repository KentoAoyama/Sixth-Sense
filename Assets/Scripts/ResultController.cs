using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using DG.Tweening;

public class ResultController : MonoBehaviour
{
    [SerializeField]
    private GameObject _resultObject;

    [SerializeField]
    private Button _restartButton;
    public Button RestartButton => _restartButton;

    [SerializeField]
    private Button _mainMenuButton;
    public Button MainMenuButton => _mainMenuButton;

    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private CinemachineVirtualCamera _cinemachine;

    private CinemachinePOV _pov;

    public void Initialize()
    {
        _pov = _cinemachine.GetCinemachineComponent<CinemachinePOV>();

        _resultObject.transform.localScale = new(0f, 0f, 0f);
        _scoreText.DOFade(0f, 0f);
    }

    public void OpenResult(float score)
    {
        _scoreText.text = score.ToString();
        _pov.m_VerticalAxis.m_MaxSpeed = 0f;
        _pov.m_HorizontalAxis.m_MaxSpeed = 0f;

        var sequence = DOTween.Sequence();
        sequence
            .Insert(0f, _resultObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f))
            .Insert(0.5f, _scoreText.DOFade(1f, 0.3f));
        
    }
}
