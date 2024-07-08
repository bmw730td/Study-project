using System.Collections;
using UnityEngine;

namespace RainOfCubes
{
    [RequireComponent(typeof(BoxCollider))]
    
    public class IntervalSpawner : ObjectSpawner
    {
        [SerializeField, Min(0f)] private float _timeInterval;

        private WaitForSeconds _timeIntervalInSeconds;

        private void OnValidate()
        {
            _timeIntervalInSeconds = new WaitForSeconds(_timeInterval);
        }

        private void Start()
        {
            StartCoroutine(SpawnWithInterval());
        }

        private IEnumerator SpawnWithInterval()
        {
            while (true)
            {
                yield return _timeIntervalInSeconds;

                SpawnObj();
            }
        }
    }
}
