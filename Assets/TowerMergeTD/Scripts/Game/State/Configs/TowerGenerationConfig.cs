using System.Collections.Generic;
using System.Linq;
using TowerMergeTD.Game.Gameplay;
using UnityEngine;

namespace TowerMergeTD.Game.State
{
    [CreateAssetMenu(menuName = "Configs/Game/State/TowerGenerationConfig", order = 0)]
    public class TowerGenerationConfig : ScriptableObject
    {
        [SerializeField] private TowerType _towersType;
        [SerializeField] private int _createCost;
        [SerializeField] private TowerData[] _generation;

        private Dictionary<TowerData, TowerDataProxy> _dataMap = new Dictionary<TowerData, TowerDataProxy>();
        
        public TowerType TowersType => _towersType;
        public int CreateCost => _createCost;
        
        public TowerDataProxy GetTowerDataProxy(int towerLevel)
        {
            var towerDataProxy = _dataMap.First(tower => tower.Key.Level == towerLevel).Value;
            
            if(towerDataProxy == null)
                throw new MissingComponentException($"Tower with level ({towerLevel}) does not exist");

            return towerDataProxy;
        }

        public bool IsLastInGeneration(int level) => level >= _generation.Length;

        private void OnValidate()
        {
            for (int i = 1; i < _generation.Length + 1; i++)
            {
                var data = _generation[i - 1];
                data.Level = i;
                _dataMap[data] = new TowerDataProxy(data);
                
                if (_createCost < 0)
                    _createCost = 0;
            }
        }
    }
}