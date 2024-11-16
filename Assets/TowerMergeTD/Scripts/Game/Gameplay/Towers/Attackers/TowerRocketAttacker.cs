using System;
using System.Collections.Generic;
using R3;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TowerMergeTD.Game.Gameplay
{
    public class TowerRocketAttacker : ITowerAttacker
    {
        private readonly TowerCollisionHandler _collisionHandler;
        private readonly Rocket _rocketPrefab;
        private readonly RocketTowerObjectView _rocketTowerObjectView;

        private float _initialDamage;
        private float _defaultAttackCooldown;
        private float _currentAttackCooldown;
        private float _lastAttackTime = -Mathf.Infinity;
        private ReadOnlyReactiveProperty<bool> _isDragging;
        private GameObject _closestTargetObject;
        
        public event Action OnAttacked;
        public event Action<GameObject> OnTargetChanged;

        public TowerRocketAttacker(
            TowerCollisionHandler collisionHandler, 
            Rocket rocketPrefab,
            RocketTowerObjectView rocketTowerObjectView)
        {
            _collisionHandler = collisionHandler;
            _rocketPrefab = rocketPrefab;
            _rocketTowerObjectView = rocketTowerObjectView;
        }
        
        public void Init(float initialDamage, float attackRange, float attackCooldown, ReadOnlyReactiveProperty<bool> isDragging)
        {
            _initialDamage = initialDamage;
            _defaultAttackCooldown = attackCooldown;
            _currentAttackCooldown = _defaultAttackCooldown;
            _isDragging = isDragging;
            
            _collisionHandler.AttackCollider.radius = attackRange;
            
            _collisionHandler.OnAttackColliderTriggering += OnAttackColliderTriggering;
        }

        public void HandleGameSpeed(GameSpeed gameSpeed)
        {
            switch (gameSpeed)
            {
                case GameSpeed.X1:
                    _currentAttackCooldown = _defaultAttackCooldown;
                    break;
                
                case GameSpeed.X2:
                    _currentAttackCooldown /= 2f;
                    break;
                
                case GameSpeed.X4:
                    _currentAttackCooldown /= 4f;
                    break;
                
                case GameSpeed.X8:
                    _currentAttackCooldown /= 8f;
                    break;
            }
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
                Attack(_closestTargetObject.transform);
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

        private void Attack(Transform transform)
        {
            if(CanAttack() == false || _isDragging.CurrentValue)
                return;
            
            Rocket rocket = Object.Instantiate(_rocketPrefab);
            rocket.transform.position = _rocketTowerObjectView.RocketSpawnTransform.position;
            rocket.Init(_initialDamage, transform);
            
            _lastAttackTime = Time.time;
            
            OnAttacked?.Invoke();
        }
        
        private bool CanAttack()
        {
            return Time.time >= _lastAttackTime + _currentAttackCooldown;
        }
    }
}