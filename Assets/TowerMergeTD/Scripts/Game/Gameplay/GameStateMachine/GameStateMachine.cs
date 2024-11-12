using System;
using System.Collections.Generic;
using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.Gameplay
{
    public class GameStateMachine
    {
        private readonly IGameStateService _gameStateService;
        private readonly Dictionary<Type, IGameState> _gameStatesMap;
        
        private IGameState _currentState;

        public GameStateMachine(
            int currentLevelIndex,
            IWaveSpawnerService[] waveSpawnerServices, 
            IPauseService pauseService, 
            PlayerHealthProxy playerHealthProxy,
            IGameStateService gameStateService,
            IGameTimerService gameTimerService,
            IScoreService scoreService,
            IGameStateProvider gameStateProvider)
        {
            _gameStateService = gameStateService;
            _gameStatesMap = new Dictionary<Type, IGameState>()
            {
                [typeof(BootState)] = new BootState(this),
                [typeof(GameplayState)] = new GameplayState(this, playerHealthProxy, waveSpawnerServices, pauseService, gameTimerService),
                [typeof(WinGameState)] = new WinGameState(currentLevelIndex, pauseService, scoreService, gameStateProvider),
                [typeof(LoseGameState)] = new LoseGameState(pauseService),
            };
        }

        public void EnterIn<T>() where T : IGameState
        {
            if(_gameStatesMap.TryGetValue(typeof(T), out IGameState state) == false)
                throw new MissingMemberException($"Missing state in dictionary: {typeof(T)}");

            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
            
            _gameStateService.ChangeState(state);
        }
    }
}
