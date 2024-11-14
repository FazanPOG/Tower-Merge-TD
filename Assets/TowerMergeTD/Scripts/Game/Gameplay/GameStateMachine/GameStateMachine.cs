using System;
using System.Collections.Generic;
using TowerMergeTD.Game.State;
using TowerMergeTD.Utils;
using Zenject;

namespace TowerMergeTD.Game.Gameplay
{
    public class GameStateMachine
    {
        private readonly IGameStateService _gameStateService;
        private readonly Dictionary<Type, IGameState> _gameStatesMap;
        
        private IGameState _currentState;

        public GameStateMachine(
            DiContainer container,
            LevelConfig levelConfig,
            int currentLevelIndex,
            ITutorialBinder tutorialBinder)
        {
            var monoBehaviourWrapper = container.Resolve<MonoBehaviourWrapper>();
            var waveSpawnerServices = container.Resolve<IWaveSpawnerService[]>();
            var pauseService = container.Resolve<IPauseService>();
            var gameStateService = container.Resolve<IGameStateService>();
            var playerHealthProxy = container.Resolve<PlayerHealthProxy>();
            var gameTimerService = container.Resolve<IGameTimerService>();
            var scoreService = container.Resolve<IScoreService>();
            var rewardCalculatorService = container.Resolve<IRewardCalculatorService>();
            var gameStateProvider = container.Resolve<IGameStateProvider>();
            var currencyProvider = container.Resolve<ICurrencyProvider>();

            _gameStatesMap = new Dictionary<Type, IGameState>()
            {
                [typeof(BootState)] = new BootState(this, levelConfig),
                [typeof(TutorialState)] = new TutorialState(container, this, currentLevelIndex, tutorialBinder),
                [typeof(GameplayState)] = new GameplayState(this, playerHealthProxy, waveSpawnerServices, pauseService, gameTimerService),
                [typeof(WinGameState)] = new WinGameState(currentLevelIndex, pauseService, scoreService, rewardCalculatorService, gameStateProvider, currencyProvider),
                [typeof(LoseGameState)] = new LoseGameState(pauseService),
                [typeof(NoneState)] = new NoneState(),
            };
            
            _gameStateService = gameStateService;
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
