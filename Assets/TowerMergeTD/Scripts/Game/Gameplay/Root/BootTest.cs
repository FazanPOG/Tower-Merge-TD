using System.Collections.Generic;
using System.Linq;
using TowerMergeTD.Game.State;
using TowerMergeTD.Game.UI;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace TowerMergeTD.Game.Gameplay
{
    public class BootTest : MonoBehaviour
    {
        [SerializeField] private TileSetConfig _tileSetConfig;
        [SerializeField] private TowerObject _prefab;
        [SerializeField] private Transform _towerParent;
        [SerializeField] private TowerGenerationConfig _gunTowerGeneration;
        [SerializeField] private TowerGenerationConfig _rocketTowerGeneration;
        [SerializeField] private Tilemap _baseTileMap;
        [SerializeField] private Tilemap _environmentTileMap;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Transform _enemyParent;
        [SerializeField] private Transform _enemySpawnPosition;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private TowerObject[] _gunTowers;
        [SerializeField] private TowerObject[] _rocketTowers;
        [Space(15)] 
        [SerializeField] private Transform[] _pathPoints;
        [SerializeField] private EnemyFinishTrigger _enemyFinish;
        [SerializeField] private PlayerHealthView _playerHealthView;
        [SerializeField] private TowerActionsView _towerActionsView;

        private TilemapCoordinator _tilemapCoordinator;
        private InputHandler _inputHandler;
        private IWaveSpawnerService _waveSpawnerService;
        private EnemyFactory _enemyFactory;
        private PlayerHealthProxy _playerHealthProxy;
        private PlayerMoneyProxy _playerMoneyProxy;
        
        private void Awake()
        {
            _tilemapCoordinator = new TilemapCoordinator(_baseTileMap, _environmentTileMap, _tileSetConfig);
            
            InitPlayer();
            InitTowers();
            InitEnemies();
            InitUI();
            
            _waveSpawnerService.SpawnNextWave();
            _waveSpawnerService.OnWaveCompleted += () => { _waveSpawnerService.SpawnNextWave(); };
            _waveSpawnerService.OnAllWavesCompleted += () => { Debug.Log("LEVEL COMPLETE!"); };
        }

        private void InitPlayer()
        {
            _inputHandler = new InputHandler();

            PlayerHealth health = new PlayerHealth(_gameConfig.InitialHealth);
            _playerHealthProxy = new PlayerHealthProxy(health);
            
            PlayerMoney playerMoney = new PlayerMoney(_gameConfig.InitialMoney);
            _playerMoneyProxy = new PlayerMoneyProxy(playerMoney);
        }

        private void InitTowers()
        {
            var container = new DiContainer();
            var towerFactory = new TowerFactory(container, _prefab, _tilemapCoordinator, _towerParent, this);

            MergeHandler.Init(towerFactory, _inputHandler);
            
            foreach (var tower in _gunTowers)
            {
                Tower model = new Tower()
                {
                    ID = tower.GetInstanceID(),
                    Level = 1,
                    Position = _tilemapCoordinator.GetCellPosition(TilemapType.Base, tower.transform.position),
                    Type = _gunTowerGeneration.TowersType
                };
                
                TowerProxy proxy = new TowerProxy(model);
                ITowerAttacker attacker = new TowerRegularAttacker(tower.CollisionHandler, this);
                
                tower.Init(_inputHandler, _gunTowerGeneration, proxy, _tilemapCoordinator, attacker);
            }
            
            foreach (var tower in _rocketTowers)
            {
                Tower model = new Tower()
                {
                    ID = tower.GetInstanceID(),
                    Level = 1,
                    Position = _tilemapCoordinator.GetCellPosition(TilemapType.Base, tower.transform.position),
                    Type = _rocketTowerGeneration.TowersType
                };
                
                TowerProxy proxy = new TowerProxy(model);
                ITowerAttacker attacker = new TowerRegularAttacker(tower.CollisionHandler, this);
                
                tower.Init(_inputHandler, _rocketTowerGeneration, proxy, _tilemapCoordinator, attacker);
            }
        }

        private void InitEnemies()
        {
            DiContainer container = new DiContainer();
            
            _enemyFactory = new EnemyFactory(container, _enemyPrefab, _enemyParent);
            List<Vector3> path = _pathPoints.Select(x => x.position).ToList();
            _waveSpawnerService = new WaveSpawnerService(_enemyFactory, _gameConfig.Waves, path, _enemySpawnPosition.transform.position, this);
            _enemyFinish.Init(_playerHealthProxy);
        }

        private void InitUI()
        {
            TowerActionsAdapter actionsAdapter = new TowerActionsAdapter(_towerActionsView, _inputHandler,  _tilemapCoordinator, _playerMoneyProxy);
            
            
            _playerHealthView.Init(_playerHealthProxy);
        }
    }
}