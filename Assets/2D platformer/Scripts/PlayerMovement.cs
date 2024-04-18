using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerMovement : MonoBehaviour
{
    private const float _isGroundedDistance = 0.05f;
    
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private LayerMask _groundLayer;
    
    private BoxCollider2D _collider;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _animator.SetBool("IsWalkingLeft", true);
            }

            transform.Translate(new Vector3(_speed * -1 * Time.deltaTime, 0, 0));
            
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            _animator.SetBool("IsWalkingLeft", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                _animator.SetBool("IsWalkingRight", true);
            }

            transform.Translate(new Vector3(_speed * Time.deltaTime, 0, 0));
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            _animator.SetBool("IsWalkingRight", false);
        }

        if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
        {
            _rigidbody2D.AddForce(new Vector2(0, _jumpPower));
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, _isGroundedDistance, _groundLayer);

        return raycastHit2D.collider != null;
    }
}
