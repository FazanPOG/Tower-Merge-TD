using R3;

namespace TowerMergeTD.Game.State
{
    public interface ILocalizationProvider
    {
        Observable<ILocalizationAsset> LoadLocalizationAsset();
    }
}