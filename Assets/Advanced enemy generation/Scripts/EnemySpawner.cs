using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnTime;

    private EnemySpawnpoint[] _spawnpoints;
    private WaitForSeconds _waitTime;

    private void Start()
    {
        _waitTime = new WaitForSeconds(_spawnTime);
        _spawnpoints = new EnemySpawnpoint[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _spawnpoints[i] = GetComponentsInChildren<EnemySpawnpoint>()[i];
        }

        StartCoroutine(StartSpawning());
    }

    private IEnumerator StartSpawning()
    {
        while (true)
        {
            _spawnpoints[Random.Range(0, _spawnpoints.Length)].SummonEnemy();

            yield return _waitTime;
        }
    }
}
