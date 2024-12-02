using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerMergeTD.Utils
{
    public class TimerService
    {
        private readonly MonoBehaviourWrapper _monoBehaviourWrapper;
        private readonly Dictionary<string, TimerData> _timers = new Dictionary<string, TimerData>();

        public TimerService(MonoBehaviourWrapper monoBehaviourWrapper)
        {
            _monoBehaviourWrapper = monoBehaviourWrapper;
        }
        
        public void StartTimer(string timerId, float duration, Action<float> onTick = null, Action onComplete = null)
        {
            if (_timers.ContainsKey(timerId))
            {
                UnityEngine.Debug.LogWarning($"Timer with ID {timerId} already exists.");
                return;
            }

            var timerData = new TimerData(duration, onTick, onComplete);
            _timers.Add(timerId, timerData);

            _monoBehaviourWrapper.StartCoroutine(RunTimer(timerId));
        }

        public void StopTimer(string timerId)
        {
            if (_timers.ContainsKey(timerId))
            {
                _timers.Remove(timerId);
            }
        }

        public float GetRemainingTime(string timerId)
        {
            return _timers.TryGetValue(timerId, out var timerData) ? timerData.RemainingTime : 0;
        }

        public bool HasTimer(string timerId)
        {
            return _timers.ContainsKey(timerId);
        }

        public TimerData GetTimer(string timerId)
        {
            return _timers[timerId];
        }
        
        private IEnumerator RunTimer(string timerId)
        {
            if (!_timers.TryGetValue(timerId, out var timerData))
            {
                yield break;
            }

            while (timerData.RemainingTime > 0)
            {
                timerData.OnTick?.Invoke(timerData.RemainingTime);
                timerData.RemainingTime -= 1f;
                yield return new WaitForSeconds(1);
            }

            timerData.OnComplete?.Invoke();
            _timers.Remove(timerId);
        }

        public class TimerData
        {
            public float RemainingTime;
            public Action<float> OnTick;
            public Action OnComplete;

            public TimerData(float duration, Action<float> onTick, Action onComplete)
            {
                RemainingTime = duration;
                OnTick = onTick;
                OnComplete = onComplete;
            }
        }
    }
}