using UnityEngine;

namespace BattleForPlatformer
{
    [RequireComponent(typeof(EnemyMovement))]
    
    public class LookAtTarget : MonoBehaviour
    {
        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Vector3 _targetPosition;
        private float angle;

        private void Update()
        {
            _targetPosition = _enemyMovement.CurrentTarget.position;

            _targetPosition.x -= transform.position.x;
            _targetPosition.y -= transform.position.y;

            angle = Mathf.Atan2(_targetPosition.y, _targetPosition.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            if(Mathf.Abs(angle) > 90)
            {
                _spriteRenderer.flipY = true;
            }
            else
            {
                _spriteRenderer.flipY = false;
            }
        }
    }
}
