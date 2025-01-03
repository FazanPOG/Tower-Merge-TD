﻿using System;
using System.Linq;
using ObservableCollections;
using R3;
using TowerMergeTD.Game.Gameplay;

namespace TowerMergeTD.Game.State
{
    public class GameStateProxy
    {
        public ObservableList<LevelSaveDataProxy> LevelDatas { get; } = new ObservableList<LevelSaveDataProxy>();
        public ObservableList<TowerType> UnlockTowers { get; } = new ObservableList<TowerType>();
        public ObservableList<string> ShopPurchasedItemIDs { get; } = new ObservableList<string>();
        public ReactiveProperty<DateTime> LastExitTime { get; } = new ReactiveProperty<DateTime>();
        public ReactiveProperty<float> MusicVolume { get; } = new ReactiveProperty<float>();
        public ReactiveProperty<float> SoundVolume { get; } = new ReactiveProperty<float>();

        public GameStateProxy(GameState gameState)
        {
            gameState.LevelDatas.ForEach(levelData => { LevelDatas.Add(new LevelSaveDataProxy(levelData)); });
            gameState.UnlockTowers.ForEach(unlockTower => { UnlockTowers.Add(unlockTower); });
            gameState.ShopPurchasedItemIDs.ForEach(id => { ShopPurchasedItemIDs.Add(id); });
            LastExitTime.Value = gameState.LastExitTime;
            MusicVolume.Value = gameState.MusicVolume;
            SoundVolume.Value = gameState.SoundVolume;
            
            LevelDatas.ObserveAdd().Subscribe(e =>
            {
                var newData = e.Value;
                gameState.LevelDatas.Add(new LevelSaveData
                {
                    ID = newData.ID,
                    IsOpen = newData.IsOpen,
                    Score = newData.Score,
                });
            });

            LevelDatas.ObserveRemove().Subscribe(e =>
            {
                var removedLevelData = e.Value;
                var levelData = gameState.LevelDatas.FirstOrDefault(x => x.ID == removedLevelData.ID);
                gameState.LevelDatas.Remove(levelData);
            });

            UnlockTowers.ObserveAdd().Subscribe(e =>
            {
                var newData = e.Value;
                gameState.UnlockTowers.Add(newData);
            });

            UnlockTowers.ObserveRemove().Subscribe(e =>
            {
                var removedData = e.Value;
                var data = gameState.UnlockTowers.FirstOrDefault(x => x == removedData);
                gameState.UnlockTowers.Remove(data);
            });
            
            ShopPurchasedItemIDs.ObserveAdd().Subscribe(e =>
            {
                var newData = e.Value;
                gameState.ShopPurchasedItemIDs.Add(newData);
            });

            ShopPurchasedItemIDs.ObserveRemove().Subscribe(e =>
            {
                var removedData = e.Value;
                var data = gameState.ShopPurchasedItemIDs.FirstOrDefault(x => x == removedData);
                gameState.ShopPurchasedItemIDs.Remove(data);
            });

            LastExitTime.Subscribe(newTime => gameState.LastExitTime = newTime);
            MusicVolume.Subscribe(newVolume => gameState.MusicVolume = newVolume);
            SoundVolume.Subscribe(newVolume => gameState.SoundVolume = newVolume);
        }
    }
}