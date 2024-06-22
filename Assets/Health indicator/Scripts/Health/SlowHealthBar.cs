using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace HealthBar
{
    public class SlowHealthBar : HealthViewer
    {
        [SerializeField, Min(0f)] private float _transitionSpeed;
        
        private Slider _slider;
        private float _sliderValueFraction;
        private float _healthValueFraction;
        private bool _isChangingSliderValue = false;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        protected override void UpdateValues()
        {
            _sliderValueFraction = (_slider.value - _slider.minValue) / (_slider.maxValue - _slider.minValue);
            _healthValueFraction = (Health.Value - Health.MinValue) / (Health.MaxValue - Health.MinValue);

            if (_isChangingSliderValue == false && _sliderValueFraction != _healthValueFraction)
                StartCoroutine(UpdateSliderValue());
        }

        private IEnumerator UpdateSliderValue()
        {
            _isChangingSliderValue = true;
            
            while (_sliderValueFraction != _healthValueFraction)
            {
                float desiredSliderValue = _slider.minValue + (_slider.maxValue - _slider.minValue) * _healthValueFraction;

                _slider.value = Mathf.MoveTowards(_slider.value, desiredSliderValue, _transitionSpeed * Time.deltaTime);

                yield return null;
            }

            _isChangingSliderValue = false;
        }
    }
}