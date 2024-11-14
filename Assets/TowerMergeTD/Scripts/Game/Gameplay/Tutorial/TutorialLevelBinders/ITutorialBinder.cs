using System.Collections.Generic;
using Zenject;

namespace TowerMergeTD.Game.Gameplay
{
    public interface ITutorialBinder
    {
        Queue<ITutorialAction> TutorialActions { get; }
        Queue<string> TutorialTexts { get; }
        Queue<TutorialHandImageData> TutorialHandDatas { get; }

        void Bind(DiContainer diContainer);
    }
}