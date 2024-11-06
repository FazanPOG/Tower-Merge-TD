using System;

namespace TowerMergeTD.Game.State
{
    [Serializable]
    public class LevelSaveData
    {
        public int ID;
        public bool IsOpen;
        public int Score;
    }
}