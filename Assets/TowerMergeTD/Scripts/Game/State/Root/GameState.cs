using System;
using System.Collections.Generic;
using TowerMergeTD.Game.State;

namespace TowerMergeTD.Scripts.Game.State.Root
{
    [Serializable]
    public class GameState
    {
        public List<Tower> Towers;
    }
}