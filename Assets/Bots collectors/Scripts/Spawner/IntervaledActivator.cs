using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ObjectSpawner))]

public class IntervaledActivator : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _interval;
    
    private ObjectSpawner _spawner;
    private WaitForSeconds _intervalInSeconds;
    private Coroutine _spawningCoroutine;

    private void Awake()
    {
        _spawner = GetComponent<ObjectSpawner>();

        _intervalInSeconds = new WaitForSeconds(_interval);
    }

    private void OnEnable()
    {
        if (_spawningCoroutine != null)
            StopCoroutine(_spawningCoroutine);
        
        _spawningCoroutine = StartCoroutine(SpawnObjects());
    }

    private void OnDisable()
    {
        if (_spawningCoroutine != null)
            StopCoroutine(_spawningCoroutine);
    }

    private IEnumerator SpawnObjects()
    {
        while (enabled)
        {
            yield return _intervalInSeconds;

            _spawner.SpawnObject();
        }
    }
}
