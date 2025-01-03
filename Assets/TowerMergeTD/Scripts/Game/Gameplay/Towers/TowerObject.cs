using TowerMergeTD.Game.Audio;
using TowerMergeTD.Game.State;
using UnityEngine;
using AudioType = TowerMergeTD.Game.Audio.AudioType;

namespace TowerMergeTD.Game.Gameplay
{
    public class TowerObject : MonoBehaviour
    {
        [SerializeField] private TowerGenerationConfig _generation;
        [SerializeField] private TowerCollisionHandler _collisionHandler;
        [SerializeField] private TowerObjectView _view;

        private TowerDataProxy _dataProxy;
        private TowerProxy _towerProxy;
        private MapCoordinator _mapCoordinator;
        private DragAndDrop _dragAndDrop;
        private ITowerAttacker _attacker;
        private IPauseService _pauseService;
        private AudioPlayer _audioPlayer;
        private Vector3 _rotateTarget;

        public TowerType Type => _generation.TowersType;
        public int Level => _towerProxy.Level.Value;
        public TowerCollisionHandler CollisionHandler => _collisionHandler;
        public TowerObjectView View => _view;
        
        public void Init(
            IInput input, 
            TowerProxy proxy, 
            MapCoordinator mapCoordinator, 
            ITowerAttacker attacker,
            IPauseService pauseService,
            AudioPlayer audioPlayer)
        {
            _towerProxy = proxy;
            _mapCoordinator = mapCoordinator;
            _attacker = attacker;
            _pauseService = pauseService;
            _audioPlayer = audioPlayer;
            
            _dragAndDrop = _collisionHandler.gameObject.AddComponent<DragAndDrop>();
            _dragAndDrop.Init(input, transform, _mapCoordinator);

            _dataProxy = _generation.GetTowerDataProxy(_towerProxy.Level.CurrentValue);
            _attacker.Init(_dataProxy.Damage, _dataProxy.AttackRange, _dataProxy.AttackCooldown, _dragAndDrop.IsDragging);

            _view.Init(_dataProxy, _attacker, _audioPlayer);

            _pauseService.Register(_dragAndDrop);
            
            _dragAndDrop.OnDroppedOnTileMap += UpdateModel;
            _dragAndDrop.OnDroppedOnTower += OnDroppedOnTower;
            
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
        
        private void OnDroppedOnTower(TowerObject towerObject)
        {
            bool merged = MergeHandler.TryMerge(_generation, this, towerObject);
            
            if(merged == false)
                _dragAndDrop.ResetPosition();
            else
                _audioPlayer.Play(AudioType.MergeTowers);
        }

        private void UpdateModel()
        {
            _towerProxy.Position.Value = _mapCoordinator.GetCellPosition(TilemapType.Base, transform.position);
        }

        private void OnDisable()
        {
            _dragAndDrop.OnDroppedOnTileMap -= UpdateModel;
            _dragAndDrop.OnDroppedOnTower -= OnDroppedOnTower;
            _pauseService.Unregister(_dragAndDrop);
        }
    }
}
