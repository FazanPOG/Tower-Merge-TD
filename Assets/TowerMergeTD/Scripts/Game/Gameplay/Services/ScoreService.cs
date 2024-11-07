namespace TowerMergeTD.Game.Gameplay
{
    public class ScoreService : IScoreService
    {
        private int _score;

        public int Score => _score;

        public ScoreService()
        {
            _score = 0;
        }
        
        public void AddScore(int score)
        {
            if(score < 0)
                return;
            
            _score += score;
        }
    }
}