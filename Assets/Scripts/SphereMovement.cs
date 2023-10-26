using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.localPosition += transform.forward * _speed * Time.deltaTime;
    }
}
