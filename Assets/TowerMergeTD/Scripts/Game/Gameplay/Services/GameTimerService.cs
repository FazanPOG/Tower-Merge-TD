using System;
using System.Collections;
using R3;
using TowerMergeTD.Utils;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class GameTimerService : IGameTimerService, IPauseHandler
    {
        private readonly MonoBehaviourWrapper _monoBehaviourWrapper;

        private ReactiveProperty<TimeSpan> _time;
        private bool _isStopped;
        private Coroutine _coroutine;

        public ReadOnlyReactiveProperty<TimeSpan> Time => _time;

        public GameTimerService(MonoBehaviourWrapper monoBehaviourWrapper)
        {
            _monoBehaviourWrapper = monoBehaviourWrapper;
            
            _time = new ReactiveProperty<TimeSpan>();
        }
        
        public void StartTimer()
        {
            _coroutine = _monoBehaviourWrapper.StartCoroutine(StartTime());
        }

        public void HandlePause(bool isPaused) => _isStopped = isPaused;

        public void StopTimer()
        {
            _monoBehaviourWrapper.StopCoroutine(_coroutine);
        }

        private IEnumerator StartTime()
        {
            do
            {
                yield return new WaitUntil(() => !_isStopped);
                yield return new WaitForSeconds(1);
                _time.Value += TimeSpan.FromSeconds(1);
        
            } while (!_isStopped);
        }
    }
}