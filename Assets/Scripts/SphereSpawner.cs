using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    [SerializeField] private Transform _sphereTemplate;
    [SerializeField] private float _spawnTime;
    [SerializeField] private Quaternion _rotation;

    private Transform[] _spawnpoints;
    private WaitForSeconds _waitTime;

    private void Start()
    {
        _waitTime = new WaitForSeconds(_spawnTime);
        _spawnpoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _spawnpoints[i] = transform.GetChild(i);
        }

        StartCoroutine(StartSpawning());
    }

    private IEnumerator StartSpawning()
    {
        while (true)
        {
            Instantiate(_sphereTemplate, _spawnpoints[Random.Range(0, _spawnpoints.Length)].position, _rotation);
            
            yield return _waitTime;
        }
    }
}
