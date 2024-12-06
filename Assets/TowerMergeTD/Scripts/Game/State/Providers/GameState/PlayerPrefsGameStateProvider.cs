using System;
using System.Collections.Generic;
using System.Linq;
using GamePush;
using R3;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.GameRoot;
using TowerMergeTD.Utils;
using UnityEngine;

namespace TowerMergeTD.Game.State
{
    public class PlayerPrefsGameStateProvider : IGameStateProvider
    {
        private const string GAME_STATE_KEY = nameof(GAME_STATE_KEY);
        
        private readonly ProjectConfig _projectConfig;
        private readonly MonoBehaviourWrapper _monoBehaviourWrapper;

        private GameState _gameStateOrigin;
        
        public GameStateProxy GameState { get; private set; }

        public PlayerPrefsGameStateProvider(ProjectConfig projectConfig, MonoBehaviourWrapper monoBehaviourWrapper)
        {
            _projectConfig = projectConfig;
            _monoBehaviourWrapper = monoBehaviourWrapper;
            
            _monoBehaviourWrapper.OnDestroyed += OnDestroyed;
        }

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

                if (_gameStateOrigin.LevelDatas.Count < _projectConfig.Levels.Length)
                {
                    for (var i = 0; i < _projectConfig.Levels.Length; i++)
                    {
                        if (_gameStateOrigin.LevelDatas.Any(x => x.ID == i) == false)
                            CreateLevelSaveDataToOrigin(_projectConfig.Levels[i].LevelConfig.IsOpen);
                    }
                }

                if (_gameStateOrigin.LevelDatas.Count > _projectConfig.Levels.Length)
                {
                    for (int i = _gameStateOrigin.LevelDatas.Count - 1; i >= _projectConfig.Levels.Length; i--)
                    {
                        Debug.Log($"RemoveData: {_gameStateOrigin.LevelDatas[i].ID}");
                        RemoveLevelSaveDataFromOrigin(_gameStateOrigin.LevelDatas[i]);
                    }
                }

                GameState = new GameStateProxy(_gameStateOrigin);
                
                Debug.Log($"Game State loaded: {JsonUtility.ToJson(_gameStateOrigin, true)}");
            }
            
            return Observable.Return(GameState);
        }

        public Observable<bool> SaveGameState()
        {
            _gameStateOrigin.LevelDatas.Sort();
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

        private void CreateLevelSaveDataToOrigin(bool isOpen)
        {
            var levelData = new LevelSaveData()
            {
                ID = _gameStateOrigin.LevelDatas.Count,
                IsOpen = isOpen,
                Score = 0
            };

            _gameStateOrigin.LevelDatas.Add(levelData);
            SaveGameState();
        }

        private void RemoveLevelSaveDataFromOrigin(LevelSaveData saveData)
        {
            _gameStateOrigin.LevelDatas.Remove(saveData);
            SaveGameState();
        }

        private GameStateProxy CreateGameStateFromSettings()
        {
            List<LevelSaveData> levelSaveDatas = new List<LevelSaveData>();
            List<TowerType> towerTypes = new List<TowerType>();
            List<string> shopPurchasedItemIDs = new List<string>();
            
            for (int i = 0; i < _projectConfig.Levels.Length; i++)
            {
                levelSaveDatas.Add(new LevelSaveData()
                {
                    ID = i,
                    IsOpen = _projectConfig.Levels[i].LevelConfig.IsOpen,
                    Score = 0
                });
            }
            
            towerTypes.Add(TowerType.Gun);
            
            _gameStateOrigin = new GameState()
            {
                LevelDatas = levelSaveDatas,
                UnlockTowers = towerTypes,
                ShopPurchasedItemIDs = shopPurchasedItemIDs,
                MusicVolume = 0.3f,
                SoundVolume = 0.3f
            };
            
            return new GameStateProxy(_gameStateOrigin);
        }

        private void OnDestroyed()
        {
            GameState.LastExitTime.Value = DateTime.Now;
        }
    }
}