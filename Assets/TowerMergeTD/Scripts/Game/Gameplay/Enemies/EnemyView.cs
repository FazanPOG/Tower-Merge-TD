using System;
using R3;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private HealthBarView _healthBar;

        private float _maxHealth;
        private IDisposable _disposable;
        
        public void Init(ReadOnlyReactiveProperty<float> health, Sprite enemySprite)
        {
            _maxHealth = health.CurrentValue;
            GetComponent<SpriteRenderer>().sprite = enemySprite;
            
            _disposable = health.Subscribe(UpdateHealthBar);
        }

        private void UpdateHealthBar(float health)
        {
            _healthBar.UpdateBarFillAmount(health / _maxHealth);
        }
        
        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}