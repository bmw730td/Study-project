using System;
using UnityEngine;
using UnityEngine.UI;

namespace HealthBar
{
    [RequireComponent(typeof(Slider))]

    public class HealthBar : HealthViewer
    {
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        protected override void UpdateValues()
        {
            float healthValueFraction = (Health.Value - Health.MinValue) / (Health.MaxValue - Health.MinValue);

            _slider.value = _slider.minValue + (_slider.maxValue - _slider.minValue) * healthValueFraction;
        }
    }
}
