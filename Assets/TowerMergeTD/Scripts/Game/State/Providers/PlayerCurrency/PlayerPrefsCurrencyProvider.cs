using R3;
using UnityEngine;

namespace TowerMergeTD.Game.State
{
    public class PlayerPrefsCurrencyProvider : ICurrencyProvider
    {
        private const string CURRENCY_GOLD_KEY = nameof(CURRENCY_GOLD_KEY);
        private const string CURRENCY_GEMS_KEY = nameof(CURRENCY_GEMS_KEY);
        
        public PlayerCoinsProxy Coins { get; private set; }
        public PlayerGemsProxy Gems { get; private set;}

        public Observable<bool> LoadCurrency()
        {
            if (PlayerPrefs.HasKey(CURRENCY_GOLD_KEY) == false)
            {
                SetGoldFromSettings();
                SaveCoins();
            }
            else
            {
                PlayerCoins coins = new PlayerCoins(PlayerPrefs.GetInt(CURRENCY_GOLD_KEY));
                Coins = new PlayerCoinsProxy(coins);
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

        public Observable<bool> SaveCoins()
        {
            PlayerPrefs.SetInt(CURRENCY_GOLD_KEY, Coins.Coins.CurrentValue);
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
            PlayerCoins coins = new PlayerCoins(0);
            Coins = new PlayerCoinsProxy(coins);
            
            SaveCoins();
        }

        private void SetGemsFromSettings()
        {
            PlayerGems gems = new PlayerGems(0);
            Gems = new PlayerGemsProxy(gems);
            
            SaveGems();
        }
    }
}