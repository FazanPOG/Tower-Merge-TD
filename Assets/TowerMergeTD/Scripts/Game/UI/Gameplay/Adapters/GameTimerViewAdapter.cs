using System;
using R3;
using TowerMergeTD.Game.Gameplay;

namespace TowerMergeTD.Game.UI
{
    public class GameTimerViewAdapter
    {
        private readonly GameTimerView _view;
        private readonly IGameTimerService _gameTimerService;

        public GameTimerViewAdapter(GameTimerView view, IGameTimerService gameTimerService)
        {
            _view = view;
            _gameTimerService = gameTimerService;

            _gameTimerService.Time.Subscribe(UpdateView);
        }

        private void UpdateView(TimeSpan timeSpan)
        {
            string timeText = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            _view.SetTimeText(timeText);
        }
    }
}