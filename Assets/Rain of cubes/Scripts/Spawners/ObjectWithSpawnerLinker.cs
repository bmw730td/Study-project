using UnityEngine;

namespace RainOfCubes
{
    [RequireComponent(typeof(ObjectSpawner))]
    
    public class ObjectLeaverLinker : MonoBehaviour
    {
        [SerializeField] private TargetableSpawner _targetSpawner;

        private ObjectSpawner _spawner;

        private void Awake()
        {
            _spawner = GetComponent<ObjectSpawner>();
        }

        private void OnEnable()
        {
            _spawner.WillAddToPool += LinkObjectLeaver;
        }

        private void OnDisable()
        {
            _spawner.WillAddToPool -= LinkObjectLeaver;
        }

        private void LinkObjectLeaver(GameObject obj)
        {
            if (obj.TryGetComponent(out ObjectLeaver objLeaver))
                objLeaver.SetSpawner(_targetSpawner);
        }
    }
}
