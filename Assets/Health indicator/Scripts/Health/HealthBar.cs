using System;
using UnityEngine;
using UnityEngine.UI;

namespace HealthBar
{
    [RequireComponent(typeof(Slider))]

    public class HealthBar : MonoBehaviour
    {
        [SerializeField] protected Health _health;

        protected Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        protected void OnEnable()
        {
            _health.HealthChanged += UpdateValues;

            UpdateValues();
        }

        protected void OnDisable()
        {
            _health.HealthChanged -= UpdateValues;
        }

        protected virtual void UpdateValues()
        {
            float healthValueFraction = (_health.Value - _health.MinValue) / (_health.MaxValue - _health.MinValue);

            _slider.value = _slider.minValue + (_slider.maxValue - _slider.minValue) * healthValueFraction;
        }
    }
}
