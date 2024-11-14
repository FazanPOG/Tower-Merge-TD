﻿using System.Collections;
using R3;
using TowerMergeTD.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.Gameplay
{
    public class ClickButtonTutorialAction : ITutorialAction
    {
        private readonly MonoBehaviourWrapper _monoBehaviourWrapper;
        private readonly Button _button;
        private readonly ReactiveProperty<bool> _isComplete = new ReactiveProperty<bool>();

        private Coroutine _coroutine;
        
        public ReadOnlyReactiveProperty<bool> IsComplete => _isComplete;

        public ClickButtonTutorialAction(MonoBehaviourWrapper monoBehaviourWrapper, Button button)
        {
            _monoBehaviourWrapper = monoBehaviourWrapper;
            _button = button;
        }

        public void StartAction()
        {
            _coroutine = _monoBehaviourWrapper.StartCoroutine(Subscribe());
        }

        private IEnumerator Subscribe()
        {
            yield return new WaitUntil(() => _button.gameObject.activeSelf);
            _button.onClick.AddListener(IsCompleted);
        }
        
        private void IsCompleted()
        {
            _isComplete.Value = true;
        }
        
        public void Dispose()
        {
            _isComplete?.Dispose();

            if (_coroutine != null)
                _monoBehaviourWrapper.StopCoroutine(_coroutine);

            _button.onClick.RemoveListener(IsCompleted);
        }
    }
}