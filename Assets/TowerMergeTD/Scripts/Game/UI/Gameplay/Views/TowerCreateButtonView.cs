using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerMergeTD.Game.UI
{
    public class TowerCreateButtonView : MonoBehaviour
    {
        [SerializeField] private GameObject _costGameObject;
        [SerializeField] private Image _towerImage;
        [SerializeField] private Image _lockImage;
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private Button _button;

        public event Action OnButtonClicked;
        
        private void OnEnable()
        {
            _button.onClick.AddListener(() => OnButtonClicked?.Invoke());
        }

        public void SetCostText(string text) => _costText.text = text; 
        public void SetTowerImageSprite(Sprite sprite) => _towerImage.sprite = sprite;
        public void SetButtonInteractable(bool canInteract) => _button.interactable = canInteract;

        public void SetActiveCostGameObject(bool activeState) => _costGameObject.gameObject.SetActive(activeState);
        public void SetActiveTowerImage(bool activeState) => _towerImage.gameObject.SetActive(activeState);
        public void SetActiveLockImage(bool activeState) => _lockImage.gameObject.SetActive(activeState);
        
        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}