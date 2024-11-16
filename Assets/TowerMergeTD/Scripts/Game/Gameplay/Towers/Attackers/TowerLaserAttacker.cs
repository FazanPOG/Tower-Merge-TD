using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class TowerLaserAttacker : ITowerAttacker
    {
        private const float DAMAGE_MULTIPLIER = 1.25f;
        
        private readonly TowerCollisionHandler _collisionHandler;
        
        private float _initialDamage;
        private float _currentDamage;
        private bool _isAttacking;
        private float _attackRange;
        private float _attackCooldown;
        private float _timeSinceLastAction = 0f;
        private ReadOnlyReactiveProperty<bool> _isDragging;
        private GameObject _closestTargetObject;
        private IDamageable _closestTargetEnemy;
        private IDamageable _currentTargetEnemy;
        private float _lastAttackTime = -Mathf.Infinity;

        public GameObject ClosestTargetObject => _closestTargetObject;
        public bool OnDrag => _isDragging.CurrentValue;

        public event Action OnAttacked;

        public event Action<GameObject> OnTargetChanged;
        
        public TowerLaserAttacker(TowerCollisionHandler collisionHandler)
        {
            _collisionHandler = collisionHandler;
        }
        
        public void Init(float initialDamage, float attackRange, float attackCooldown, ReadOnlyReactiveProperty<bool> isDragging)
        {
            _initialDamage = initialDamage;
            _attackRange = attackRange;
            _attackCooldown = attackCooldown;
            
            _currentDamage = _initialDamage;
            _isDragging = isDragging;
            _collisionHandler.AttackCollider.radius = _attackRange;
            
            _collisionHandler.OnAttackColliderTriggering += OnAttackColliderTriggering;
        }
        
        private void OnAttackColliderTriggering(List<Collider2D> others)
        {
            List<GameObject> enemies = new List<GameObject>();
            foreach (var other in others)
                if (other.gameObject.TryGetComponent(out IDamageable damageable))
                    enemies.Add(other.gameObject);
            
            if (enemies.Contains(_closestTargetObject) == false)
                FindClosestEnemy(others);

            Attack();

            if (_closestTargetObject == null)
            {
                Debug.Log("_closestTargetObject NULL");
            }
        }

        private void FindClosestEnemy(List<Collider2D> others)
        {
            float closestDistance = Mathf.Infinity;
            
            foreach (var other in others)
            {
                if (other.gameObject.TryGetComponent(out IDamageable damageable))
                {
                    float distance = Vector2.Distance(_collisionHandler.transform.position, other.transform.position);
            
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        _closestTargetObject = other.gameObject;
                        _closestTargetEnemy = damageable;

                        if (_currentTargetEnemy != _closestTargetEnemy)
                        {
                            _currentTargetEnemy = _closestTargetEnemy;
                            StopAttack();
                        }

                        OnTargetChanged?.Invoke(_closestTargetObject);
                    }
                }
            }
        }

        private void Attack()
        {
            if(_isDragging.CurrentValue)
                return;

            if (_closestTargetObject != null && CanAttack())
            {
                if (_isAttacking == false)
                    StartAttack();
                else
                    ProcessAttack();
            }
        }

        private void StartAttack()
        {
            _isAttacking = true;
            OnAttacked?.Invoke();
        }

        private void ProcessAttack()
        {
            _timeSinceLastAction += Time.fixedDeltaTime;

            if (_timeSinceLastAction >= _attackCooldown)
            {
                if (IsInAttackRange())
                {
                    DealDamage();
                }
                else
                {
                    StopAttack();
                }

                _timeSinceLastAction = 0f;
            }
        }

        private void DealDamage()
        {
            _currentTargetEnemy.TakeDamage(_currentDamage);
            _currentDamage *= DAMAGE_MULTIPLIER;
            _lastAttackTime = Time.time;
        }

        private void StopAttack()
        {
            _timeSinceLastAction = 0f;
            _isAttacking = false;
            _currentDamage = _initialDamage;
        }
        
        private bool IsInAttackRange()
        {
            if (_closestTargetObject != null)
            {
                Vector3 direction = _closestTargetObject.transform.position - _collisionHandler.transform.position;
                float distance = direction.magnitude;
                
                return distance <= _attackRange;
            }

            return false;
        }

        private bool CanAttack()
        {
            return Time.time >= _lastAttackTime + _attackCooldown;
        }
    }
}