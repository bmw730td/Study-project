using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]

public class ScoreView : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private TextMeshProUGUI _scoreText;
    
    private void OnEnable()
    {
        if (_scoreText != null)
        {
            UpdateText();
            _score.ScoreChanged += UpdateText;
        }
    }

    private void OnDisable()
    {
        if (_scoreText != null)
        {
            _score.ScoreChanged -= UpdateText;
        }
    }

    private void UpdateText()
    {
        _scoreText.text = _score.Value.ToString();
    }
}
