using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class LaserMover : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _speed;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = transform.right * _speed * Time.fixedDeltaTime;
    }
}
