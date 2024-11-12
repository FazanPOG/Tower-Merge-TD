using System;

namespace TowerMergeTD.Game.State
{
    [Serializable]
    public class LevelSaveData : IComparable<LevelSaveData>
    {
        public int ID;
        public bool IsOpen;
        public int Score;

        public int CompareTo(LevelSaveData other)
        {
            if (other == null) return 1;

            return ID.CompareTo(other.ID); 
        }
    }
}