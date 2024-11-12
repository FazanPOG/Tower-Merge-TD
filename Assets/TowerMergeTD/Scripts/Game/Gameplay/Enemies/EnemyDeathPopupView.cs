using System;
using TMPro;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class EnemyDeathPopupView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Animation _animation;

        private Action _onPopupEndedCallback;
        
        public void ShowPopup(int buildingCurrencyValue, Action onPopupEndedCallback)
        {
            _onPopupEndedCallback = onPopupEndedCallback;
            
            _text.text = buildingCurrencyValue.ToString();
            _animation.Play();
        }

        private void OnAnimationEnded()
        {
            _onPopupEndedCallback?.Invoke();
        }
    }
}