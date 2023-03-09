using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreTextController : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    [SerializeField]
    private AudioSource _audio;

    public void Initialize()
    {
        _text.DOFade(0f, 0f);
    }

    public void SetScoreText(int score)
    {
        _text.text = score.ToString();
        _audio.Play();
        var sequence = DOTween.Sequence();
        sequence.Insert(0f, _text.DOFade(1f, 0.1f))
            .Insert(0.5f, _text.DOFade(0f, 0.1f))
            .Play();
    }
}
