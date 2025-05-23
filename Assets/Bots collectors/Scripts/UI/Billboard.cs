using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform _camera;

    private void Awake()
    {
        _camera = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(transform.position + _camera.forward);
    }
}
