using UnityEngine;

namespace BattleForPlatformer
{
    [RequireComponent(typeof(EnemyMover))]
    [RequireComponent(typeof(SpriteRenderer))]

    public class TargetObserver : MonoBehaviour
    {
        private EnemyMover _enemyMovement;
        private SpriteRenderer _spriteRenderer;

        private Vector3 _targetPosition;
        private float _angle;

        private void Awake()
        {
            _enemyMovement = GetComponent<EnemyMover>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _targetPosition = _enemyMovement.TargetPosition;
            
            _targetPosition.x -= transform.position.x;
            _targetPosition.y -= transform.position.y;

            _angle = Mathf.Atan2(_targetPosition.y, _targetPosition.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, _angle));

            _spriteRenderer.flipY = Mathf.Abs(_angle) > 90f;
        }
    }
}
