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
    [Tooltip("上瞼となるImage")]
    [SerializeField]
    private Image _upEyelids;

    [Tooltip("下瞼となるImage")]
    [SerializeField]
    private Image _downEyelids;

    [Tooltip("瞼の開閉にかける時間")]
    [SerializeField]
    private float _duration = 0.5f;

    [Tooltip("頭にある周りのものを見えなくするためのオブジェクト")]
    [SerializeField]
    private GameObject _eyelidObject;

    [Tooltip("目を閉じた際に変化するスピード")]
    [SerializeField, Range(0f, 1f)]
    private float _changeSpeed = 0.5f;

    private PlayerController _player;

    private SpeedController _speedController;

    private const float CLOSE_HEIGHT_VALUE = 120f;
    private const float OPEN_HEIGHT_VALUE = 320f;

    public void Initialize(PlayerController player, SpeedController speedController)
    {
        _player = player;
        _speedController = speedController;

        _eyelidObject.SetActive(false);

        player.ObserveEveryValueChanged(c => player.Input.GetCloseEye())
            .Skip(1)
            .ThrottleFirst(TimeSpan.FromSeconds(Mathf.Max(0, _duration - 0.05f)))
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
        EyelidTween(CLOSE_HEIGHT_VALUE).OnComplete(Check);
    }

    private void Open()
    {
        CloseEyelidActive(false);
        EyelidTween(OPEN_HEIGHT_VALUE).OnComplete(Check);
    }

    /// <summary>
    /// 目の開閉処理後に入力の変化があるか改めてチェック
    /// </summary>
    private void Check()
    {
        if (_player.Input.GetCloseEye())
        {
            EyelidTween(CLOSE_HEIGHT_VALUE);
        }
        else
        {
            CloseEyelidActive(false);
            EyelidTween(OPEN_HEIGHT_VALUE);
        }
    }

    private Sequence EyelidTween(float value)
    {
        var sequence = DOTween.Sequence();
        //瞼を動かして、最後に閉じ切っているか判定する
        return sequence
            .Insert(0f, _upEyelids.rectTransform.DOLocalMoveY(value, _duration))
            .Insert(0f, _downEyelids.rectTransform.DOLocalMoveY(-value, _duration))
            .OnComplete(() => 
            CloseEyelidActive(_upEyelids.rectTransform.localPosition.y == CLOSE_HEIGHT_VALUE));
    }

    /// <summary>
    /// 瞼の開閉に関する処理Trueだと目が開いていて、Falseだと目が完全にしまっている
    /// </summary>
    private void CloseEyelidActive(bool isClose)
    {
        _upEyelids.gameObject.SetActive(!isClose);
        _downEyelids.gameObject.SetActive(!isClose);
        _eyelidObject.SetActive(isClose);

        if (isClose)
        {
            Camera.main.cullingMask = -1;
            _speedController.SpeedChange(_changeSpeed, ChangeSpeedType.NonPlayer);
        }
        else
        {
            Camera.main.cullingMask &= ~(1 << 9);
            Camera.main.cullingMask &= ~(1 << 10);
            _speedController.SpeedChange(1f);
        }
    }
}
