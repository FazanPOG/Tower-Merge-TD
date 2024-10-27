using R3;
using UnityEngine;

namespace TowerMergeTD.MainMenu.Root
{
    public class UIMainMenuRootView : MonoBehaviour
    {
        public Subject<Unit> _exitSceneSignalBus;

        public void HandleGoToGameplayButtonClicked()
        {
            _exitSceneSignalBus?.OnNext(Unit.Default);
        }

        public void Bind(Subject<Unit> exitSceneSignalBus)
        {
            _exitSceneSignalBus = exitSceneSignalBus;
        }
    }
}
