using System;
using System.Collections.Generic;
using ModestTree;
using R3;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class TowerImpactAttacker : ITowerAttacker
    {
        private readonly TowerCollisionHandler _collisionHandler;

        private float _initialDamage;
        private float _attackCooldown;
        private ReadOnlyReactiveProperty<bool> _isDragging;
        private float _lastAttackTime = -Mathf.Infinity;

        public event Action OnAttacked;
        public event Action<GameObject> OnTargetChanged;

        public TowerImpactAttacker(TowerCollisionHandler collisionHandler)
        {
            _collisionHandler = collisionHandler;
        }

        public void Init(float initialDamage, float attackRange, float attackCooldown, ReadOnlyReactiveProperty<bool> isDragging)
        {
            _initialDamage = initialDamage;
            _attackCooldown = attackCooldown;
            _isDragging = isDragging;
            
            _collisionHandler.AttackCollider.radius = attackRange;
            
            _collisionHandler.OnAttackColliderTriggering += OnAttackColliderTriggering;
        }

        private void OnAttackColliderTriggering(List<Collider2D> others)
        {
            List<IDamageable> enemies = new List<IDamageable>();
            foreach (var other in others)
                if (other.gameObject.TryGetComponent(out IDamageable damageable))
                    enemies.Add(damageable);
            
            if (enemies.IsEmpty() == false)
                Attack(enemies);
        }

        private void Attack(List<IDamageable> damageables)
        {
            if (CanAttack() && _isDragging.CurrentValue == false)
            {
                foreach (var damageable in damageables)
                    damageable.TakeDamage(_initialDamage);

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