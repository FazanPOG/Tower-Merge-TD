using System;
using System.Collections.Generic;
using R3;
using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.Gameplay
{
    public class GameStateMachine : IGameStateObservable
    {
        private Dictionary<Type, IGameState> _gameStatesMap;
        private ReactiveProperty<IGameState> _currentState = new ReactiveProperty<IGameState>();

        public Observable<IGameState> GameState => _currentState;

        public GameStateMachine(IWaveSpawnerService waveSpawnerService, IPauseService pauseService, PlayerHealthProxy playerHealthProxy)
        {
            _gameStatesMap = new Dictionary<Type, IGameState>()
            {
                [typeof(BootState)] = new BootState(this),
                [typeof(GameplayState)] = new GameplayState(this, playerHealthProxy, waveSpawnerService, pauseService),
                [typeof(WinGameState)] = new WinGameState(pauseService),
                [typeof(LoseGameState)] = new LoseGameState(pauseService),
            };
        }

        public void EnterIn<T>() where T : IGameState
        {
            if(_gameStatesMap.TryGetValue(typeof(T), out IGameState state) == false)
                throw new MissingMemberException($"Missing state in dictionary: {typeof(T)}");

            _currentState.Value?.Exit();
            _currentState.Value = state;
            _currentState.Value.Enter();
        }
    }
}
