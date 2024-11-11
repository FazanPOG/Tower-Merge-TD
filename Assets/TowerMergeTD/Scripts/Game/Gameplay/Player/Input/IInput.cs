using System;
using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public interface IInput
    {
        event Action OnClicked;
        event Action OnClickStarted;
        event Action OnDragStarted;
        event Action<Vector2> OnDragWithThreshold;
        event Action OnClickCanceled;
        event Action<float> OnZoomIn;
        event Action<float> OnZoomOut;

        Vector3 GetInputWorldPosition();
    }
}