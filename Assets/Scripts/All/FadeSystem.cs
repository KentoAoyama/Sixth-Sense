using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Threading;
using Cysharp.Threading.Tasks;

public class FadeSystem : MonoBehaviour
{
    [Tooltip("フェードに使うImage")]
    [SerializeField] Image _fadeImage;

    [Tooltip("フェードにかける時間")]
    [SerializeField] float _fadeTime = 3f;

    public async UniTask StartFadeIn(CancellationToken token)//フェードイン関数
    {
        await _fadeImage
            .DOFade(endValue: 0f, duration: _fadeTime)
            .OnComplete(() => _fadeImage.gameObject.SetActive(false))
            .ToUniTask(cancellationToken: token);
    }

    public void StartFadeOut(string scene)//フェードアウト関数
    {
        _fadeImage.gameObject.SetActive(true);
        _fadeImage.DOFade(endValue: 1f, duration: _fadeTime).OnComplete(() => SceneManager.LoadScene(scene));
    }


    public void Exit()
    {
        Application.Quit();
    }


    void OnDisable()
    {
        DOTween.KillAll();
    }
}