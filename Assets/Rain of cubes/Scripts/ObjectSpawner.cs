using UnityEngine;
using UnityEngine.Pool;

namespace RainOfCubes
{
    [RequireComponent(typeof(BoxCollider))]
    
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _objectPrefab;
        [SerializeField, Min(0f)] private float _timeDelay;

        private ObjectPool<GameObject> _pool;

        private BoxCollider _boxCollider;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
        }

        private void Start()
        {
            _pool = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(_objectPrefab),
                actionOnGet: (obj) => ResetObj(obj),
                actionOnRelease: (obj) => obj.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: true,
                defaultCapacity: 50,
                maxSize: 1000);

            InvokeRepeating(nameof(GetObj), 0f, _timeDelay);
        }

        private void ResetObj(GameObject obj)
        {
            Vector3 lowestPosition = transform.position + _boxCollider.center - Vector3.Scale(_boxCollider.size, transform.localScale) * 0.5f;
            Vector3 highestPosition = transform.position + _boxCollider.center + Vector3.Scale(_boxCollider.size, transform.localScale) * 0.5f;

            obj.transform.position = new Vector3(Random.Range(lowestPosition.x, highestPosition.x),
                                                 Random.Range(lowestPosition.y, highestPosition.y),
                                                 Random.Range(lowestPosition.z, highestPosition.z));
            obj.transform.rotation = _objectPrefab.transform.rotation;

            if (obj.TryGetComponent(out Rigidbody objRigidbody))
            {
                objRigidbody.velocity = Vector3.zero;
                objRigidbody.angularVelocity = Vector3.zero;
            }

            obj.SetActive(true);
        }

        private void GetObj()
        {
            GameObject newGameObject = _pool.Get();

            if (newGameObject.TryGetComponent(out SelfReleaser selfReleaser))
                selfReleaser.SetPool(_pool);
        }
    }
}
