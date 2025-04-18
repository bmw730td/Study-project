using System.Collections;
using UnityEngine;

public class IntervalSpawner : ObjectSpawner
{
    [SerializeField, Min(0f)] private float _interval;

    private WaitForSeconds _intervalInSeconds;
    private Coroutine _spawningCoroutine;

    private void OnEnable()
    {
       StartSpawning();
    }

    private void OnDisable()
    {
        StopCoroutine(_spawningCoroutine);
    }

    private void Start()
    {
        _intervalInSeconds = new WaitForSeconds(_interval);
    }

    public override void Reset()
    {
        ResetPool();
        StartSpawning();
    }

    private IEnumerator SpawnObjects()
    {
        while (enabled)
        {
            yield return _intervalInSeconds;

            SpawnObject();
        }
    }

    private void StartSpawning()
    {
        if (enabled)
        {
            if (_spawningCoroutine != null)
                StopCoroutine(_spawningCoroutine);

            _spawningCoroutine = StartCoroutine(SpawnObjects());
        }
    }
}
