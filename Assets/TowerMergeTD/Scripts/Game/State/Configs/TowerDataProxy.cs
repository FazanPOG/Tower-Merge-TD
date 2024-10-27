using UnityEngine;

namespace TowerMergeTD.Game.State
{
    public class TowerDataProxy
    {
        public float AttackRange { get; }

        public float AttackCooldown { get; }
        
        public float Damage { get; }

        public int Level { get; }

        public Sprite Sprite { get; }
        
        public TowerDataProxy(TowerData data)
        {
            Sprite = data.Sprite;
            Level = data.Level;
            Damage = data.Damage;
            AttackCooldown = data.AttackCooldown;
            AttackRange = data.AttackRange;
        }
    }
}