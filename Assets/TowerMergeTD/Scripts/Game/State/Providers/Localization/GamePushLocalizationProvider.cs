using R3;

namespace TowerMergeTD.Game.State
{
    public class GamePushLocalizationProvider : ILocalizationProvider
    {
        public Observable<ILocalizationAsset> LoadLocalizationAsset()
        {
            //TODO: game push
            ILocalizationAsset localizationAsset = new RussianLocalizationAsset();
            
            return Observable.Return(localizationAsset);
        }
    }
}