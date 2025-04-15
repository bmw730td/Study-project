using TMPro;
using UnityEngine;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private ScoreCounter _counter;
    
    private TMP_Text _scoreText;

    private void Awake()
    {
        _scoreText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _counter.ScoreChanged += ChangeScoreText;
    }

    private void OnDisable()
    {
        _counter.ScoreChanged -= ChangeScoreText;
    }

    private void ChangeScoreText(int score)
    {
        _scoreText.text = score.ToString();
    }
}
