using UnityEngine;

[RequireComponent(typeof(ObjectSpawner))]

public class PositionRandomizer : MonoBehaviour
{
    [SerializeField] private BoxCollider[] _spawnAreas;

    private ObjectSpawner _spawner;

    private void Awake()
    {
        _spawner = GetComponent<ObjectSpawner>();
    }

    private void OnEnable()
    {
        _spawner.WillSpawnObject += RandomizePosition;
    }

    private void OnDisable()
    {
        _spawner.WillSpawnObject -= RandomizePosition;
    }

    private void RandomizePosition(ReturnAnnouncer obj)
    {
        int randomIndex = Random.Range(0, _spawnAreas.Length);
        Vector3 lowestPosition = _spawnAreas[randomIndex].bounds.min;
        Vector3 highestPosition = _spawnAreas[randomIndex].bounds.max;

        obj.transform.position = new Vector3(Random.Range(lowestPosition.x, highestPosition.x),
                                             Random.Range(lowestPosition.y, highestPosition.y),
                                             Random.Range(lowestPosition.z, highestPosition.z));
    }
}
