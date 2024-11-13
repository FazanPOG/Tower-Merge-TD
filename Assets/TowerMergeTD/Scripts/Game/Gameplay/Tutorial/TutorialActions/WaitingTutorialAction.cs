using System.Collections;
using R3;
using TowerMergeTD.Utils;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class WaitingTutorialAction : ITutorialAction
    {
        private readonly MonoBehaviourWrapper _monoBehaviourWrapper;
        private readonly float _waitingTime;
        private readonly ReactiveProperty<bool> _isComplete = new ReactiveProperty<bool>();

        public ReadOnlyReactiveProperty<bool> IsComplete => _isComplete;

        public WaitingTutorialAction(MonoBehaviourWrapper monoBehaviourWrapper, float waitingTime)
        {
            _monoBehaviourWrapper = monoBehaviourWrapper;
            _waitingTime = waitingTime;
        }

        public void StartAction()
        {
            _monoBehaviourWrapper.StartCoroutine(Waiting(_waitingTime));
        }

        private IEnumerator Waiting(float waitingTime)
        {
            yield return new WaitForSeconds(waitingTime);
            _isComplete.Value = true;
        }
    }
}