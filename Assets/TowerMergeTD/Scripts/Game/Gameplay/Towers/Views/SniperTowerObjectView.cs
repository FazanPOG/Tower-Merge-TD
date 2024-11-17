using TowerMergeTD.Game.State;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class SniperTowerObjectView : TowerObjectView
    {
        [SerializeField] private Transform _towerTransform;
        [SerializeField] private GameObject _laserObject;

        private float _attackRange;
        
        public override void Init(TowerDataProxy data, ITowerAttacker towerAttacker)
        {
            base.Init(data, towerAttacker);

            _attackRange = data.AttackRange;
            
            _laserObject.gameObject.SetActive(true);
            _laserObject.transform.localScale = new Vector3(_laserObject.transform.localScale.x, _attackRange, _laserObject.transform.localScale.z);
        }

        protected override void TowerAttackerOnAttacked() { }
    }
}
