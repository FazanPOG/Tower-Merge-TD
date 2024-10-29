using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private EnemyView _view;
        
        private readonly ReactiveProperty<float> _health = new ReactiveProperty<float>();
        private EnemyConfig _config;
        
        public event Action OnDied;

        public int Damage => _config.Damage;
        
        public void Init(EnemyConfig config, List<Vector3> movePath)
        {
            _config = config;
            _health.Value = _config.Health;
            
            EnemyMovement movement = new EnemyMovement();
            movement.Move(this, _view.transform, movePath, _config.MoveSpeed);
            _view.Init(_health, _config.Sprite);
        }

        public void TakeDamage(float damage)
        {
            if(damage < 0)
                throw new ArgumentException("Damage must be more than 0");

            _health.Value -= damage;

            if (_health.Value <= 0)
                DestroySelf();
        }

        public void DestroySelf()
        {
            OnDied?.Invoke();
            Destroy(gameObject);
        }
    }
}