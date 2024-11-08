using Sirenix.OdinInspector;
using TowerMergeTD.Game.Gameplay;
using UnityEngine;
using Zenject;

namespace TowerMergeTD.Utils.Debug
{
    [HideInEditorMode]
    public class GameplayDebug : MonoBehaviour
    {
        private IScoreService _scoreService;
        private IWaveSpawnerService _waveSpawnerService;
        private PlayerHealthProxy _playerHealthProxy;
        private PlayerBuildingCurrencyProxy _buildingCurrency;

        [ShowInInspector, ReadOnly] public int Score => _scoreService.Score;
        [ShowInInspector] public int Health => _playerHealthProxy.Health.CurrentValue;
        [ShowInInspector] public int BuildingCurrency => _buildingCurrency.BuildingCurrency.CurrentValue;

        public void Init(DiContainer diContainer)
        {
            _scoreService = diContainer.Resolve<IScoreService>();
            _waveSpawnerService = diContainer.Resolve<IWaveSpawnerService>();
            
            _playerHealthProxy = diContainer.Resolve<PlayerHealthProxy>();
            _buildingCurrency = diContainer.Resolve<PlayerBuildingCurrencyProxy>();
        }
        
        [Button("Set health")]
        private void SetHealth(int health)
        {
            _playerHealthProxy.Health.Value = health;
        }
        
        [Button("Set building currency")]
        private void SetBuildingCurrency(int money)
        {
            _buildingCurrency.BuildingCurrency.Value = money;
        }
        
        [Button("Add score")]
        private void AddScore(int score)
        {
            _scoreService.AddScore(score);
        }
        
        [Button("Spawn next wave")]
        private void SpawnNextWave()
        {
            _waveSpawnerService.SpawnNextWave();
        }
    }
}