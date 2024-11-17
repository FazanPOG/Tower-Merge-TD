using System.Linq;
using TowerMergeTD.Game.Gameplay;

namespace TowerMergeTD.Game.UI
{
    public class WavesCounterViewAdapter
    {
        private readonly IWaveSpawnerService[] _waveSpawnerServices;
        private readonly WavesCounterView _counterView;
        private readonly int _maxWaves;
        
        private int _currentWave;
        private int _completeWavesCount;
        
        public WavesCounterViewAdapter(IWaveSpawnerService[] waveSpawnerServices, WavesCounterView counterView)
        {
            _waveSpawnerServices = waveSpawnerServices;
            _counterView = counterView;
            
            _maxWaves = waveSpawnerServices.First().WavesCount;
            
            foreach (var waveSpawnerService in _waveSpawnerServices)
                waveSpawnerService.OnWaveCompleted += OnWaveComplete;

            UpdateText();
        }

        private void OnWaveComplete()
        {
            _completeWavesCount++;
            if (_completeWavesCount == _waveSpawnerServices.Length)
            {
                _completeWavesCount = 0;
                _currentWave++;
                UpdateText();
            }
        }
        
        private void UpdateText()
        {
            _counterView.SetCountText($"{_currentWave}/{_maxWaves}");
        }
    }
}