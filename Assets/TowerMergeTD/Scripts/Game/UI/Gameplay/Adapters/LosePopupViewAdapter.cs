using R3;
using TowerMergeTD.Game.Audio;
using TowerMergeTD.Game.Gameplay;
using TowerMergeTD.Game.State;
using TowerMergeTD.Gameplay.Root;
using TowerMergeTD.GameRoot;
using TowerMergeTD.MainMenu.Root;

namespace TowerMergeTD.Game.UI
{
    public class LosePopupViewAdapter : IGameStateHandler
    {
        private const int COIN_LOSE_REWARD = 10;
        
        private readonly LosePopupView _losePopupView;
        private readonly int _currentLevelIndex;
        private readonly ReactiveProperty<SceneEnterParams> _exitSceneSignalBus;
        private readonly AudioPlayer _audioPlayer;
        private readonly PlayerCoinsProxy _playerCoins;

        public LosePopupViewAdapter(
            LosePopupView losePopupView, 
            int currentLevelIndex, 
            ReactiveProperty<SceneEnterParams> exitSceneSignalBus,
            ILocalizationAsset localizationAsset,
            AudioPlayer audioPlayer,
            PlayerCoinsProxy playerCoins)
        {
            _losePopupView = losePopupView;
            _currentLevelIndex = currentLevelIndex;
            _exitSceneSignalBus = exitSceneSignalBus;
            _audioPlayer = audioPlayer;
            _playerCoins = playerCoins;

            _losePopupView.SetLoseText(localizationAsset.GetTranslation(LocalizationKeys.LOSE_KEY));
            _losePopupView.SetCoinText(localizationAsset.GetTranslation(LocalizationKeys.COIN_KEY));
            
            _losePopupView.OnHomeButtonClicked += HandleOnHomeButtonClicked;
            _losePopupView.OnRestartButtonClicked += HandleOnRestartButtonClicked;
        }

        public void HandleGameState(IGameState gameState)
        {
            if (gameState is LoseGameState)
            {
                _losePopupView.Show();
                _audioPlayer.Play(AudioType.GameOver);
                _playerCoins.Coins.Value += COIN_LOSE_REWARD;
            }
        }

        private void HandleOnHomeButtonClicked()
        {
            _audioPlayer.Play(AudioType.Button);
            _exitSceneSignalBus.OnNext(new MainMenuEnterParams("TEST"));
        }

        private void HandleOnRestartButtonClicked()
        {
            _audioPlayer.Play(AudioType.Button);
            _exitSceneSignalBus.OnNext(new GameplayEnterParams(_currentLevelIndex));
        }
    }
}