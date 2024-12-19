using System;
using GamePush;
using R3;
using TowerMergeTD.GameRoot;
using YG;

namespace TowerMergeTD.Game.State
{
    public class YandexGamesLocalizationProvider : ILocalizationProvider
    {
        private readonly ProjectConfig _projectConfig;

        public YandexGamesLocalizationProvider(ProjectConfig projectConfig)
        {
            _projectConfig = projectConfig;
        }
    
        public Observable<ILocalizationAsset> LoadLocalizationAsset()
        {
            ILocalizationAsset localizationAsset;
            string currentLanguage;

            if (_projectConfig.IsDevelopmentSettings)
                currentLanguage = ConvertToISOFormatLanguage(_projectConfig.Language);
            else
                currentLanguage = YandexGame.EnvironmentData.language;

            switch (currentLanguage)
            {
                case "ru":
                    localizationAsset = new RussianLocalizationAsset();
                    break;
                
                case "en":
                    localizationAsset = new EnglishLocalizationAsset();
                    break;
                
                default:
                    localizationAsset = new RussianLocalizationAsset();
                    break;
            }
            
            return Observable.Return(localizationAsset);
        }

        private string ConvertToISOFormatLanguage(Language language)
        {
            switch (language)
            {
                case Language.Russian:
                    return "ru";
                
                case Language.English:
                    return "en";
                
                default:
                    throw new NotImplementedException($"Language does not support: {language}");
            }
        }
    }
}