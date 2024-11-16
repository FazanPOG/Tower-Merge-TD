using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class LaserTowerObjectView : TowerObjectView
    {
        [SerializeField] private Transform _towerTransform;
        [SerializeField] private GameObject _laser;
        [SerializeField] private SpriteRenderer _laserSpriteRenderer;

        private TowerLaserAttacker _towerAttacker;
        private float _attackRange;
        
        public override void Init(TowerDataProxy data, ITowerAttacker towerAttacker)
        {
            base.Init(data, towerAttacker);

            _attackRange = data.AttackRange;
            _towerAttacker = towerAttacker as TowerLaserAttacker;
        }

        protected override void TowerAttackerOnAttacked()
        {
            _laser.gameObject.SetActive(true);
        }

        protected override void Update()
        {
            base.Update();
            
            if (_towerAttacker.ClosestTargetObject != null && _towerAttacker.OnDrag == false)
            {
                Vector3 direction = _towerAttacker.ClosestTargetObject.transform.position - _towerTransform.position;
                float distance = direction.magnitude;
                
                if (distance <= _attackRange)
                {
                    if(_laser.gameObject.activeSelf == false)
                        _laser.gameObject.SetActive(true);
                    
                    _laserSpriteRenderer.size = new Vector2(_laserSpriteRenderer.size.x, distance / 3);
                    var scale = _laserSpriteRenderer.transform.localScale;
                    scale = new Vector3(scale.x, distance / 2.9f, scale.z);
                    _laserSpriteRenderer.transform.localScale = scale;
                }
                else
                {
                    _laser.gameObject.SetActive(false);
                }
            }
            else
            {
                _laser.gameObject.SetActive(false);
            }
        }
    }
}
