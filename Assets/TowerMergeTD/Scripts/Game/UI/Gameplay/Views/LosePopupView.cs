using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class LosePopupView : MonoBehaviour
    {
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _restartButton;

        public event Action OnHomeButtonClicked;
        public event Action OnRestartButtonClicked;
        
        private void OnEnable()
        {
            _homeButton.onClick.AddListener(() => OnHomeButtonClicked?.Invoke());
            _restartButton.onClick.AddListener(() => OnRestartButtonClicked?.Invoke());
        }

        public void Show() => gameObject.SetActive(true);
        
        private void OnDisable()
        {
            _homeButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
        }
    }
}