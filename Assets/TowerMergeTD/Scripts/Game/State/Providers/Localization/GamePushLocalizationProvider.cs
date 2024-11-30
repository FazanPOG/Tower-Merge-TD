using GamePush;
using R3;

namespace TowerMergeTD.Game.State
{
    public class GamePushLocalizationProvider : ILocalizationProvider
    {
        public Observable<ILocalizationAsset> LoadLocalizationAsset()
        {
            ILocalizationAsset localizationAsset;
                
            switch (GP_Language.Current())
            {
                case Language.Russian:
                    localizationAsset = new RussianLocalizationAsset();
                    break;
                
                case Language.English:
                    localizationAsset = new EnglishLocalizationAsset();
                    break;
                
                default:
                    localizationAsset = new RussianLocalizationAsset();
                    break;
            }
            
            return Observable.Return(localizationAsset);
        }
    }
}