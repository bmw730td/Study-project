using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangingVolume : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _speed;
    [SerializeField] private float _minVolume;
    [SerializeField] private float _maxVolume;

    private Coroutine _changeVolume;

    private void Start()
    {
        _audioSource.volume = _minVolume;
    }

    private void OnValidate()
    {
        if (_minVolume < 0f)
        {
            _minVolume = 0f;
        }
        else if (_minVolume > 1f)
        {
            _minVolume = 1f;
        }

        if (_maxVolume < 0f)
        {
            _maxVolume = 0f;
        }
        else if (_maxVolume > 1f)
        {
            _maxVolume = 1f;
        }

        if (_minVolume > _maxVolume)
        {
            _minVolume = _maxVolume;
        }
    }

    public void IncreaseVolume()
    {
        StartChangingVolume(_maxVolume);
    }

    public void DecreaseVolume()
    {
        StartChangingVolume(_minVolume);
    }

    private void StartChangingVolume(float targetVolume)
    {
        if (_changeVolume != null)
        {
            StopCoroutine(_changeVolume);
        }

        _changeVolume = StartCoroutine(ChangeVolume(targetVolume));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _speed * Time.deltaTime);

            yield return null;
        }
    }
}