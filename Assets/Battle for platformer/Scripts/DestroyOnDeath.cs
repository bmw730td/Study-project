using UnityEngine;

namespace BattleForPlatformer
{
    [RequireComponent(typeof(Health))]
    
    public class DestroyOnDeath : MonoBehaviour
    {
        [SerializeField] private Health _health;
        
        private void OnEnable()
        {
            _health.Death += DestroySelf;
        }

        private void OnDisable()
        {
            _health.Death -= DestroySelf;
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
