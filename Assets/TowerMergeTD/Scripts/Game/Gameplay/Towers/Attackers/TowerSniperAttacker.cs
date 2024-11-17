using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class TowerSniperAttacker : ITowerAttacker
    {
        private readonly TowerCollisionHandler _collisionHandler;
        
        private float _initialDamage;
        private float _attackCooldown;
        private float _attackRange;
        private ReadOnlyReactiveProperty<bool> _isDragging;
        private GameObject _closestTargetObject;
        private float _lastAttackTime = -Mathf.Infinity;
        
        public event Action OnAttacked;
        public event Action<GameObject> OnTargetChanged;
        
        public TowerSniperAttacker(TowerCollisionHandler collisionHandler)
        {
            _collisionHandler = collisionHandler;
        }
        
        public void Init(float initialDamage, float attackRange, float attackCooldown, ReadOnlyReactiveProperty<bool> isDragging)
        {
            _initialDamage = initialDamage;
            _attackRange = attackRange;
            _attackCooldown = attackCooldown;
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

            if (_closestTargetObject != null)
                Attack();
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
                        OnTargetChanged?.Invoke(_closestTargetObject);
                    }
                }
            }
        }
        
        private void Attack()
        {
            if (CanAttack() && _isDragging.CurrentValue == false)
            {
                Vector2 direction = (_closestTargetObject.transform.position - _collisionHandler.transform.position).normalized;
                RaycastHit2D[] hits = Physics2D.RaycastAll(_collisionHandler.transform.position, direction, _attackRange);

                foreach (var hit in hits)
                {
                    if (hit.collider.TryGetComponent(out IDamageable damageable))
                    {
                        damageable.TakeDamage(_initialDamage);
                    }
                }
                
                _lastAttackTime = Time.time;
                OnAttacked?.Invoke();
            }
        }

        private bool CanAttack()
        {
            return Time.time >= _lastAttackTime + _attackCooldown;
        }
    }
}