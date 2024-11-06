using System.Linq;
using ObservableCollections;
using R3;

namespace TowerMergeTD.Game.State
{
    public class GameStateProxy
    {
        public ObservableList<LevelSaveDataProxy> LevelDatas { get; } = new ObservableList<LevelSaveDataProxy>();

        public GameStateProxy(GameState gameState)
        {
            gameState.LevelDatas.ForEach(levelData => { LevelDatas.Add(new LevelSaveDataProxy(levelData)); });

            LevelDatas.ObserveAdd().Subscribe(e =>
            {
                var newData = e.Value;
                gameState.LevelDatas.Add(new LevelSaveData
                {
                    ID = newData.ID,
                    IsOpen = newData.IsOpen,
                    Score = newData.Score,
                });
            });

            LevelDatas.ObserveRemove().Subscribe(e =>
            {
                var removedLevelData = e.Value;
                var levelData = gameState.LevelDatas.FirstOrDefault(x => x.ID == removedLevelData.ID);
                gameState.LevelDatas.Remove(levelData);
            });
        }
    }
}