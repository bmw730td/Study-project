using UnityEngine;

namespace BattleForPlatformer
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Animator))]

    public class PlayerMover : MonoBehaviour
    {
        private readonly Vector3 TurnRotation = new(0f, 180f, 0f);
        
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpPower;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _groundCheckDistance;

        private Rigidbody2D _rigidbody2D;
        private Collider2D _collider2D;
        private Animator _animator;

        private bool _playerWantsToMove;
        private float _playerMoveInput;
        private bool _playerWantsToJump;

        private RaycastHit2D _groundHit;
        private bool _isFacingRight = true;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            ReadPlayerInput();
        }

        private void FixedUpdate()
        {
            TryMove();
            TryJump();
        }

        private void ReadPlayerInput()
        {
            if (_playerWantsToMove == false)
            {
                _playerWantsToMove = UserInput.Instance.Controls.Movement.Move.IsPressed();

                if (_playerWantsToMove)
                    _playerMoveInput = UserInput.Instance.MoveInput.x;
            }

            if (_playerWantsToJump == false)
                _playerWantsToJump = UserInput.Instance.Controls.Movement.Jump.WasPressedThisFrame();
        }

        private void TryMove()
        {
            if (_playerWantsToMove)
            {
                _playerWantsToMove = false;
                
                _animator.SetBool(PlayerAnimatorData.Parameters.IsWalking, true);

                _playerMoveInput = UserInput.Instance.MoveInput.x;
                CheckFacingDirection();

                transform.Translate(_moveSpeed * Vector2.right * _playerMoveInput * Time.deltaTime, Space.World);
            }
            else
            {
                _animator.SetBool(PlayerAnimatorData.Parameters.IsWalking, false);
            }
        }

        private void CheckFacingDirection()
        {
            if (_isFacingRight == false && _playerMoveInput > 0f)
            {
                Turn();
            }
            else if (_isFacingRight == true && _playerMoveInput < 0f)
            {
                Turn();
            }
        }

        private void Turn()
        {
            _isFacingRight = !_isFacingRight;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + TurnRotation);
        }

        private void TryJump()
        {
            if (IsGrounded() && _playerWantsToJump)
            {
                _playerWantsToJump = false;
                _animator.SetTrigger(PlayerAnimatorData.Parameters.Jump);
                _rigidbody2D.AddForce(_jumpPower * Vector2.up);
            }
        }

        private bool IsGrounded()
        {
            _groundHit = Physics2D.BoxCast(_collider2D.bounds.center, _collider2D.bounds.size, 0f, Vector2.down, _groundCheckDistance, _groundLayer);
            _animator.SetBool(PlayerAnimatorData.Parameters.IsInAir, _groundHit.collider == null);

            return _groundHit.collider != null;
        }
    }
}
