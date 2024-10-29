using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class TowerActionsView : MonoBehaviour
    {
        [SerializeField] private Button _createGunTowerButton;
        [SerializeField] private Button _createRocketTowerButton;
        [SerializeField] private Button _sellTowerButton;

        public event Action OnCreateGunTowerButtonClicked;
        public event Action OnCreateRocketTowerButtonClicked;
        public event Action OnSellTowerButtonClicked;

        private void Awake() => Hide();

        private void OnEnable()
        {
            _createGunTowerButton.onClick.AddListener(() => OnCreateGunTowerButtonClicked?.Invoke());
            _createRocketTowerButton.onClick.AddListener(() => OnCreateRocketTowerButtonClicked?.Invoke());
            _sellTowerButton.onClick.AddListener(() => OnSellTowerButtonClicked?.Invoke());
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);

        public void SetActiveCreateTowerButtons(bool activeState)
        {
            _createGunTowerButton.gameObject.SetActive(activeState); 
            _createRocketTowerButton.gameObject.SetActive(activeState);
        }

        public void SetActiveSellButton(bool activeState) => _sellTowerButton.gameObject.SetActive(activeState);
        
        private void OnDisable()
        {
            _createGunTowerButton.onClick.RemoveAllListeners();
            _createRocketTowerButton.onClick.RemoveAllListeners();
            _sellTowerButton.onClick.RemoveAllListeners();
        }
    }
}