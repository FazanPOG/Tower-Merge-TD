using System;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public interface IInput
    {
        event Action OnClicked;
        event Action OnClickStarted;
        event Action OnDrag;
        event Action OnClickCanceled;
        event Action<float> OnZoomIn;
        event Action<float> OnZoomOut;

        Vector3 GetClickWorldPosition();
    }
}