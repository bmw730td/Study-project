using UnityEngine;

namespace BattleForPlatformer
{
    [RequireComponent(typeof(Collider2D))]

    public class AttackOnCollision : MonoBehaviour
    {
        [SerializeField] private bool _isTargetPlayer;
        [SerializeField] private float _damage;

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (_isTargetPlayer)
            {
                if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth targetHealth))
                    targetHealth.TakeDamage(_damage);
            }
            else
            {
                if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth targetHealth))
                    targetHealth.TakeDamage(_damage);
            }
        }
    }
}
