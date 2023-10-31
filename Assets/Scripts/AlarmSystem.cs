using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _speed;
    [SerializeField] private float _minVolume;
    [SerializeField] private float _maxVolume;

    private void OnTriggerEnter(Collider otherCollider)
    {
        StartCoroutine(ChangeVolume(_speed, _maxVolume));
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        StartCoroutine(ChangeVolume(_speed * -1, _minVolume));
    }

    private IEnumerator ChangeVolume(float speed, float targetVolume)
    {
        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume += speed * Time.deltaTime;

            yield return null;
        }
    }
}
