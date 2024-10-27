using System;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public interface ITowerAttacker
    {
        event Action<GameObject> OnTargetChanged;
        void Init(float initialDamage, float attackRange, float attackCooldown);
    }
}