using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class ElectricTowerObjectView : TowerObjectView
    {
        [SerializeField] private Transform _electricSpawnTransform;

        public Transform ElectricSpawnTransform => _electricSpawnTransform;

        protected override void TowerAttackerOnAttacked() { }
    }
}