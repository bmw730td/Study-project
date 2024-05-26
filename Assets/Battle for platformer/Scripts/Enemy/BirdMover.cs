using System.Collections.Generic;
using UnityEngine;

namespace BattleForPlatformer
{
    [RequireComponent(typeof(PlayerSensor))]
    
    public class BirdMover : EnemyMover
    {
        [SerializeField] private List<Vector3> _path = new();

        private int _currentWaypointIndex;

        private PlayerSensor _playerSensor;

        private void OnValidate()
        {
            CheckPath();
        }

        private void Awake()
        {
            _playerSensor = GetComponent<PlayerSensor>();
        }

        private void Start()
        {
            CheckPath();
        }

        private void Update()
        {
            CheckTarget();

            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, _speed * Time.deltaTime);
        }

        private void CheckTarget()
        {
            if (_playerSensor.IsPlayerInRange())
            {
                TargetPosition = _playerSensor.PlayerTransform.position;
            }
            else
            {
                TargetPosition = _path[_currentWaypointIndex];

                if (transform.position == TargetPosition)
                {
                    _currentWaypointIndex++;

                    if (_currentWaypointIndex >= _path.Count)
                        _currentWaypointIndex = 0;

                    TargetPosition = _path[_currentWaypointIndex];
                }
            }
        }

        private void CheckPath()
        {
            if (_path.Count < 1)
                _path.Add(transform.position);
        }
    }
}
