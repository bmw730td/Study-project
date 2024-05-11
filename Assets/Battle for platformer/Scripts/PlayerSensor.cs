using UnityEngine;

namespace BattleForPlatformer
{
    public class PlayerSensor : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _range;
        
        private PlayerMovement _playerMovement;

        public Transform PlayerTransform { get; private set; }

        public bool IsPlayerInRange()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _range);
            _playerMovement = null;

            foreach (Collider2D hit in hits)
            {
                hit.TryGetComponent<PlayerMovement>(out _playerMovement);
            }
            
            if (_playerMovement != null)
                PlayerTransform = _playerMovement.transform;

            return _playerMovement != null;
        }
    }
}
