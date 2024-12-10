using System;
using System.Collections.Generic;
using R3;
using TowerMergeTD.Game.Audio;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TowerMergeTD.Game.Gameplay
{
    public class TowerElectricAttacker : ITowerAttacker
    {
        private readonly TowerCollisionHandler _collisionHandler;
        private readonly Lightning _lightningPrefab;
        private readonly ElectricTowerObjectView _electricTowerObjectView;
        private readonly AudioPlayer _audioPlayer;

        private float _initialDamage;
        private float _attackCooldown;
        private float _lastAttackTime = -Mathf.Infinity;
        private ReadOnlyReactiveProperty<bool> _isDragging;
        private GameObject _closestTargetObject;

        public event Action OnAttacked;
        public event Action<GameObject> OnTargetChanged;


        public TowerElectricAttacker(
            TowerCollisionHandler collisionHandler, 
            Lightning lightningPrefab,
            ElectricTowerObjectView electricTowerObjectView,
            AudioPlayer audioPlayer)
        {
            _collisionHandler = collisionHandler;
            _lightningPrefab = lightningPrefab;
            _electricTowerObjectView = electricTowerObjectView;
            _audioPlayer = audioPlayer;
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
            
            Lightning lightning = Object.Instantiate(_lightningPrefab);
            lightning.transform.position = _electricTowerObjectView.ElectricSpawnTransform.position;
            lightning.Init(_initialDamage, transform, _audioPlayer);
            
            _lastAttackTime = Time.time;
            
            OnAttacked?.Invoke();
        }
        
        private bool CanAttack()
        {
            return Time.time >= _lastAttackTime + _attackCooldown;
        }
    }
}