using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class TowerObject : MonoBehaviour
    {
        [SerializeField] private TowerCollisionHandler _collisionHandler;
        [SerializeField] private TowerObjectView _view;
        [SerializeField, TextArea(0, 10)] private string DEBUG_STRING;

        private TowerGenerationConfig _generation;
        private TowerDataProxy _dataProxy;
        private TowerProxy _towerProxy;
        private MapCoordinator _mapCoordinator;
        private DragAndDrop _draggable;
        private ITowerAttacker _attacker;
        private ObjectRotator _rotator;
        private Vector3 _rotateTarget;

        public TowerType Type => _generation.TowersType;
        public int Level => _towerProxy.Level.Value;
        public TowerCollisionHandler CollisionHandler => _collisionHandler;

        private void Update()
        {
            DEBUG_STRING = "";

            if (_generation == null) return;

            DEBUG_STRING += $"Type: {_generation.TowersType} \n";
            
            if (_dataProxy == null) return;
            
            DEBUG_STRING += $"Level: {_dataProxy.Level} \n" +
                            $"Damage: {_dataProxy.Damage} \n" +
                            $"Attack range: {_dataProxy.AttackRange} \n" +
                            $"Attack cooldown: {_dataProxy.AttackCooldown} \n";
        }

        public void Init(InputHandler inputHandler, TowerGenerationConfig generation, TowerProxy proxy, MapCoordinator mapCoordinator, ITowerAttacker attacker)
        {
            _generation = generation;
            _towerProxy = proxy;
            _mapCoordinator = mapCoordinator;
            _attacker = attacker;
            
            _draggable = _collisionHandler.gameObject.AddComponent<DragAndDrop>();
            _draggable.Init(inputHandler, transform, _mapCoordinator);

            _dataProxy = _generation.GetTowerDataProxy(_towerProxy.Level.CurrentValue);
            _attacker.Init(
                _dataProxy.Damage, 
                _dataProxy.AttackRange,
                _dataProxy.AttackCooldown
                );

            _view.Init(_dataProxy);
            
            //TODO: inject
            _rotator = new ObjectRotator(_view.gameObject.transform);

            _draggable.OnDroppedOnTileMap += UpdateModel;
            _draggable.OnDroppedOnTower += OnDroppedOnTower;
            _attacker.OnTargetChanged += AttackerOnTargetChanged;
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }
        
        private void OnDroppedOnTower(TowerObject towerObject)
        {
            bool merged = MergeHandler.TryMerge(_generation, this, towerObject);
            
            if(merged == false)
                _draggable.ResetPosition();
        }

        private void AttackerOnTargetChanged(GameObject target)
        {
            _rotator.StartRotate(this, target.transform.position);
        }

        private void UpdateModel()
        {
            _towerProxy.Position.Value = _mapCoordinator.GetCellPosition(TilemapType.Base, transform.position);
        }

        private void OnDisable()
        {
            _draggable.OnDroppedOnTileMap -= UpdateModel;
            _draggable.OnDroppedOnTower -= OnDroppedOnTower;
            _attacker.OnTargetChanged -= AttackerOnTargetChanged;
        }
    }
}
