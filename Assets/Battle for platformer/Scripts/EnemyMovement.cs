using UnityEngine;

namespace BattleForPlatformer
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Transform _path;
        [SerializeField] private float _speed;

        [SerializeField] private PlayerMovement _player;
        [SerializeField, Min(0f)] private float _playerCheckDistance;
        [SerializeField] private float _chaseSpeed;

        private int _currentWaypointIndex = 0;
        private Transform _currentWaypoint;

        private void Start()
        {
            _currentWaypoint = GetNextWaypoint();
            transform.position = _currentWaypoint.position;
            _currentWaypoint = GetNextWaypoint();
        }

        private void Update()
        {
            if (Vector2.Distance(transform.position, _player.transform.position) <= _playerCheckDistance)
            {
                ChasePlayer();
            }
            else
            {
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
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _chaseSpeed * Time.deltaTime);
        }
    }
}
