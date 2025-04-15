using System.Collections;
using UnityEngine;

public class IntervalSpawner : ObjectSpawner
{
    [SerializeField, Min(0f)] private float _interval;

    private WaitForSeconds _intervalInSeconds;
    private Coroutine _spawningCoroutine;

    private void OnValidate()
    {
        _intervalInSeconds = new WaitForSeconds(_interval);
    }

    private void OnEnable()
    {
       StartCoroutine();
    }

    private void OnDisable()
    {
        StopCoroutine(_spawningCoroutine);
    }

    private void Start()
    {
        GameStarted += StartCoroutine;
    }

    private IEnumerator SpawnObjects()
    {
        if (_intervalInSeconds == null)
            _intervalInSeconds = new WaitForSeconds(_interval);
        
        while (enabled)
        {
            yield return _intervalInSeconds;

            SpawnObject();
        }
    }

    private void StartCoroutine()
    {
        if (enabled)
        {
            if (_spawningCoroutine != null)
                StopCoroutine(_spawningCoroutine);

            _spawningCoroutine = StartCoroutine(SpawnObjects());
        }
    }
}
