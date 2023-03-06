using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Rendering;
using DG.Tweening;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private FadeSystem _fade;

    [SerializeField]
    private Graphic _titleLogo;

    [SerializeField]
    private Button _clickToStart;

    [SerializeField]
    private Text _clickText;

    [SerializeField]
    private Volume _volume;

    private void Start()
    {
        ShowStart(default).Forget();
    }

    private async UniTask ShowStart(CancellationToken token)
    {
        ButtonInit();

        _volume.weight = 0f;
        _ = DOTween.To(
            () => _volume.weight,
            (x) => _volume.weight = x,
            1f,
            2f);

        await _fade.StartFadeIn(token);

        _clickToStart.enabled = true;

        var sequence = DOTween.Sequence();
        await sequence
            .Insert(0f, _clickText.DOFade(1f, 1f))
            .Insert(1f, _clickText.DOFade(0f, 1f))
            .SetLoops(-1)
            .Play()
            .ToUniTask(cancellationToken: token);
    }

    private void ButtonInit()
    {
        _clickToStart.OnPointerClickAsObservable()
            .Subscribe(_ => _fade.StartFadeOut("InGameScene"))
            .AddTo(gameObject);

        _clickToStart.enabled = false;
        _clickText.DOFade(0f, 0f);
    }
}
