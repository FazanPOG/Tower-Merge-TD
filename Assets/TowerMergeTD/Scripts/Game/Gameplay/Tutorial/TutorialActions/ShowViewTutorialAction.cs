using System.Collections;
using R3;
using TowerMergeTD.Utils;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class ShowViewTutorialAction : ITutorialAction
    {
        private readonly MonoBehaviourWrapper _monoBehaviourWrapper;
        private readonly GameObject _viewObject;
        private readonly ReactiveProperty<bool> _isComplete = new ReactiveProperty<bool>();

        private Coroutine _coroutine;
        
        public ReadOnlyReactiveProperty<bool> IsComplete => _isComplete;

        public ShowViewTutorialAction(MonoBehaviourWrapper monoBehaviourWrapper, GameObject viewObject)
        {
            _monoBehaviourWrapper = monoBehaviourWrapper;
            _viewObject = viewObject;
        }
        
        public void StartAction()
        {
            _coroutine = _monoBehaviourWrapper.StartCoroutine(WaitUntilViewShowed());
        }

        private IEnumerator WaitUntilViewShowed()
        {
            yield return new WaitUntil(() => _viewObject.gameObject.activeSelf);
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