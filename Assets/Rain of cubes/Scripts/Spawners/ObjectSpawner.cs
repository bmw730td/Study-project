using System;
using UnityEngine;
using UnityEngine.Pool;

namespace RainOfCubes
{
    public abstract class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _objectPrefab;

        private ObjectPool<GameObject> _pool;

        private Vector3 _spawnPos;

        public event Action<GameObject> WillAddToPool;
        public event Action<GameObject> WillSpawnObject;

        private void Awake()
        {
            _pool = new ObjectPool<GameObject>(
                createFunc: () => InitializeObject(),
                actionOnGet: (obj) => ResetObj(obj),
                actionOnRelease: (obj) => obj.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: true,
                defaultCapacity: 50,
                maxSize: 1000);
        }

        protected void SpawnObj()
        {
            SpawnObj(_objectPrefab.transform.position);
        }

        protected void SpawnObj(Vector3 startingPos)
        {
            _spawnPos = startingPos;
            _pool.Get();
        }

        private GameObject InitializeObject()
        {
            GameObject newObject = Instantiate(_objectPrefab);

            if (newObject.TryGetComponent(out SelfReleaser selfReleaser))
                selfReleaser.SetPool(_pool);

            WillAddToPool?.Invoke(newObject);

            return newObject;
        }

        private void ResetObj(GameObject obj)
        {
            obj.transform.position = _spawnPos;
            obj.transform.rotation = _objectPrefab.transform.rotation;

            if (obj.TryGetComponent(out Rigidbody objRigidbody))
            {
                objRigidbody.velocity = Vector3.zero;
                objRigidbody.angularVelocity = Vector3.zero;
            }

            WillSpawnObject?.Invoke(obj);
            obj.SetActive(true);
        }
    }
}
