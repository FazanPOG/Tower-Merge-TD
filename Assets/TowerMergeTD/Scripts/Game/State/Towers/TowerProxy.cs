using R3;
using TowerMergeTD.Game.Gameplay;
using UnityEngine;

namespace TowerMergeTD.Game.State
{
    public class TowerProxy
    {
        public int ID { get; }
        public TowerType Type { get; }
        public ReactiveProperty<Vector2Int> Position { get; }
        public ReactiveProperty<int> Level { get; }

        public TowerProxy(Tower tower)
        {
            ID = tower.ID;
            Type = tower.Type;
            Position = new ReactiveProperty<Vector2Int>(tower.Position);
            Level = new ReactiveProperty<int>(tower.Level);

            Position.Skip(1).Subscribe(value => { tower.Position = value; });
            Level.Skip(1).Subscribe(value => { tower.Level = value; });
        }
    }
}