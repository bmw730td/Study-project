using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int _score;

    public event Action<int> ScoreChanged;

    public int Score => _score;

    public void IncreaseScore()
    {
        _score++;
        ScoreChanged?.Invoke(Score);
    }

    public void Reset()
    {
        _score = 0;
        ScoreChanged?.Invoke(Score);
    }
}
