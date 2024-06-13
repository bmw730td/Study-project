using UnityEngine;
using UnityEngine.UI;

namespace HealthBar
{
    [RequireComponent(typeof(Slider))]

    public class SlowHealthBar : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField, Min(0f)] private float _transitionSpeed;

        private Slider _slider;
        private float _sliderValueFraction;
        private float _healthValueFraction;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            _health.HealthChanged += UpdateValueFractions;

            UpdateValueFractions();
        }

        private void OnDisable()
        {
            _health.HealthChanged -= UpdateValueFractions;
        }

        private void Update()
        {
            if (_sliderValueFraction != _healthValueFraction)
            {
                float desiredSliderValue = _slider.minValue + (_slider.maxValue - _slider.minValue) * _healthValueFraction;

                _slider.value = Mathf.MoveTowards(_slider.value, desiredSliderValue, _transitionSpeed * Time.deltaTime);
            }
        }

        private void UpdateValueFractions()
        {
            _sliderValueFraction = (_slider.value - _slider.minValue) / (_slider.maxValue - _slider.minValue);
            _healthValueFraction = (_health.Value - _health.MinValue) / (_health.MaxValue - _health.MinValue);
        }
    }
}