using System.Collections;
using R3;
using TowerMergeTD.Utils;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class WaitingTutorialAction : ITutorialAction
    {
        private readonly MonoBehaviourWrapper _monoBehaviourWrapper;
        private readonly IInput _input;
        private readonly float _waitingTime;
        private readonly ReactiveProperty<bool> _isComplete = new ReactiveProperty<bool>();

        private Coroutine _coroutine;
        
        public ReadOnlyReactiveProperty<bool> IsComplete => _isComplete;

        public WaitingTutorialAction(MonoBehaviourWrapper monoBehaviourWrapper, IInput input, float waitingTime)
        {
            _monoBehaviourWrapper = monoBehaviourWrapper;
            _input = input;
            _waitingTime = waitingTime;
        }

        public void StartAction()
        {
            _coroutine = _monoBehaviourWrapper.StartCoroutine(Waiting(_waitingTime));
        }

        private IEnumerator Waiting(float waitingTime)
        {
            _input.DisableInput();
            yield return new WaitForSeconds(waitingTime);
            _input.EnableInput();
            _isComplete.Value = true;
        }

        public void Dispose()
        {
            if (_coroutine != null)
                _monoBehaviourWrapper.StopCoroutine(_coroutine);

            _isComplete?.Dispose();
        }
    }
}