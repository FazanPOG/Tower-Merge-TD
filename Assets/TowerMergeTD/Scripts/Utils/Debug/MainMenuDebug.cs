using Sirenix.OdinInspector;
using TowerMergeTD.Game.State;
using UnityEngine;
using Zenject;

namespace TowerMergeTD.Utils.Debug
{
    [HideInEditorMode]
    public class MainMenuDebug : MonoBehaviour
    {
        private PlayerCoinsProxy _coins;
        private PlayerGemsProxy _gems;

        [ShowInInspector, ReadOnly]
        public int Gold => _coins.Coins.CurrentValue;
        
        [ShowInInspector, ReadOnly]
        public int Gems => _gems.Gems.CurrentValue;
        
        public void Init(DiContainer diContainer)
        {
            _coins = diContainer.Resolve<PlayerCoinsProxy>();
            _gems = diContainer.Resolve<PlayerGemsProxy>();
        }

        [Button("Set gold")]
        private void SetGold(int gold)
        {
            _coins.Coins.Value = gold;
        }
        
        [Button("Set gems")]
        private void SetGems(int gems)
        {
            _gems.Gems.Value = gems;
        }
    }
}