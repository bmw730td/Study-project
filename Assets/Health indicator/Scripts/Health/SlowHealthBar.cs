using UnityEngine;

namespace HealthBar
{
    public class SlowHealthBar : HealthBar
    {
        [SerializeField, Min(0f)] private float _transitionSpeed;

        private float _sliderValueFraction;
        private float _healthValueFraction;

        private void Update()
        {
            if (_sliderValueFraction != _healthValueFraction)
            {
                float desiredSliderValue = _slider.minValue + (_slider.maxValue - _slider.minValue) * _healthValueFraction;

                _slider.value = Mathf.MoveTowards(_slider.value, desiredSliderValue, _transitionSpeed * Time.deltaTime);
            }
        }

        protected override void UpdateValues()
        {
            _sliderValueFraction = (_slider.value - _slider.minValue) / (_slider.maxValue - _slider.minValue);
            _healthValueFraction = (_health.Value - _health.MinValue) / (_health.MaxValue - _health.MinValue);
        }
    }
}