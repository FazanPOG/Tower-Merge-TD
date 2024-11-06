namespace TowerMergeTD.Game.State
{
    public class LevelSaveDataProxy
    {
        public int ID { get; }
        public bool IsOpen { get; }
        public int Score { get; }
        
        public LevelSaveDataProxy(LevelSaveData levelSaveData)
        {
            ID = levelSaveData.ID;
            IsOpen = levelSaveData.IsOpen;
            Score = levelSaveData.Score;
        }
    }
}