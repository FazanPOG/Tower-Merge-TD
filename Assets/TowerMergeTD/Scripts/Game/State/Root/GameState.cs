using System;
using System.Collections.Generic;
using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.State
{
    [Serializable]
    public class GameState
    {
        public List<LevelSaveData> LevelDatas;
    }
}