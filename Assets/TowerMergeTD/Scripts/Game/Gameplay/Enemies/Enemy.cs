using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private EnemyView _view;
        
        private readonly ReactiveProperty<float> _health = new ReactiveProperty<float>();
        
        private Collider2D _collider;
        private PlayerBuildingCurrencyProxy _playerBuildingCurrencyProxy;
        private EnemyConfig _config;

        public event Action OnDied;
        
        public int Damage => _config.Damage;

        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }

        public void Init(PlayerBuildingCurrencyProxy playerBuildingCurrencyProxy, EnemyConfig config, List<Vector3> movePath)
        {
            _playerBuildingCurrencyProxy = playerBuildingCurrencyProxy;
            _config = config;
            _health.Value = _config.Health;
            
            EnemyMovement movement = new EnemyMovement();
            movement.Move(this, _view.transform, movePath, _config.MoveSpeed);
            _view.Init(this, _health, _config.BuildingCurrencyOnDeath, _config.Sprite);
        }

        public void TakeDamage(float damage)
        {
            if(damage < 0)
                throw new ArgumentException("Damage must be more than 0");

            _health.Value -= damage;

            if (_health.Value <= 0)
            {
                DestroySelf();
            }
        }
        
        public void DestroySelf()
        {
            _playerBuildingCurrencyProxy.BuildingCurrency.Value += _config.BuildingCurrencyOnDeath;
            OnDied?.Invoke();
            Destroy(gameObject);
        }
    }
}