using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        gameObject.transform.Rotate(new Vector3(0, 1, 0), _speed * Time.deltaTime);
    }
}
