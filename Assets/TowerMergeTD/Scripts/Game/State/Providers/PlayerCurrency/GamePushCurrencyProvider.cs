using GamePush;
using R3;

namespace TowerMergeTD.Game.State
{
    public class GamePushCurrencyProvider : ICurrencyProvider
    {
        private const string CURRENCY_COIN_KEY = nameof(CURRENCY_COIN_KEY);
        private const string CURRENCY_GEMS_KEY = nameof(CURRENCY_GEMS_KEY);
        
        public PlayerCoinsProxy Coins { get; private set; }
        public PlayerGemsProxy Gems { get; private set;}
        
        public Observable<bool> LoadCurrency()
        {
            if (string.IsNullOrEmpty(GP_Player.GetString(CURRENCY_COIN_KEY)))
            {
                SetGoldFromSettings();
                SaveCoins();
            }
            else
            {
                PlayerCoins coins = new PlayerCoins(GP_Player.GetInt(CURRENCY_COIN_KEY));
                Coins = new PlayerCoinsProxy(coins);
            }

            if (string.IsNullOrEmpty(GP_Player.GetString(CURRENCY_GEMS_KEY)))
            {
                SetGemsFromSettings();
                SaveGems();
            }
            else
            {
                PlayerGems gems = new PlayerGems(GP_Player.GetInt(CURRENCY_GEMS_KEY));
                Gems = new PlayerGemsProxy(gems);
            }

            return Observable.Return(true);
        }

        public Observable<bool> SaveCoins()
        {
            GP_Player.Set(CURRENCY_COIN_KEY, Coins.Coins.CurrentValue);

            return Observable.Return(true);
        }

        public Observable<bool> SaveGems()
        {
            GP_Player.Set(CURRENCY_GEMS_KEY, Gems.Gems.CurrentValue);
            
            return Observable.Return(true);
        }

        public Observable<bool> ResetCurrency()
        {
            GP_Player.ResetPlayer();
            
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