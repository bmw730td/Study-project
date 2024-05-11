using UnityEngine;

namespace BattleForPlatformer
{
    [RequireComponent(typeof(PlayerSensor))]

    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Transform _path;
        [SerializeField] private float _speed;

        [SerializeField] private PlayerSensor _playerSensor;
        [SerializeField] private float _chaseSpeed;

        private int _currentWaypointIndex = 0;
        private Transform _currentWaypoint;

        public Transform CurrentTarget { get; private set; }

        private void Start()
        {
            _currentWaypoint = GetNextWaypoint();
            transform.position = _currentWaypoint.position;
            _currentWaypoint = GetNextWaypoint();
            CurrentTarget = _currentWaypoint;
        }

        private void Update()
        {
            if (_playerSensor.IsPlayerInRange())
            {
                CurrentTarget = _playerSensor.PlayerTransform;
                ChasePlayer();
            }
            else
            {
                CurrentTarget = _currentWaypoint;
                MoveToCurrentWaypoint();
            }
        }

        private void MoveToCurrentWaypoint()
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentWaypoint.position, _speed * Time.deltaTime);

            if (transform.position == _currentWaypoint.position)
                _currentWaypoint = GetNextWaypoint();
        }

        private Transform GetNextWaypoint()
        {
            if (_currentWaypointIndex >= _path.childCount)
                _currentWaypointIndex %= _path.childCount;

            return _path.GetChild(_currentWaypointIndex++).transform;
        }

        private void ChasePlayer()
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerSensor.PlayerTransform.position, _chaseSpeed * Time.deltaTime);
        }
    }
}
