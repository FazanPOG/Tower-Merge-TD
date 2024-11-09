using System;
using System.Collections.Generic;
using R3;
using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.Gameplay
{
    public class GameplayState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly PlayerHealthProxy _playerHealthProxy;
        private readonly IWaveSpawnerService[] _waveSpawnerServices;
        private readonly IPauseService _pauseService;

        private int _completeWavesCount;
        private int _completeAllWavesCount;
        private IDisposable _disposable;
        
        public GameplayState(
            GameStateMachine gameStateMachine, 
            PlayerHealthProxy playerHealthProxy, 
            IWaveSpawnerService[] waveSpawnerServices,
            IPauseService pauseService)
        {
            _gameStateMachine = gameStateMachine;
            _playerHealthProxy = playerHealthProxy;
            _waveSpawnerServices = waveSpawnerServices;
            _pauseService = pauseService;

            _completeWavesCount = 0;
            _completeAllWavesCount = 0;
        }
        
        public void Enter()
        {
            _pauseService.SetPause(false);

            foreach (var waveSpawnerService in _waveSpawnerServices)
            {
                waveSpawnerService.OnWaveCompleted += OnWaveComplete;
                waveSpawnerService.OnAllWavesCompleted += OnAllWavesCompleted;
            }

            _disposable = _playerHealthProxy.Health.Subscribe(OnHealthChanged);
            
            SpawnNextWaves();
        }

        private void OnWaveComplete()
        {
            _completeWavesCount++;
            
            if (_completeWavesCount == _waveSpawnerServices.Length)
                SpawnNextWaves();
        }

        private void OnAllWavesCompleted()
        {
            _completeAllWavesCount++;

            if (_completeAllWavesCount == _waveSpawnerServices.Length)
                WinGame();
        }

        private void OnHealthChanged(int health)
        {
            if(health <= 0)
                _gameStateMachine.EnterIn<LoseGameState>();
        }

        private void WinGame()
        {
            _gameStateMachine.EnterIn<WinGameState>();
        }

        private void SpawnNextWaves()
        {
            _completeWavesCount = 0;
            
            foreach (var spawnerService in _waveSpawnerServices)
                spawnerService.SpawnNextWave();
        }
        
        public void Exit()
        {
            foreach (var waveSpawnerService in _waveSpawnerServices)
            {
                waveSpawnerService.OnWaveCompleted -= OnWaveComplete;
                waveSpawnerService.OnAllWavesCompleted -= OnAllWavesCompleted;
            }
            
            _disposable.Dispose();
        }
    }
}