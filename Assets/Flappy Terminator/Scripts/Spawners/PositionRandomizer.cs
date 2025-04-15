using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class PositionRandomizer : MonoBehaviour
{
    private ObjectSpawner _spawner;
    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _spawner = GetComponent<ObjectSpawner>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        _spawner.WillSpawnObject += RandomizePosition;
    }

    private void OnDisable()
    {
        _spawner.WillSpawnObject -= RandomizePosition;
    }

    private void RandomizePosition(SelfReturner obj)
    {
        Vector2 lowestPosition = _boxCollider.bounds.min;
        Vector2 highestPosition = _boxCollider.bounds.max;

        obj.transform.position = new Vector3(Random.Range(lowestPosition.x, highestPosition.x),
                                             Random.Range(lowestPosition.y, highestPosition.y));
    }
}
