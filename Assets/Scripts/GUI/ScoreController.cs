using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ScoreController
{
    private ReactiveProperty<int> _score;
    public IReadOnlyReactiveProperty<int> Score => _score;

    public ScoreController()
    {
        _score = new(0);
    }

    public void AddScore()
    {
        _score.Value++;
    }
}
