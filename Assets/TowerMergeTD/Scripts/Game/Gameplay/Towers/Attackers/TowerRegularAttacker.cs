using System;
using System.Collections;
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

        private void OnAttackColliderTriggering(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                Attack(damageable);
                OnTargetChanged?.Invoke(other.gameObject);
            }
        }

        private void Attack(IDamageable damageable)
        {
            if (_canAttack)
            {
                damageable.TakeDamage(_initialDamage);
                _monoBehaviourContext.StartCoroutine(AttackCooldown(_attackCooldown));
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