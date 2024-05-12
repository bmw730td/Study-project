using UnityEngine;

namespace BattleForPlatformer
{
    [RequireComponent(typeof(EnemyMovement))]
    
    public class TargetObserver : MonoBehaviour
    {
        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Vector3 _targetPosition;
        private float _angle;

        private void Update()
        {
            _targetPosition = _enemyMovement.CurrentTarget.position;

            _targetPosition.x -= transform.position.x;
            _targetPosition.y -= transform.position.y;

            _angle = Mathf.Atan2(_targetPosition.y, _targetPosition.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
            _spriteRenderer.flipY = Mathf.Abs(_angle) > 90;
        }
    }
}
