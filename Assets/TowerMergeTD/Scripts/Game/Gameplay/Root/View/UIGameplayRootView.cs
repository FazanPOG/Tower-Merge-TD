using R3;
using UnityEngine;

namespace TowerMergeTD.Gameplay.Root
{
    public class UIGameplayRootView : MonoBehaviour
    {
        public Subject<Unit> _exitSceneSignalBus;

        public void HandleGoToMainMenuButtonClicked()
        {
            _exitSceneSignalBus?.OnNext(Unit.Default);
        }

        public void Bind(Subject<Unit> exitSceneSignalBus)
        {
            _exitSceneSignalBus = exitSceneSignalBus;
        }
    }
}
