using UnityEngine;

namespace RainOfCubes
{
    [RequireComponent(typeof(SelfReleaser))]

    public class ObjectLeaver : MonoBehaviour
    {
        private TargetableSpawner _spawner;
        private SelfReleaser _selfReleaser;

        private void Awake()
        {
            _selfReleaser = GetComponent<SelfReleaser>();
        }

        private void OnEnable()
        {
            _selfReleaser.AboutToRealease += SpawnObj;
        }

        private void OnDisable()
        {
            _selfReleaser.AboutToRealease -= SpawnObj;
        }

        public void SetSpawner(TargetableSpawner spawner)
        {
            _spawner = spawner;
        }

        private void SpawnObj()
        {
            _spawner.SpawnAtTargetPos(transform.position);
        }
    }
}
