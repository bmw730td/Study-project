using UnityEngine;
using HealthBar;

namespace BattleForPlatformer
{
    [RequireComponent(typeof(Health))]
    
    public class SelfDestroyer : MonoBehaviour
    {
        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _health.ValueReachedMin += DestroySelf;
        }

        private void OnDisable()
        {
            _health.ValueReachedMin -= DestroySelf;
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
