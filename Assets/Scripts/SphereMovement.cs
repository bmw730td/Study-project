using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private void Update()
    {
        transform.position += _speed * transform.forward * Time.deltaTime;
    }
}
