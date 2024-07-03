using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HealthBar;

namespace BattleForPlatformer
{
    [RequireComponent(typeof(UserInput))]
    [RequireComponent(typeof(Animator))]
    
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject _slash;

        [SerializeField, Min(0f)] private float _attackPower;
        [SerializeField, Min(0f)] private float _attackInterval;

        [SerializeField] private Transform _attackPoint;
        [SerializeField, Min(0f)] private float _attackCircleRadius;
        [SerializeField] private LayerMask _enemyLayer;

        private UserInput _userInput;
        private Animator _animator;

        private bool _isAbleToAttack = true;
        private float _attackTimer;
        private List<Collider2D> _attackedEnemies = new();

        private void Awake()
        {
            _userInput = GetComponent<UserInput>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _slash.SetActive(false);
        }

        private void Update()
        {
            if (_userInput.Controls.Combat.Attack.WasPressedThisFrame() && _isAbleToAttack)
            {
                StartCoroutine(AttackCoroutine());
            }
        }

        private IEnumerator AttackCoroutine()
        {
            SwitchStates();

            _attackTimer = _attackInterval;
            _attackedEnemies.Clear();

            while (_attackTimer > 0f)
            {
                Attack();
                _attackTimer -= Time.deltaTime;

                yield return null;
            }

            SwitchStates();
        }

        private void SwitchStates()
        {
            _isAbleToAttack = !_isAbleToAttack;
            _slash.SetActive(!_slash.activeSelf);
            _animator.SetBool(PlayerAnimatorData.Parameters.IsAttacking, !_animator.GetBool(PlayerAnimatorData.Parameters.IsAttacking));
        }

        private void Attack()
        {
            Collider2D[] enemiesInAttackArea = Physics2D.OverlapCircleAll(_attackPoint.position, _attackCircleRadius, _enemyLayer);

            foreach (Collider2D enemy in enemiesInAttackArea)
            {
                if (_attackedEnemies.Contains(enemy) == false && enemy.TryGetComponent(out Health enemyHealth))
                {
                    enemyHealth.TakeDamage(_attackPower);
                    _attackedEnemies.Add(enemy);
                }
            }
        }
    }
}
