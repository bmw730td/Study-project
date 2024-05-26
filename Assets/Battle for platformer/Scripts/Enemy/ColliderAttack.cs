using UnityEngine;

namespace BattleForPlatformer
{
    [RequireComponent(typeof(Collider2D))]
    
    public class ColliderAttack : MonoBehaviour
    {
        [SerializeField] private float _attackPower;

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerMover _) &&
                collision.gameObject.TryGetComponent(out Health playerHealth))
            {
                playerHealth.TakeDamage(_attackPower);
            }
        }
    }
}
