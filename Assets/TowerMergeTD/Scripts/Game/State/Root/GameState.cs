using System;
using System.Collections.Generic;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;

namespace TowerMergeTD.Game.State
{
    [Serializable]
    public class GameState
    {
        public List<LevelSaveData> LevelDatas;
        public List<TowerType> UnlockTowers;
        public List<string> ShopPurchasedItemIDs;
    }
}