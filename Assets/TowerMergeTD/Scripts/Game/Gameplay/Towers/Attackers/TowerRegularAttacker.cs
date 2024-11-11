using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class TowerRegularAttacker : ITowerAttacker
    {
        private readonly TowerCollisionHandler _collisionHandler;
        private readonly MonoBehaviour _monoBehaviourContext;
        
        private float _initialDamage;
        private float _attackCooldown;
        private bool _canAttack = true;
        private IDamageable _currentAttackTarget;

        public event Action OnAttacked;
        public event Action<GameObject> OnTargetChanged;

        public TowerRegularAttacker(TowerCollisionHandler collisionHandler, MonoBehaviour monoBehaviourContext)
        {
            _collisionHandler = collisionHandler;
            _monoBehaviourContext = monoBehaviourContext;
        }
        
        public void Init(float initialDamage, float attackRange, float attackCooldown)
        {
            _initialDamage = initialDamage;
            _attackCooldown = attackCooldown;
            
            _collisionHandler.AttackCollider.radius = attackRange;
            
            _collisionHandler.OnAttackColliderTriggering += OnAttackColliderTriggering;
        }

        private void OnAttackColliderTriggering(List<Collider2D> others)
        {
            IDamageable closestTarget = null;
            GameObject closestTargetObject = null;
            float closestDistance = Mathf.Infinity;

            foreach (var other in others)
            {
                if (other.gameObject.TryGetComponent(out IDamageable damageable))
                {
                    float distance = Vector2.Distance(_collisionHandler.transform.position, other.transform.position);
            
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTarget = damageable;
                        closestTargetObject = other.gameObject;
                    }
                }
            }

            if (closestTarget != null && closestTarget != _currentAttackTarget)
            {
                _currentAttackTarget = closestTarget;
                Attack(_currentAttackTarget);
                OnTargetChanged?.Invoke(closestTargetObject);
            }
        }

        private void Attack(IDamageable damageable)
        {
            if (_canAttack)
            {
                damageable.TakeDamage(_initialDamage);
                _monoBehaviourContext.StartCoroutine(AttackCooldown(_attackCooldown));
                OnAttacked?.Invoke();
            }
        }

        private IEnumerator AttackCooldown(float cooldown)
        {
            _canAttack = false;
            yield return new WaitForSeconds(cooldown);
            _canAttack = true;
        }
    }
}