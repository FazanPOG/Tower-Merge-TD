using TowerMergeTD.Game.Gameplay;

namespace TowerMergeTD.Game.UI
{
    public class GameSpeedViewAdapter
    {
        private readonly IGameSpeedService _gameSpeedService;
        private readonly GameSpeedView _gameSpeedView;

        private GameSpeed _currentGameSpeed;
        
        public GameSpeedViewAdapter(IGameSpeedService gameSpeedService, GameSpeedView gameSpeedView)
        {
            _gameSpeedService = gameSpeedService;
            _gameSpeedView = gameSpeedView;
            
            _currentGameSpeed = GameSpeed.X1;
            _gameSpeedView.SetSpeedText(_currentGameSpeed.ToString());
            
            _gameSpeedView.OnSpeedButtonClicked += OnSpeedButtonClicked;
        }

        private void OnSpeedButtonClicked()
        {
            switch (_currentGameSpeed)
            {
                case GameSpeed.X1:
                    _currentGameSpeed = GameSpeed.X2;
                    break;
                
                case GameSpeed.X2:
                    _currentGameSpeed = GameSpeed.X4;
                    break;
                
                case GameSpeed.X4:
                    _currentGameSpeed = GameSpeed.X8;
                    break;
                
                case GameSpeed.X8:
                    _currentGameSpeed = GameSpeed.X1;
                    break;
            }
            
            _gameSpeedService.SetSpeed(_currentGameSpeed);
            _gameSpeedView.SetSpeedText(_currentGameSpeed.ToString());
        }
    }
}