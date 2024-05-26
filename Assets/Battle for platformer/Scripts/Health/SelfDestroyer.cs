using UnityEngine;

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
            _health.OnDeath += DestroySelf;
        }

        private void OnDisable()
        {
            _health.OnDeath -= DestroySelf;
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
