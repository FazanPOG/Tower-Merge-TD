using System;
using R3;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private HealthBarView _healthBar;
        [SerializeField] private EnemyDeathPopupView _deathPopupPrefab;

        private Enemy _enemy;
        private SpriteRenderer _spriteRenderer;
        private float _maxHealth;
        private int _buildingCurrencyOnDeath;
        private IDisposable _disposable;

        public void Init(Enemy enemy, ReadOnlyReactiveProperty<float> health, int buildingCurrencyOnDeath, Sprite enemySprite)
        {
            _enemy = enemy;
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

            var deathPopup = Instantiate(_deathPopupPrefab);
            deathPopup.transform.position = _enemy.transform.position;
            deathPopup.Init(_buildingCurrencyOnDeath);
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