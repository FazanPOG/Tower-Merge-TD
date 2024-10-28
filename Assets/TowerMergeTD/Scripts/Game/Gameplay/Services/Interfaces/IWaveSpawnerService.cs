using System;

namespace TowerMergeTD.Game.Gameplay
{
    public interface IWaveSpawnerService
    {
        event Action OnWaveCompleted;
        event Action OnAllWavesCompleted;
        void SpawnNextWave();
    }
}