using UnityEngine;

namespace HealthBar
{
    public abstract class HealthViewer : MonoBehaviour
    {
        [SerializeField] private Health _health;

        protected Health Health => _health;

        private void OnEnable()
        {
            _health.ValueChanged += UpdateValues;

            UpdateValues();
        }

        private void OnDisable()
        {
            _health.ValueChanged -= UpdateValues;
        }

        protected abstract void UpdateValues();
    }
}
