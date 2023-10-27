using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    public Vector3 CollectionPoint;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, CollectionPoint, _speed * Time.deltaTime);
    }
}
