﻿namespace TowerMergeTD.Game.Gameplay
{
    public interface IScoreService
    {
        int Score { get; }
        void AddScore(int score);
    }
}