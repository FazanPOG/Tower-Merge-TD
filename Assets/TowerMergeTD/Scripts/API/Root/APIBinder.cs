using Zenject;

namespace TowerMergeTD.API
{
    public class APIBinder
    {
        private readonly DiContainer _container;

        public APIBinder(DiContainer container)
        {
            _container = container;
        }
        
        public void Bind()
        {
            _container.Bind<IADService>().To<ADService>().FromNew().AsSingle().NonLazy();
        }
    }
}