using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleForPlatformer
{
    [RequireComponent(typeof(Collider2D))]

    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private LayerMask _groundLayer;

        [SerializeField, Min(0)] private float _speed;
        [SerializeField, Min(0)] private float _jumpPower; 
        [SerializeField] private float _groundedDistanceCheck = 0.05f;

        private float _horizontalAxis;

        void Update()
        {
            _horizontalAxis = Input.GetAxis("Horizontal");

            transform.Translate(_speed * Vector2.right * _horizontalAxis * Time.deltaTime);
            _animator.SetFloat("HorizontalMovement", _horizontalAxis);
            _animator.SetBool("IsMoving", _horizontalAxis != 0);

            if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
                _rigidbody.AddForce(_jumpPower * Vector2.up);
        }

        private bool IsGrounded()
        {
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, _groundedDistanceCheck, _groundLayer);

            return raycastHit2D.collider != null;
        }
    }
}
