using GamePush;
using R3;
using TowerMergeTD.GameRoot;

namespace TowerMergeTD.Game.State
{
    public class GamePushLocalizationProvider : ILocalizationProvider
    {
        private readonly ProjectConfig _projectConfig;

        public GamePushLocalizationProvider(ProjectConfig projectConfig)
        {
            _projectConfig = projectConfig;
        }
    
        public Observable<ILocalizationAsset> LoadLocalizationAsset()
        {
            ILocalizationAsset localizationAsset;
            Language currentLanguage;

            if (_projectConfig.IsDevelopmentSettings)
                currentLanguage = _projectConfig.Language;
            else
                currentLanguage = GP_Language.Current();
            
            switch (currentLanguage)
            {
                case Language.Russian:
                    localizationAsset = new RussianLocalizationAsset();
                    GP_Language.Change(Language.Russian);
                    break;
                
                case Language.English:
                    localizationAsset = new EnglishLocalizationAsset();
                    GP_Language.Change(Language.English);
                    break;
                
                default:
                    localizationAsset = new RussianLocalizationAsset();
                    GP_Language.Change(Language.Russian);
                    break;
            }
            
            return Observable.Return(localizationAsset);
        }
    }
}