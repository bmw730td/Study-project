using System;
using System.Collections;
using UnityEngine;

namespace BattleForPlatformer
{
    public abstract class AbilityBase : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _power;
        [SerializeField, Min(0f)] private float _cooldownDuration;

        private float _cooldownTimer;

        public event Action CooldownTimerChanged;

        public float Power => _power;
        public float CooldownDuration => _cooldownDuration;
        public float CooldownTimer => _cooldownTimer;
        public bool IsOnCooldown => _cooldownTimer != 0f;

        protected void GoOnCooldown()
        {
            if (IsOnCooldown == false)
                StartCoroutine(CooldownTimerCounter());
        }

        private IEnumerator CooldownTimerCounter()
        {
            _cooldownTimer = _cooldownDuration;

            while (_cooldownTimer > 0f)
            {
                yield return null;

                _cooldownTimer -= Time.deltaTime;
                CooldownTimerChanged?.Invoke();
            }

            if (_cooldownTimer < 0f)
                _cooldownTimer = 0f;
        }
    }
}
