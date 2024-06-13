using UnityEngine;
using UnityEngine.UI;

namespace HealthBar
{
    [RequireComponent(typeof(Slider))]

    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health _health;

        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            _health.HealthChanged += UpdateSlider;

            UpdateSlider();
        }

        private void OnDisable()
        {
            _health.HealthChanged -= UpdateSlider;
        }

        private void UpdateSlider()
        {
            float healthValueFraction = (_health.Value - _health.MinValue) / (_health.MaxValue - _health.MinValue);

            _slider.value = _slider.minValue + (_slider.maxValue - _slider.minValue) * healthValueFraction;
        }
    }
}
