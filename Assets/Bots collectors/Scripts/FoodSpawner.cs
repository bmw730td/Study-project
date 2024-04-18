using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnInterval;
    [SerializeField] private List<Transform> _foodPrefabs; 

    private WaitForSeconds _interval;
    private Coroutine _spawningCoroutine;

    private void Start()
    {
        _spawningCoroutine = StartCoroutine(SpawnFood());
    }

    private void OnValidate()
    {
        _interval = new WaitForSeconds(_spawnInterval);
    }

    private IEnumerator SpawnFood()
    {
        while(true)
        {
            var newFood = Instantiate(_foodPrefabs[Random.Range(0, _foodPrefabs.Count)]);
            
            yield return _interval;
        }
    }
}
