using System;
using System.Collections;
using R3;
using TowerMergeTD.Game.UI;
using TowerMergeTD.Utils;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class CreateTowerTutorialAction : ITutorialAction
    {
        private readonly MonoBehaviourWrapper _monoBehaviourWrapper;
        private readonly TowersListView _towersListView;
        private readonly TowerCreateButtonView _towersCreateButtonView;
        private readonly ReactiveProperty<bool> _isComplete = new ReactiveProperty<bool>();

        private Coroutine _coroutine;
        
        public ReadOnlyReactiveProperty<bool> IsComplete => _isComplete;

        public CreateTowerTutorialAction(
            MonoBehaviourWrapper monoBehaviourWrapper, 
            TowersListView towersListView, 
            TowerCreateButtonView towersCreateButtonView)
        {
            _monoBehaviourWrapper = monoBehaviourWrapper;
            _towersListView = towersListView;
            _towersCreateButtonView = towersCreateButtonView;
        }

        public void StartAction()
        {
            _towersListView.CanDisable = false;
            _coroutine = _monoBehaviourWrapper.StartCoroutine(Subscribe());
        }

        private IEnumerator Subscribe()
        {
            yield return new WaitUntil(() => _towersListView.gameObject.activeSelf);

            _towersCreateButtonView.OnButtonClicked += IsCompleted;
        }
        
        private void IsCompleted()
        {
            _towersListView.CanDisable = true;
            _towersListView.Hide();
            _isComplete.Value = true;
            _towersCreateButtonView.OnButtonClicked -= IsCompleted;
        }
        
        public void Dispose()
        {
            _isComplete?.Dispose();

            if (_coroutine != null)
                _monoBehaviourWrapper.StopCoroutine(_coroutine);
        }
    }
}