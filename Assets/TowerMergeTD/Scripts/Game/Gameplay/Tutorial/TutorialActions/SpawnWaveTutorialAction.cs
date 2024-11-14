using R3;

namespace TowerMergeTD.Game.Gameplay
{
    public class SpawnWaveTutorialAction : ITutorialAction
    {
        private readonly IWaveSpawnerService[] _waveSpawnerServices;
        private readonly ReactiveProperty<bool> _isComplete = new ReactiveProperty<bool>();

        public ReadOnlyReactiveProperty<bool> IsComplete => _isComplete;

        public SpawnWaveTutorialAction(IWaveSpawnerService[] waveSpawnerServices)
        {
            _waveSpawnerServices = waveSpawnerServices;
        }
        
        public void StartAction()
        {
            foreach (var waveSpawner in _waveSpawnerServices)
            {
                waveSpawner.SpawnNextWave();
            }

            _isComplete.Value = true;
        }

        public void Dispose()
        {
            _isComplete?.Dispose();
        }
    }
}