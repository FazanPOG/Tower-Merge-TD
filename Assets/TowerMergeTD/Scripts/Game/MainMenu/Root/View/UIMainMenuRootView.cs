using R3;
using UnityEngine;

namespace TowerMergeTD.MainMenu.Root
{
    public class UIMainMenuRootView : MonoBehaviour
    {
        private ReactiveProperty<int> _exitSceneSignalBus;

        public void HandleGoToGameplayButtonClicked(int levelNumber)
        {
            _exitSceneSignalBus?.OnNext(levelNumber);
        }

        public void Bind(ReactiveProperty<int> exitSceneSignalBus)
        {
            _exitSceneSignalBus = exitSceneSignalBus;
        }
    }
}
