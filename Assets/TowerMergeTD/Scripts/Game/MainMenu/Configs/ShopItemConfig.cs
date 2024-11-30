using Sirenix.OdinInspector;
using TowerMergeTD.Game.Gameplay;
using UnityEngine;

namespace TowerMergeTD.Game.MainMenu
{
    [CreateAssetMenu(menuName = "Configs/ShopItem")]
    public class ShopItemConfig : ScriptableObject
    {
        [Title("General Settings")]
        [EnumToggleButtons]
        
        [SerializeField, LabelText("Can only be purchased once")]
        private bool _isSinglePurchase;
        
        [SerializeField, LabelText("Price Type")]
        private ShopItemPriceType _itemPriceType;

        [EnumToggleButtons]
        [SerializeField, LabelText("Item Type")]
        private ShopItemType _itemType;
        
        [ShowIf(nameof(_hasPrice))]
        [SerializeField, LabelText("Item Price"), MinValue(0)]
        private int _itemPrice;
        
        [ShowIf(nameof(_isTowerItem))]
        [SerializeField, LabelText("Tower to Unlock")]
        private TowerType towerToUnlock;

        [ShowIf(nameof(_isCurrencyItem))]
        [SerializeField, LabelText("Currency Value"), MinValue(1)]
        private int _currencyValue;

        [Space]
        [Title("Shop Display Settings")]

        [PreviewField(75)]
        [SerializeField, LabelText("Item Icon")]
        private Sprite _itemIcon;
        
        [ShowIf(nameof(_hasPrice))]
        [PreviewField(75)]
        [SerializeField, LabelText("Currency Icon")]
        private Sprite _currencyIcon;

        private bool _isTowerItem;
        private bool _isCurrencyItem;
        private bool _hasPrice;

        public string ID => name;
        
        public bool IsSinglePurchase => _isSinglePurchase;
        
        public ShopItemPriceType ItemPriceType => _itemPriceType;

        public ShopItemType ItemType => _itemType;
        
        public TowerType TowerToUnlock => towerToUnlock;

        public int CurrencyValue => _currencyValue;

        public int ItemPrice => _itemPrice;

        public Sprite ItemIcon => _itemIcon;
        
        public Sprite CurrencyIcon => _currencyIcon;

        private void OnValidate()
        {
            switch (_itemPriceType)
            {
                case ShopItemPriceType.Coin:
                    _hasPrice = true;
                    break;
                
                case ShopItemPriceType.Gem:
                    _hasPrice = true; 
                    break;
                
                default:
                    _hasPrice = false;
                    break;
            }
            
            switch (_itemType)
            {
                case ShopItemType.Tower:
                    _isTowerItem = true;
                    _isCurrencyItem = false;
                    break;
                
                case ShopItemType.Coin:
                    _isTowerItem = false;
                    _isCurrencyItem = true;
                    break;
                
                case ShopItemType.Gem:
                    _isTowerItem = false;
                    _isCurrencyItem = true;
                    break;
                
                default:
                    _isTowerItem = false;
                    _isCurrencyItem = false;
                    towerToUnlock = TowerType.None;
                    break;
            }
        }
    }
}