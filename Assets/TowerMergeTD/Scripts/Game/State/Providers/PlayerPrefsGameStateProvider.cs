using System.Collections.Generic;
using R3;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;
using TowerMergeTD.Scripts.Game.State.Root;
using UnityEngine;

namespace TowerMergeTD.Scripts.Game.State
{
    public class PlayerPrefsGameStateProvider : IGameStateProvider
    {
        private const string GAME_STATE_KEY = nameof(GAME_STATE_KEY);
        
        public GameStateProxy GameState { get; private set; }

        private GameState _gameStateOrigin;
        
        public Observable<GameStateProxy> LoadGameState()
        {
            if (PlayerPrefs.HasKey(GAME_STATE_KEY) == false)
            {
                GameState = CreateGameStateFromSettings();
                Debug.Log($"Game State created from settings: {JsonUtility.ToJson(_gameStateOrigin, true)}");
                
                SaveGameState();
            }
            else
            {
                var json = PlayerPrefs.GetString(GAME_STATE_KEY);
                _gameStateOrigin = JsonUtility.FromJson<GameState>(json);
                GameState = new GameStateProxy(_gameStateOrigin);
                
                Debug.Log($"Game state loaded: {json}");
            }

            return Observable.Return(GameState);
        }

        public Observable<bool> SaveGameState()
        {
            var json = JsonUtility.ToJson(_gameStateOrigin, true);
            PlayerPrefs.SetString(GAME_STATE_KEY, json);
            PlayerPrefs.Save();
            
            return Observable.Return(true);
        }

        public Observable<bool> ResetGameState()
        {
            GameState = CreateGameStateFromSettings();
            SaveGameState();
            
            return Observable.Return(true);
        }

        private GameStateProxy CreateGameStateFromSettings()
        {
            _gameStateOrigin = new GameState()
                {
                    Towers = new List<Tower>()
                    {
                        new Tower()
                        {
                            Type = TowerType.Gun,
                        },
                        new Tower()
                        {
                            Type = TowerType.Laser,
                        }
                    }
                };
            
            return new GameStateProxy(_gameStateOrigin);
        }
    }
}