using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class RocketTowerObjectView : TowerObjectView
    {
        [SerializeField] private Transform rocketSpawnTransform;

        public Transform RocketSpawnTransform => rocketSpawnTransform;

        protected override void TowerAttackerOnAttacked()
        {
            
        }
    }
}