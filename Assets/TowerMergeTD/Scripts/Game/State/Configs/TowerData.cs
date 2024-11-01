using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TowerMergeTD.Game.State
{
    [Serializable]
    public class TowerData
    {
        [BoxGroup("Settings")] [Range(1, 20)] [LabelText("Attack Range")] [Tooltip("Defines the range of the tower's attack.")]
        public float AttackRange = 1f;

        [BoxGroup("Settings")] [LabelText("Tower Attack Cooldown")] [Tooltip("Initial tower attack cooldown.")]
        public float AttackCooldown;
        
        [BoxGroup("Settings")] [LabelText("Tower Damage")] [Tooltip("Initial tower damage.")]
        public float Damage;
        
        [BoxGroup("Settings")] [LabelText("Tower Level")] [Tooltip("Current level of the tower.")]
        public int Level;

        [BoxGroup("Visuals")] [PreviewField(75, ObjectFieldAlignment.Center)] [HideLabel] [Tooltip("Sprite for the tower.")]
        public Sprite Sprite;
    }
}