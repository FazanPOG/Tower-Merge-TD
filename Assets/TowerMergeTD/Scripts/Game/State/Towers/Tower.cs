using System;
using TowerMergeTD.Game.Gameplay;
using UnityEngine;

namespace TowerMergeTD.Game.State
{
    [Serializable]
    public class Tower
    {
        public int ID;
        public TowerType Type;
        public Vector2Int Position;
        public int Level;
    }
}