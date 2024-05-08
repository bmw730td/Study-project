using System;
using UnityEngine;

namespace BattleForPlatformer
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _minHealth;
        [SerializeField] private float _health;

        public event Action DamageInflicted;
        public event Action Death;

        private void OnValidate()
        {
            if (_health > _maxHealth)
            {
                _health = _maxHealth;
            }
            else if( _health < _minHealth)
            {
                _health = _minHealth;
            }

            if (_minHealth > _maxHealth)
                _minHealth = _maxHealth;
        }

        private void Start()
        {
            _health = _maxHealth;
        }

        public void TakeDamage(float amount)
        {
            if (amount < 0f)
                amount = 0f;

            _health -= amount;
            DamageInflicted?.Invoke();

            if (_health <= _minHealth)
            {
                _health = _minHealth;
                Death?.Invoke();
            }
        }

        public void Heal(float amount)
        {
            if (amount < 0f)
                amount = 0f;
            
            _health += amount;

            if (_health > _maxHealth)
                _health = _maxHealth;
        }
    }
}
