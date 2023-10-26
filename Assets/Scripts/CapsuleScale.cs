using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleScale : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.localScale += new Vector3(_speed * Time.deltaTime, _speed * Time.deltaTime, _speed * Time.deltaTime);
    }
}
