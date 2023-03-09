using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Pool;

public enum SoundEffectType
{
    Normal,
    Danger1,
    Danger2
}

public class SoundEffect : MonoBehaviour
{
    [Header("通常のエフェクト（青）のインターバル")]
    [SerializeField]
    private float _startIntervalN = 0.5f;

    [SerializeField]
    private float _finishIntervalN = 3f;

    [Header("通常のエフェクト（赤）のインターバル")]
    [SerializeField]
    private float _startIntervalD = 0.5f;

    [SerializeField]
    private float _finishIntervalD = 3f;

    [Header("共通して使う設定")]
    [SerializeField]
    private MeshRenderer _meshRenderer;

    [SerializeField]
    private Material _normalMaterial;

    [SerializeField]
    private Material _dangerMaterial;

    [SerializeField]
    private AudioSource _danger1Audio;

    [SerializeField]
    private AudioSource _danger2Audio;

    private IObjectPool<SoundEffect> _objectPool;

    private float _startInterval = 0f;
    private float _finishInterval = 0f;

    public void Initialize(IObjectPool<SoundEffect> objectPool, SoundEffectType type, Vector3 position)
    {
        if (_objectPool == null)_objectPool = objectPool;

        //音のタイプごとに値を設定する
        switch(type)
        {
            case SoundEffectType.Normal:
                _meshRenderer.material = _normalMaterial;
                gameObject.layer = 10;
                _startInterval = _startIntervalN;
                _finishInterval = _finishIntervalN;
                break;
            case SoundEffectType.Danger1:
                _meshRenderer.material = _dangerMaterial;
                gameObject.layer = 9;
                _startInterval = _startIntervalD;
                _finishInterval = _finishIntervalD;
                _danger1Audio.Play();

                break;
            case SoundEffectType.Danger2:
                _meshRenderer.material = _dangerMaterial;
                gameObject.layer = 9;
                _startInterval = _startIntervalD;
                _finishInterval = _finishIntervalD;
                _danger2Audio.Play();

                break;
        }

        transform.position = position;
        transform.localScale = new Vector3(0f, 0f, 0f);
        StartSound();
    }

    private void StartSound()
    {
        var sequence = DOTween.Sequence();
        sequence.Insert(0f, transform.DOScale(1f, _startInterval))
            .Insert(_startInterval, transform.DOScale(0f, _finishInterval))
            .Play()
            .OnComplete(() => _objectPool.Release(this));
    }

    private void Update()
    {
        //常にプレイヤーのほうを向くようにする
        transform.LookAt(Camera.main.transform.position);
    }
}
