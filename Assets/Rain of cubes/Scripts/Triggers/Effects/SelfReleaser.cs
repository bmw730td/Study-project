using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace RainOfCubes
{
    [RequireComponent(typeof(OneShotTrigger))]
    
    public class SelfReleaser : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _minDelay;
        [SerializeField, Min(0f)] private float _maxDelay;
        
        private OneShotTrigger _trigger;
        private ObjectPool<SelfReleaser> _pool;

        private float _delayCounter;

        public event Action DelayCounterChanged;
        public event Action AboutToRealease;

        public float BaseDelay { get; private set; }
        public float DelayCounter => _delayCounter;

        private void OnValidate()
        {
            if (_minDelay > _maxDelay)
                _minDelay = _maxDelay;
        }

        private void Awake()
        {
            _trigger = GetComponent<OneShotTrigger>();
        }

        private void OnEnable()
        {
            _trigger.Activated += StartSelfRelease;
        }

        private void OnDisable()
        {
            _trigger.Activated -= StartSelfRelease;
        }

        public void SetPool(ObjectPool<SelfReleaser> objectPool)
        {
            _pool = objectPool;
        }

        private void StartSelfRelease()
        {
            if (_delayCounter == 0f)
                StartCoroutine(ReleaseSelf());
        }

        private IEnumerator ReleaseSelf()
        {
            BaseDelay = UnityEngine.Random.Range(_minDelay, _maxDelay);
            _delayCounter = BaseDelay;
            DelayCounterChanged?.Invoke();

            while (_delayCounter > 0f)
            {
                yield return null;

                _delayCounter -= Time.deltaTime;

                if (_delayCounter < 0f)
                    _delayCounter = 0f;

                DelayCounterChanged?.Invoke();
            }

            AboutToRealease?.Invoke();
            _pool.Release(this);
        }
    }
}
