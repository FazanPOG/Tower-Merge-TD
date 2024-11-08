using R3;
using UnityEngine;

namespace TowerMergeTD.Game.State
{
    public class PlayerPrefsCurrencyProvider : ICurrencyProvider
    {
        private const string CURRENCY_GOLD_KEY = nameof(CURRENCY_GOLD_KEY);
        private const string CURRENCY_GEMS_KEY = nameof(CURRENCY_GEMS_KEY);
        
        public PlayerGoldProxy Gold { get; private set; }
        public PlayerGemsProxy Gems { get; private set;}
        
        public Observable<bool> LoadCurrency()
        {
            if (PlayerPrefs.HasKey(CURRENCY_GOLD_KEY) == false)
            {
                SetGoldFromSettings();
                SaveGold();
            }
            else
            {
                PlayerGold gold = new PlayerGold(PlayerPrefs.GetInt(CURRENCY_GOLD_KEY));
                Gold = new PlayerGoldProxy(gold);
            }
            
            if (PlayerPrefs.HasKey(CURRENCY_GEMS_KEY) == false)
            {
                SetGemsFromSettings();
                SaveGems();
            }
            else
            {
                PlayerGems gems = new PlayerGems(PlayerPrefs.GetInt(CURRENCY_GEMS_KEY));
                Gems = new PlayerGemsProxy(gems);
            }

            return Observable.Return(true);
        }

        public Observable<bool> SaveGold()
        {
            PlayerPrefs.SetInt(CURRENCY_GOLD_KEY, Gold.Gold.CurrentValue);
            PlayerPrefs.Save();

            return Observable.Return(true);
        }

        public Observable<bool> SaveGems()
        {
            PlayerPrefs.SetInt(CURRENCY_GEMS_KEY, Gems.Gems.CurrentValue);
            PlayerPrefs.Save();
            
            return Observable.Return(true);
        }

        public Observable<bool> ResetCurrency()
        {
            SetGoldFromSettings();
            SetGemsFromSettings();

            return Observable.Return(true);
        }

        private void SetGoldFromSettings()
        {
            PlayerGold gold = new PlayerGold(0);
            Gold = new PlayerGoldProxy(gold);
            
            SaveGold();
        }

        private void SetGemsFromSettings()
        {
            PlayerGems gems = new PlayerGems(0);
            Gems = new PlayerGemsProxy(gems);
            
            SaveGems();
        }
    }
}