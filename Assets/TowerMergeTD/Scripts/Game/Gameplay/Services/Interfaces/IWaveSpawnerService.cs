using System;

namespace TowerMergeTD.Game.Gameplay
{
    public interface IWaveSpawnerService
    {
        int WavesCount { get; }
        event Action OnWaveCompleted;
        event Action OnAllWavesCompleted;
        void SpawnNextWave();
    }
}