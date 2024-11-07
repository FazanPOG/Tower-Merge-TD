using Sirenix.OdinInspector;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;
using UnityEngine;
using Zenject;

namespace TowerMergeTD.Utils.Debug
{
    [HideInEditorMode]
    public class GameplayDebug : MonoBehaviour
    {
        private IScoreService _scoreService;
        private IWaveSpawnerService _waveSpawnerService;

        [ShowInInspector, ReadOnly] public int Score => _scoreService.Score;
        [ShowInInspector] public PlayerHealthProxy Health { get; private set; }
        [ShowInInspector] public PlayerMoneyProxy Money { get; private set; }

        public void Init(DiContainer diContainer)
        {
            _scoreService = diContainer.Resolve<IScoreService>();
            _waveSpawnerService = diContainer.Resolve<IWaveSpawnerService>();
            Health = diContainer.Resolve<PlayerHealthProxy>();
            Money = diContainer.Resolve<PlayerMoneyProxy>();
        }
        
        [Button("Set health")]
        private void SetHealth(int health)
        {
            Health.Health.Value = health;
        }
        
        [Button("Set money")]
        private void SetMoney(int money)
        {
            Money.Money.Value = money;
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