using UnityEngine;

[RequireComponent(typeof(ObjectSpawner))]

public class SpawnerWarmUper : MonoBehaviour
{
    [SerializeField, Min(0)] private int _amountToSpawnOnStart;

    private ObjectSpawner _spawner;

    private void Awake()
    {
        _spawner = GetComponent<ObjectSpawner>();
    }

    private void Start()
    {
        for (int i = 0; i < _amountToSpawnOnStart; i++)
        {
            _spawner.SpawnObject();
        }
    }
}
