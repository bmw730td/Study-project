using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class Counter : MonoBehaviour
{
    [SerializeField] private Button _button;

    [SerializeField] private Score _score;
    [SerializeField, Min(0)] private float _interval;
    [SerializeField] private int _amount;

    private WaitForSeconds _intervalInSeconds;
    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _intervalInSeconds = new WaitForSeconds(_interval);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(SwitchCounter);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(SwitchCounter);
    }

    private void OnValidate()
    {
        _intervalInSeconds = new WaitForSeconds(_interval);
    }

    private void SwitchCounter()
    {
        if (_score != null)
        {
            if (_currentCoroutine == null)
            {
                _currentCoroutine = StartCoroutine(IncreaseScore());
            }
            else
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }
        }
    }

    private IEnumerator IncreaseScore()
    {
        while (_score != null)
        {
            _score.Value += _amount;
            _score.ScoreChanged.Invoke();

            yield return _intervalInSeconds;
        }
    }
}
