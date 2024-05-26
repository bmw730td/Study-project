using System;
using UnityEngine;

namespace BattleForPlatformer
{
    public class Health : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _maxHealth;
        [SerializeField, Min(0f)] private float _minHealth;
        [SerializeField, Min(0f)] private float _currentHealth;

        public event Action DamageReceived;
        public event Action OnDeath;

        private void OnValidate()
        {
            if (_minHealth > _maxHealth)
                _minHealth = _maxHealth;

            _currentHealth = Mathf.Clamp(_currentHealth, _minHealth, _maxHealth);
        }

        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float amount)
        {
            if (amount < 0f)
                amount = 0f;

            _currentHealth -= amount;
            DamageReceived?.Invoke();

            if (_currentHealth <= _minHealth)
            {
                _currentHealth = _minHealth;
                OnDeath?.Invoke();
            }
        }

        public void TakeHeal(float amount)
        {
            if (amount < 0f)
                amount = 0f;

            _currentHealth += amount;

            if (_currentHealth > _maxHealth)
                _currentHealth = _maxHealth;
        }
    }
}
