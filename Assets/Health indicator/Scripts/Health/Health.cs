using System;
using UnityEngine;

namespace HealthBar
{
    public class Health : MonoBehaviour
    {
        private const float MinAmountOfDamage = 0f;
        private const float MinAmountOfHeal = 0f;
        
        [SerializeField] private float _value;
        [SerializeField] private float _minValue;
        [SerializeField] private float _maxValue;

        public event Action ValueChanged;
        public event Action DamageReceived;
        public event Action ValueReachedMin;

        public float Value => _value;
        public float MinValue => _minValue;
        public float MaxValue => _maxValue;
        public float LastDamageReceived { get; private set; }

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
            amount = _value - Mathf.Clamp(_value - amount, _minValue, _maxValue);
            _value -= amount;
            _value = Math.Clamp(_value, _minValue, _maxValue);

            ValueChanged?.Invoke();
            LastDamageReceived = amount;
            DamageReceived?.Invoke();

            if (_value == _minValue)
                ValueReachedMin?.Invoke();
        }

        public void TakeHeal(float amount)
        {
            amount = Mathf.Clamp(_value + amount, _minValue, _maxValue) - _value;
            _value += amount;
            _value = Math.Clamp(_value, _minValue, _maxValue);

            ValueChanged?.Invoke();
        }
    }
}
