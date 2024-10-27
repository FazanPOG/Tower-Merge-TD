using System;
using UnityEngine;

namespace TowerMergeTD.Game.State
{
    [Serializable]
    public class Tower
    {
        public int ID;
        public string Type;
        public Vector2Int Position;
        public int Level;
    }
}