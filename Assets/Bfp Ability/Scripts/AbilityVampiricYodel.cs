using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HealthBar;

namespace BattleForPlatformer
{
    [RequireComponent(typeof(UserInput))]
    [RequireComponent(typeof(Health))]
    
    public class AbilityVampiricYodel : AbilityBase
    {
        [SerializeField, Min(0f)] private float _duration;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField, Min(0f)] private float _range;

        private UserInput _userInput;
        private Health _health;

        private float _durationTimer;

        public event Action Activated;
        public event Action Deactivated;
        public event Action StatsChanged;

        public float Range => _range;

        private void OnValidate()
        {
            StatsChanged?.Invoke();
        }

        private void Awake()
        {
            _userInput = GetComponent<UserInput>();
            _health = GetComponent<Health>();
        }

        private void Update()
        {
            if (_durationTimer == 0 && IsOnCooldown == false && _userInput.Controls.Ability.VampiricYodel.WasPerformedThisFrame())
            {
                StartCoroutine(DurationTimerCounter());
            }
        }

        private IEnumerator DurationTimerCounter()
        {
            Activated?.Invoke();
            _durationTimer = _duration;

            while (_durationTimer > 0f)
            {
                foreach (Health target in GetTargets())
                {
                    DrainHealth(target);
                }
                
                yield return null;

                _durationTimer -= Time.deltaTime;
            }

            if (_durationTimer < 0f)
                _durationTimer = 0f;

            Deactivated?.Invoke();
            GoOnCooldown();
        }
        
        private List<Health> GetTargets()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _range, _targetLayer);
            List<Health> targets = new();

            foreach (Collider2D hit in hits)
            {
                if (hit.TryGetComponent(out Health hitHealth))
                    targets.Add(hitHealth);
            }

            return targets;
        }

        private void DrainHealth(Health target)
        {
            target.TakeDamage(Power * Time.deltaTime);
            _health.TakeHeal(target.LastDamageReceived);
        }
    }
}

