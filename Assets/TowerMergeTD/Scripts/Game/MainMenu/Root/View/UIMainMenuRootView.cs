using System.Linq;
using R3;
using TowerMergeTD.Game.MainMenu;
using TowerMergeTD.Game.State;
using TowerMergeTD.GameRoot;
using UnityEngine;

namespace TowerMergeTD.MainMenu.Root
{
    public class UIMainMenuRootView : MonoBehaviour
    {
        [SerializeField] private LevelEntryView _levelEntryViewPrefab;
        [SerializeField] private Transform _levelEntryViewParent;
        
        private ReactiveProperty<int> _exitSceneSignalBus;
        private ProjectConfig _projectConfig;
        private IGameStateProvider _gameStateProvider;

        public void HandleGoToGameplayButtonClicked(int levelNumber)
        {
            _exitSceneSignalBus?.OnNext(levelNumber);
        }

        public void Bind(ReactiveProperty<int> exitSceneSignalBus, ProjectConfig projectConfig, IGameStateProvider gameStateProvider)
        {
            _exitSceneSignalBus = exitSceneSignalBus;
            _projectConfig = projectConfig;
            _gameStateProvider = gameStateProvider;
            
            BindAdapters();
        }

        private void BindAdapters()
        {
            for (var i = 0; i < _projectConfig.Levels.Length; i++)
            {
                var levelSaveDataProxy = _gameStateProvider.GameState.LevelDatas[i];
                var viewInstance = Instantiate(_levelEntryViewPrefab, _levelEntryViewParent);
                var levelConfig = _projectConfig.Levels[i].LevelConfig;
                new LevelEntryViewAdapter(_projectConfig.IsDevelopmentSettings, viewInstance, levelSaveDataProxy, levelConfig, HandleGoToGameplayButtonClicked);
            }
        }
    }
}
