using System;
using R3;
using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.Gameplay
{
    //TODO
    public class GameplayState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly PlayerHealthProxy _playerHealthProxy;
        private readonly IWaveSpawnerService _waveSpawnerService;
        private readonly IPauseService _pauseService;

        private IDisposable _disposable;
        
        public GameplayState(
            GameStateMachine gameStateMachine, 
            PlayerHealthProxy playerHealthProxy, 
            IWaveSpawnerService waveSpawnerService,
            IPauseService pauseService)
        {
            _gameStateMachine = gameStateMachine;
            _playerHealthProxy = playerHealthProxy;
            _waveSpawnerService = waveSpawnerService;
            _pauseService = pauseService;
        }
        
        public void Enter()
        {
            _pauseService.SetPause(false);
            
            SpawnNextWave();
            _waveSpawnerService.OnWaveCompleted += SpawnNextWave;
            _waveSpawnerService.OnAllWavesCompleted += WinGame;
            _disposable = _playerHealthProxy.Health.Subscribe(OnHealthChanged);
        }

        private void WinGame()
        {
            _gameStateMachine.EnterIn<WinGameState>();
        }
        
        private void OnHealthChanged(int health)
        {
            if(health <= 0)
                _gameStateMachine.EnterIn<LoseGameState>();
        }
        
        private void SpawnNextWave()
        {
            _waveSpawnerService.SpawnNextWave();
        }
        
        public void Exit()
        {
            _waveSpawnerService.OnWaveCompleted -= SpawnNextWave;
            _waveSpawnerService.OnAllWavesCompleted -= WinGame;
            _disposable.Dispose();
        }
    }
}