using UnityEngine;

namespace BattleForPlatformer
{
    [RequireComponent(typeof(Collider2D))]

    public class AttackOnCollision : MonoBehaviour
    {
        [SerializeField] private string _targetTag;
        [SerializeField] private float _damage;
        
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(_targetTag))
            {
                if (collision.gameObject.TryGetComponent<Health>(out Health targetHealth))
                {
                    targetHealth.TakeDamage(_damage);
                }

            }
        }
    }
}
