using Sirenix.OdinInspector;
using TowerMergeTD.Game.State;
using UnityEngine;
using Zenject;

namespace TowerMergeTD.Utils.Debug
{
    [HideInEditorMode]
    public class MainMenuDebug : MonoBehaviour
    {
        private PlayerGoldProxy _gold;
        private PlayerGemsProxy _gems;

        [ShowInInspector, ReadOnly]
        public int Gold => _gold.Gold.CurrentValue;
        
        [ShowInInspector, ReadOnly]
        public int Gems => _gems.Gems.CurrentValue;
        
        public void Init(DiContainer diContainer)
        {
            _gold = diContainer.Resolve<PlayerGoldProxy>();
            _gems = diContainer.Resolve<PlayerGemsProxy>();
        }

        [Button("Set gold")]
        private void SetGold(int gold)
        {
            _gold.Gold.Value = gold;
        }
        
        [Button("Set gems")]
        private void SetGems(int gems)
        {
            _gems.Gems.Value = gems;
        }
    }
}