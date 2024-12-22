using System.Collections.Generic;
using System.Linq;
//using GamePush;
using R3;
using TowerMergeTD.GameRoot;
using TowerMergeTD.Utils;
using UnityEngine;

namespace TowerMergeTD.Game.State
{
    public class GamePushGameStateProvider : IGameStateProvider
    {
        private const string GAME_STATE_KEY = nameof(GAME_STATE_KEY);
        
        private readonly ProjectConfig _projectConfig;

        private GameState _gameStateOrigin;
        
        public GameStateProxy GameState { get; private set; }

        public GamePushGameStateProvider(ProjectConfig projectConfig, MonoBehaviourWrapper monoBehaviourWrapper)
        {
            _projectConfig = projectConfig;
            
            monoBehaviourWrapper.OnDestroyed += OnDestroyed;
        }

        public Observable<GameStateProxy> LoadGameState()
        {
            /*
            if (string.IsNullOrEmpty(GP_Player.GetString(GAME_STATE_KEY)))
            {
                GameState = CreateGameStateFromSettings();
                SaveGameState();
            }
            else
            {
                var json = GP_Player.GetString(GAME_STATE_KEY);
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
                        RemoveLevelSaveDataFromOrigin(_gameStateOrigin.LevelDatas[i]);
                    }
                }

                GameState = new GameStateProxy(_gameStateOrigin);
            }
            
            GP_Player.Sync();
            */
            return Observable.Return(GameState);
        }

        //TODO: string does not save

        public Observable<bool> SaveGameState()
        {
            /*
            _gameStateOrigin.LevelDatas.Sort();
            var json = JsonUtility.ToJson(_gameStateOrigin, true);
            Debug.Log("SaveGameState");
            
            GP_Player.Set(GAME_STATE_KEY, json);
            GP_Player.Set("test123", "test123");
            
            GP_Player.Sync(SyncStorageType.cloud);
            GP_Player.Sync(SyncStorageType.local);
            
            Debug.Log(GP_Player.GetString(GAME_STATE_KEY));
            Debug.Log(GP_Player.GetString("test123"));
            */
            return Observable.Return(true);
        }

        public Observable<bool> ResetGameState()
        {
            //GP_Player.ResetPlayer();
            
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
            List<LevelSaveData> datas = new List<LevelSaveData>();
            
            for (int i = 0; i < _projectConfig.Levels.Length; i++)
            {
                datas.Add(new LevelSaveData()
                {
                    ID = i,
                    IsOpen = _projectConfig.Levels[i].LevelConfig.IsOpen,
                    Score = 0
                });
            }
            
            _gameStateOrigin = new GameState()
            {
                LevelDatas = datas
            };

            return new GameStateProxy(_gameStateOrigin);
        }

        private void OnDestroyed()
        {
            //GameState.LastExitTime.Value = GP_Server.Time();
        }
    }
}