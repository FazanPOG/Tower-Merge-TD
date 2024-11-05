using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    //TODO
    public class GameplayState : IGameState
    {
        private readonly IWaveSpawnerService _waveSpawnerService;

        public GameplayState(IWaveSpawnerService waveSpawnerService)
        {
            _waveSpawnerService = waveSpawnerService;
        }
        
        public void Enter()
        {
            _waveSpawnerService.SpawnNextWave();
            _waveSpawnerService.OnWaveCompleted += SpawnNextWave;
            _waveSpawnerService.OnAllWavesCompleted += () => { Debug.Log("---Level complete!---"); };
        }

        private void SpawnNextWave()
        {
            _waveSpawnerService.SpawnNextWave();
        }
        
        public void Exit()
        {
            _waveSpawnerService.OnWaveCompleted -= SpawnNextWave;
        }
    }
}