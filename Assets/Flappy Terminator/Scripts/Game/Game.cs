using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private BirdCollisionProcessor _birdProcessor;
    [SerializeField] private Window _startScreen;
    [SerializeField] private Window _endScreen;

    [SerializeField] private Bird _bird;
    [SerializeField] private ObjectSpawner _probeSpawner;
    [SerializeField] private ScoreCounter _scoreCounter;

    private void OnEnable()
    {
        _startScreen.ButtonClicked += OnPlayButtonClick;
        _endScreen.ButtonClicked += OnRestartButtonClick;
        _birdProcessor.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _startScreen.ButtonClicked -= OnPlayButtonClick;
        _endScreen.ButtonClicked -= OnRestartButtonClick;
        _birdProcessor.GameOver -= OnGameOver;
    }

    private void Start()
    {
        Time.timeScale = 0;
        _startScreen.gameObject.SetActive(true);
        _endScreen.gameObject.SetActive(false);
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;
        _endScreen.gameObject.SetActive(true);
    }

    private void OnPlayButtonClick()
    {
        _startScreen.gameObject.SetActive(false);
        StartGame();
    }

    private void OnRestartButtonClick()
    {
        _endScreen.gameObject.SetActive(false);
        StartGame();
    }

    private void StartGame()
    {
        Time.timeScale = 1;

        _bird.Reset();
        _probeSpawner.Reset();
        _scoreCounter.Reset();
    }
}
