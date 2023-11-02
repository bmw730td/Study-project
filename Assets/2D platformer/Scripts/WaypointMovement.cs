using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;

    private Transform[] _waypoints;
    private int _currentPoint;

    private void Start()
    {
        _waypoints = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _waypoints[i] = _path.GetChild(i);
        }

        _currentPoint = 0;
        
        transform.position = _waypoints[_currentPoint].position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentPoint].position, _speed * Time.deltaTime);
        
        if (transform.position == _waypoints[_currentPoint].position)
        {
            _currentPoint++;

            if (_currentPoint == _waypoints.Length)
                _currentPoint = 0;
        }
    }
}
