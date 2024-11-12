using System;
using R3;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public interface ITowerAttacker
    {
        event Action OnAttacked;
        event Action<GameObject> OnTargetChanged;
        void Init(float initialDamage, float attackRange, float attackCooldown, ReadOnlyReactiveProperty<bool> isDragging);
    }
}