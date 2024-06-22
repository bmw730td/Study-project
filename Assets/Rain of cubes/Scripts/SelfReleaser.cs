using UnityEngine;
using UnityEngine.Pool;

namespace RainOfCubes
{
    [RequireComponent(typeof(DisposableActivator))]
    
    public class SelfReleaser : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _minDelay;
        [SerializeField, Min(0f)] private float _maxDelay;
        
        private ObjectPool<GameObject> _pool;
        private DisposableActivator _disposableActivator;

        private void OnValidate()
        {
            if (_minDelay > _maxDelay)
                _minDelay = _maxDelay;
        }

        private void Awake()
        {
            _disposableActivator = GetComponent<DisposableActivator>();
        }

        private void OnEnable()
        {
            _disposableActivator.TargetReached += StartSelfRelease;
        }

        private void OnDisable()
        {
            _disposableActivator.TargetReached -= StartSelfRelease;
        }

        public void SetPool(ObjectPool<GameObject> objectPool)
        {
            _pool = objectPool;
        }

        private void StartSelfRelease()
        {
            float delay = Random.Range(_minDelay, _maxDelay);

            Invoke(nameof(ReleaseSelf), delay);
        }

        private void ReleaseSelf()
        {
            _pool.Release(gameObject);
        }
    }
}
