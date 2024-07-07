using UnityEngine;

namespace RainOfCubes
{
    [RequireComponent(typeof(ObjectSpawner))]
    [RequireComponent(typeof(BoxCollider))]

    public class PositionRandomizer : MonoBehaviour
    {
        private ObjectSpawner _spawner;
        private BoxCollider _boxCollider;

        private void Awake()
        {
            _spawner = GetComponent<ObjectSpawner>();
            _boxCollider = GetComponent<BoxCollider>();
        }

        private void OnEnable()
        {
            _spawner.WillSpawnObject += RandomizePosition;
        }

        private void OnDisable()
        {
            _spawner.WillSpawnObject -= RandomizePosition;
        }

        private void RandomizePosition(GameObject obj)
        {
            Vector3 lowestPosition = transform.position + _boxCollider.center - Vector3.Scale(_boxCollider.size, transform.localScale) * 0.5f;
            Vector3 highestPosition = transform.position + _boxCollider.center + Vector3.Scale(_boxCollider.size, transform.localScale) * 0.5f;

            obj.transform.position = new Vector3(Random.Range(lowestPosition.x, highestPosition.x),
                                                 Random.Range(lowestPosition.y, highestPosition.y),
                                                 Random.Range(lowestPosition.z, highestPosition.z));
        }
    }
}
