using System;
using R3;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private HealthBarView _healthBar;
        [SerializeField] private EnemyDeathPopupView _deathPopup;

        private Enemy _enemy;
        private Action _onDiedCallback;
        private SpriteRenderer _spriteRenderer;
        private float _maxHealth;
        private int _buildingCurrencyOnDeath;
        private IDisposable _disposable;

        public void Init(Enemy enemy, Action onDiedCallback, ReadOnlyReactiveProperty<float> health, int buildingCurrencyOnDeath, Sprite enemySprite)
        {
            _enemy = enemy;
            _onDiedCallback = onDiedCallback;
            _maxHealth = health.CurrentValue;
            _buildingCurrencyOnDeath = buildingCurrencyOnDeath;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            _spriteRenderer.sprite = enemySprite;

            enemy.OnDied += OnDied;
            _disposable = health.Subscribe(UpdateHealthBar);
        }

        private void OnDied()
        {
            _spriteRenderer.gameObject.SetActive(false);
            _healthBar.gameObject.SetActive(false);
            _deathPopup.gameObject.SetActive(true);
            _deathPopup.ShowPopup(_buildingCurrencyOnDeath, _onDiedCallback);
        }
        
        private void UpdateHealthBar(float health)
        {
            _healthBar.UpdateBarFillAmount(health / _maxHealth);
        }
        
        private void OnDisable()
        {
            _enemy.OnDied -= OnDied;
            _disposable?.Dispose();
        }
    }
}