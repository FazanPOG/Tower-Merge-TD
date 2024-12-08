using System;
using TowerMergeTD.Game.Gameplay;
using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    public class TowerCreateButtonViewAdapter
    {
        private readonly TowerCreateButtonView _view;
        private readonly TowerType _towerType;
        private readonly TowerFactory _towerFactory;
        private readonly Sprite _towerSprite;

        public TowerCreateButtonViewAdapter(
            TowerCreateButtonView view, 
            TowerType towerType,
            TowerFactory towerFactory,
            Sprite towerSprite, 
            Action<TowerType> createTowerAction)
        {
            _view = view;
            _towerType = towerType;
            _towerFactory = towerFactory;
            _towerSprite = towerSprite;

            if (_towerType == TowerType.None)
            {
                SetLockState();
                return;
            }

            Init();

            _view.OnButtonClicked += () => { createTowerAction.Invoke(_towerType); };
        }

        private void Init()
        {
            _view.SetTowerImageSprite(_towerSprite);
            _view.SetCostText(_towerFactory.GetCreateCost(_towerType).ToString());
            
            SetUnlockState();
        }
        
        public void SetUnlockState()
        {
            _view.SetActiveCostGameObject(true);
            _view.SetActiveTowerImage(true);
            _view.SetActiveLockImage(false);
            _view.SetButtonInteractable(true);
        }

        public void SetLockState()
        {
            _view.SetActiveCostGameObject(false);
            _view.SetActiveTowerImage(false);
            _view.SetActiveLockImage(true);
            _view.SetButtonInteractable(false);
        }
    }
}