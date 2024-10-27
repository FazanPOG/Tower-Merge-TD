﻿using Sirenix.OdinInspector;
using UnityEngine;

namespace TowerMergeTD.Scripts.Game.Gameplay.Configs
{
    [CreateAssetMenu(menuName = "Configs/Enemy", order = 1)]
    public class EnemyConfig : ScriptableObject
    {
        [BoxGroup("Settings")] [Range(1, 500)]
        [SerializeField] private float _health = 1f;

        [BoxGroup("Settings")] [Range(1, 20)]
        [SerializeField] private float _moveSpeed = 1f;

        [BoxGroup("Visuals")] [PreviewField(75, ObjectFieldAlignment.Center)] [HideLabel]
        [SerializeField] private Sprite _sprite;

        public float Health => _health;

        public float MoveSpeed => _moveSpeed;

        public Sprite Sprite => _sprite;
    }
}