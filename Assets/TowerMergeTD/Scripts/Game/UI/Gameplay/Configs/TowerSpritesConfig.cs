using System;
using System.Collections.Generic;
using TowerMergeTD.Game.Gameplay;
using UnityEngine;

namespace TowerMergeTD.Game.UI
{
    [CreateAssetMenu(menuName = "Configs/Game/UI/Gameplay/TowerSpritesConfig")]
    public class TowerSpritesConfig : ScriptableObject
    {
        [SerializeField] private List<TowerSpriteData> _towerSprites;

        public List<TowerSpriteData> TowerSprites => _towerSprites;
    }

    [Serializable]
    public class TowerSpriteData
    {
        [SerializeField] private TowerType _towerType;
        [SerializeField] private Sprite _towerSprite;

        public TowerType TowerType => _towerType;

        public Sprite TowerSprite => _towerSprite;
    }
}