using UnityEngine;

namespace TowerMergeTD.Game.Gameplay
{
    public class TutorialHandImageData
    {
        public Vector2 Position { get; }
        public int AnimationIndex { get; }

        public TutorialHandImageData(Vector2 position, int animationIndex)
        {
            Position = position;
            AnimationIndex = animationIndex;
        }
    }
}