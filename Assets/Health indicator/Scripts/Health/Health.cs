using System;
using UnityEngine;

namespace HealthBar
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _value;
        [SerializeField] private float _minValue;
        [SerializeField] private float _maxValue;

        public event Action ValueChanged;

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

            ValueChanged?.Invoke();
        }

        public void TakeDamage(float amount)
        {
            if (amount < 0f)
                amount = 0f;

            _value -= amount;

            if (_value < _minValue)
                _value = _minValue;

            ValueChanged?.Invoke();
        }

        public void TakeHeal(float amount)
        {
            if (amount < 0f)
                amount = 0f;

            _value += amount;

            if (_value > _maxValue)
                _value = _maxValue;

            ValueChanged?.Invoke();
        }
    }
}
