using System;
using UnityEngine;
using UnityEngine.Pool;

namespace RainOfCubes
{
    public abstract class ObjectSpawner : MonoBehaviour
    {
        private const int PoolDefaultCapacity = 50;
        private const int PoolMaxSize = 1000;
        
        [SerializeField] private SelfReleaser _objectPrefab;

        private ObjectPool<SelfReleaser> _pool;

        private Vector3 _spawnPos;

        public event Action<GameObject> WillAddObjectToPool;
        public event Action<GameObject> WillSpawnObject;
        public event Action ObjectSpawned;

        public int ObjectsInPool
        {
            get
            {
                if (_pool == null)
                {
                    return 0;
                }
                else
                {
                    return _pool.CountAll;
                }
            }
        }
        public int ObjectsSpawned { get; private set; }

        private void Awake()
        {
            _pool = new ObjectPool<SelfReleaser>(
                createFunc: () => InitializeObject(),
                actionOnGet: (obj) => ResetObj(obj),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: true,
                defaultCapacity: PoolDefaultCapacity,
                maxSize: PoolMaxSize);
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

        private SelfReleaser InitializeObject()
        {
            SelfReleaser newObject = Instantiate(_objectPrefab);

            newObject.SetPool(_pool);
            WillAddObjectToPool?.Invoke(newObject.gameObject);

            return newObject;
        }

        private void ResetObj(SelfReleaser obj)
        {
            obj.transform.position = _spawnPos;
            obj.transform.rotation = _objectPrefab.transform.rotation;

            if (obj.TryGetComponent(out Rigidbody objRigidbody))
            {
                objRigidbody.velocity = Vector3.zero;
                objRigidbody.angularVelocity = Vector3.zero;
            }

            WillSpawnObject?.Invoke(obj.gameObject);
            obj.gameObject.SetActive(true);
            ObjectsSpawned++;
            ObjectSpawned?.Invoke();
        }
    }
}
