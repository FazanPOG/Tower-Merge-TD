using System;
using System.Collections.Generic;
using TowerMergeTD.Game.Gameplay;

namespace TowerMergeTD.Game.State
{
    [Serializable]
    public class GameState
    {
        public List<LevelSaveData> LevelDatas;
        public List<TowerType> UnlockTowers;
        public List<string> ShopPurchasedItemIDs;
        public DateTime LastExitTime;
    }
}