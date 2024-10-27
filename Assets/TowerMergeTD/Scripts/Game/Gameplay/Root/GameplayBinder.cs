using TowerMergeTD.Game.Gameplay;
using Zenject;

namespace TowerMergeTD.Gameplay.Root
{
    public class GameplayBinder
    {
        private readonly DiContainer _container;
        
        public GameplayBinder(DiContainer container)
        {
            _container = container;
        }

        public void Bind()
        {
            InputHandler inputHandler = new InputHandler();
            _container.BindInterfacesTo<InputHandler>().FromInstance(inputHandler).AsSingle().NonLazy();
        }
    }
}