using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HealthBar;

namespace BattleForPlatformer
{
    public class AbilityVampiricYodel : AbilityBase
    {
        [SerializeField, Min(0f)] private float _duration;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField, Min(0f)] private float _range;

        private float _durationTimer;
        private Coroutine _durationTimerCounter;

        private Health _health;

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
            TryGetComponent(out _health);
        }

        private void Update()
        {
            if (IsOnCooldown == false && UserInput.Instance.Controls.Ability.VampiricYodel.WasPerformedThisFrame())
            {
                GoOnCooldown();
                StartDrainingHealth();
            }
        }

        private void StartDrainingHealth()
        {
            if (_durationTimer != 0)
                StopCoroutine(_durationTimerCounter);

            _durationTimerCounter = StartCoroutine(DurationTimerCounter());
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
            
            if (_health != null)
                _health.TakeHeal(Power * Time.deltaTime);
        }
    }
}

