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
        private readonly TowersListView _towersListView;
        private readonly TowerType _towerType;
        private readonly MonoBehaviourWrapper _monoBehaviourWrapper;
        private readonly ReactiveProperty<bool> _isComplete = new ReactiveProperty<bool>();

        private Coroutine _coroutine;
        
        public ReadOnlyReactiveProperty<bool> IsComplete => _isComplete;

        public CreateTowerTutorialAction(MonoBehaviourWrapper monoBehaviourWrapper, TowersListView towersListView, TowerType towerType)
        {
            _towersListView = towersListView;
            _towerType = towerType;
            _monoBehaviourWrapper = monoBehaviourWrapper;
        }

        public void StartAction()
        {
            _towersListView.CanDisable = false;
            _coroutine = _monoBehaviourWrapper.StartCoroutine(Subscribe());
        }

        private IEnumerator Subscribe()
        {
            yield return new WaitUntil(() => _towersListView.gameObject.activeSelf);

            switch (_towerType)
            {
                case TowerType.Gun:
                    _towersListView.OnGunTowerButtonClicked += IsCompleted;
                    break;
                case TowerType.Rocket:
                    _towersListView.OnRocketTowerButtonClicked += IsCompleted;
                    break;
                default:
                    throw new Exception($"Tower type {_towerType} is not implemented");
            }
        }
        
        private void IsCompleted()
        {
            _towersListView.CanDisable = true;
            _towersListView.Hide();
            _isComplete.Value = true;
        }
        
        public void Dispose()
        {
            _isComplete?.Dispose();

            if (_coroutine != null)
                _monoBehaviourWrapper.StopCoroutine(_coroutine);
        }
    }
}