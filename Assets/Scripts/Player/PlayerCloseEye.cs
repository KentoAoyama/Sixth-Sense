using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

[Serializable]
public class PlayerCloseEye
{
    [Tooltip("ãáÙ‚Æ‚È‚éImage")]
    [SerializeField]
    private Image _upEyelids;

    [Tooltip("‰ºáÙ‚Æ‚È‚éImage")]
    [SerializeField]
    private Image _downEyelids;

    [Tooltip("áÙ‚ÌŠJ•Â‚É‚©‚¯‚éŽžŠÔ")]
    [SerializeField]
    private float _duration = 0.5f;

    private PlayerController _player;

    private const float CLOSE_HEIGHT_VALUE = 120f;
    private const float OPEN_HEIGHT_VALUE = 320f;

    public void Initialize(PlayerController player)
    {
        _player = player;

        player.ObserveEveryValueChanged(c => player.Input.GetCloseEye())
            .Skip(1)
            .ThrottleFirst(TimeSpan.FromSeconds(Mathf.Max(0, _duration - 0.1f)))
            .Subscribe(CloseEye)
            .AddTo(player.gameObject);
    }

    private void CloseEye(bool isClose)
    {
        if (isClose)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    private void Close()
    {
        Debug.Log("Close");
        EyelidTween(CLOSE_HEIGHT_VALUE).OnComplete(Check);
    }

    private void Open()
    {
        Debug.Log("Open");
        EyelidTween(OPEN_HEIGHT_VALUE).OnComplete(Check);
    }

    private void Check()
    {
        if (_player.Input.GetCloseEye())
        {
            EyelidTween(CLOSE_HEIGHT_VALUE);
        }
        else
        {
            EyelidTween(OPEN_HEIGHT_VALUE);
        }
    }

    private Sequence EyelidTween(float value)
    {
        var sequence = DOTween.Sequence();
        return sequence
            .Insert(0f, _upEyelids.rectTransform.DOLocalMoveY(value, _duration))
            .Insert(0f, _downEyelids.rectTransform.DOLocalMoveY(-value, _duration));
    }
}
