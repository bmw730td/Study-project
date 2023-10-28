using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform Target;

    [SerializeField] private float _speed;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.position, _speed * Time.deltaTime);
    }
}
