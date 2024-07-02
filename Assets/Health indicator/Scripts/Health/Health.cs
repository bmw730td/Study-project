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
            if (amount < MinAmountOfDamage)
                amount = MinAmountOfDamage;

            _value -= amount;

            _value = Math.Clamp(_value, _minValue, _maxValue);

            ValueChanged?.Invoke();
            DamageReceived?.Invoke();

            if (_value == _minValue)
                ValueReachedMin?.Invoke();
        }

        public void TakeHeal(float amount)
        {
            if (amount < MinAmountOfHeal)
                amount = MinAmountOfHeal;

            _value += amount;

            _value = Math.Clamp(_value, _minValue, _maxValue);

            ValueChanged?.Invoke();
        }
    }
}
