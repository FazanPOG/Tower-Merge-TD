using R3;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;
using TowerMergeTD.GameRoot;
using TowerMergeTD.MainMenu.Root;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace TowerMergeTD.Gameplay.Root
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        //TODO: after complete level creator, ref to level prefab, instead this
        [SerializeField] private UIGameplayRootView _uiGameplayRootPrefab;
        [SerializeField] private TileSetConfig _tileSetConfig;
        [SerializeField] private Tilemap _baseTileMap;
        [SerializeField] private Tilemap _environmentTileMap;
        [SerializeField] private Transform _towersParent;
        [SerializeField] private Transform _enemiesParent;
        [SerializeField] private Transform[] _pathPoints;
        [SerializeField] private EnemyFinishTrigger _enemyFinish;
        [SerializeField] private Transform _enemySpawnPosition;

        public Observable<GameplayExitParams> Run(DiContainer gameplayContainer, GameplayEnterParams gameplayEnterParams)
        {
            gameplayContainer.UnbindAll();
            
            var uiRoot = gameplayContainer.Resolve<UIRootView>();
            var uiGameplayRoot = Instantiate(_uiGameplayRootPrefab);
            uiRoot.AttackSceneUI(uiGameplayRoot.gameObject);
            
            var exitSceneSignalSubj = new Subject<Unit>();

            var gameplayBinder = new GameplayBinder(gameplayContainer);
            gameplayBinder.Bind(
                gameplayEnterParams.LevelConfig, 
                _baseTileMap, 
                _environmentTileMap, 
                _tileSetConfig, 
                _towersParent, 
                _enemiesParent, 
                _pathPoints,
                _enemySpawnPosition.transform.position,
                _enemyFinish);
            
            uiGameplayRoot.Bind(exitSceneSignalSubj, gameplayContainer);

            Debug.Log($"GAMEPLAY ENTER PARAMS: GameConfig: {gameplayEnterParams.LevelConfig.name}");
            StartGameplay(gameplayContainer);
            
            var mainMenuEnterParams = new MainMenuEnterParams("result");
            var exitParams = new GameplayExitParams(mainMenuEnterParams);

            var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);
            return exitToMainMenuSceneSignal;
        }

        private void StartGameplay(DiContainer container)
        {
            IWaveSpawnerService waveSpawnerService = container.Resolve<IWaveSpawnerService>();
            
            waveSpawnerService.SpawnNextWave();
            waveSpawnerService.OnWaveCompleted += () => { waveSpawnerService.SpawnNextWave(); };
            waveSpawnerService.OnAllWavesCompleted += () => { Debug.Log("---Level complete!---"); };
        }
    }
}