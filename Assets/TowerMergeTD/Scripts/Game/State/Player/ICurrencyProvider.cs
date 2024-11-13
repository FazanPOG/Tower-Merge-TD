using R3;

namespace TowerMergeTD.Game.State
{
    public interface ICurrencyProvider
    {
        PlayerCoinsProxy Coins { get; }
        PlayerGemsProxy Gems { get; }
        Observable<bool> LoadCurrency();
        Observable<bool> SaveCoins();
        Observable<bool> SaveGems();
        Observable<bool> ResetCurrency();
    }
}