using UnityEngine;
using TMPro;

namespace RainOfCubes
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    
    public class SpawnerViewer : MonoBehaviour
    {
        [SerializeField] private ObjectSpawner _spawner;
        
        private TextMeshProUGUI _spawnerInfo;

        private void Awake()
        {
            _spawnerInfo = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _spawner.ObjectSpawned += UpdateText;

            UpdateText();
        }

        private void OnDisable()
        {
            _spawner.ObjectSpawned -= UpdateText;
        }

        private void UpdateText()
        {
            _spawnerInfo.text = $"{_spawner.gameObject.name}\n" +
                                $"Objects in pool: {_spawner.ObjectsInPool},\n" +
                                $"Objects spawned: {_spawner.ObjectsSpawned}";
        }
    }
}
