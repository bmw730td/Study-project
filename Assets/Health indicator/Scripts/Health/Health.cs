using System;
using UnityEngine;

namespace HealthBar
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _value;
        [SerializeField] private float _minValue;
        [SerializeField] private float _maxValue;

        public event Action HealthChanged;

        public float Value => _value;
        public float MinValue => _minValue;
        public float MaxValue => _maxValue;

        private void OnValidate()
        {
            _value = Math.Clamp(_value, _minValue, _maxValue);
            
            if (_minValue > _maxValue)
                _minValue = _maxValue;
        }

        private void Start ()
        {
            _value = _maxValue;

            HealthChanged?.Invoke();
        }

        public void ChangeValue(float amount)
        {
            _value += amount;

            _value = Mathf.Clamp(_value, _minValue, _maxValue);

            HealthChanged?.Invoke();
        }
    }
}
