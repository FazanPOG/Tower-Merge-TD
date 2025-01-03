﻿using System.Collections.Generic;

namespace TowerMergeTD.Game.State
{
    public class EnglishLocalizationAsset : ILocalizationAsset
    {
        private readonly Dictionary<string, string> _translationMap;
        
        public EnglishLocalizationAsset()
        {
            _translationMap = new Dictionary<string, string>()
            {
                [LocalizationKeys.TOWER_MERGE_TD_KEY] = "TOWER MERGE TD",
                [LocalizationKeys.SHOP_KEY] = "SHOP",
                [LocalizationKeys.SETTINGS_KEY] = "Settings",
                [LocalizationKeys.SOUND_KEY] = "Sound",
                [LocalizationKeys.MUSIC_KEY] = "Music",
                [LocalizationKeys.LEVEL_KEY] = "Level",
                [LocalizationKeys.LEVEL_LOCK_DESCRIPTION_KEY] = "This level is locked.",
                [LocalizationKeys.TOWER_KEY] = "Tower",
                [LocalizationKeys.COIN_KEY] = "Coin",
                [LocalizationKeys.GEM_KEY] = "Gem",
                [LocalizationKeys.EPISODE_KEY] = "Episode",
                [LocalizationKeys.PAUSE_KEY] = "Pause",
                [LocalizationKeys.CONTINUE_KEY] = "Continue",
                [LocalizationKeys.RESTART_KEY] = "Restart",
                [LocalizationKeys.EXIT_KEY] = "Exit",
                [LocalizationKeys.LOSE_KEY] = "Lose",
                [LocalizationKeys.COMPLETE_KEY] = "Victory!",
                [LocalizationKeys.SCORE_KEY] = "Score",
                [LocalizationKeys.TIME_KEY] = "Time",
                [LocalizationKeys.LEVEL_1_TUTORIAL_TEXT_1_KEY] = "You can only place towers on special square-lined areas. Click on the left one to build your first tower!",
                [LocalizationKeys.LEVEL_1_TUTORIAL_TEXT_2_KEY] = "Click to select and place a tower. This requires resources.",
                [LocalizationKeys.LEVEL_1_TUTORIAL_TEXT_3_KEY] = "Keep track of the number of waves",
                [LocalizationKeys.LEVEL_1_TUTORIAL_TEXT_4_KEY] = "Get resources by destroying enemies",
                [LocalizationKeys.LEVEL_1_TUTORIAL_TEXT_5_KEY] = "Prevent enemies from reaching the base to save health.",
                [LocalizationKeys.LEVEL_1_TUTORIAL_TEXT_6_KEY] = "Now click on the right one to place the second tower",
                [LocalizationKeys.LEVEL_1_TUTORIAL_TEXT_7_KEY] = "Place the second tower",
                [LocalizationKeys.LEVEL_1_TUTORIAL_TEXT_8_KEY] = "You can move the towers to other areas. If you merge two identical towers, you will get a more powerful version, try it!",
                [LocalizationKeys.LEVEL_1_TUTORIAL_TEXT_9_KEY] = "Combine identical towers to get a stronger version of them!",
                [LocalizationKeys.LEVEL_1_TUTORIAL_TEXT_10_KEY] = "The training is complete, have a good game!",
                [LocalizationKeys.FREE_KEY] = "FREE",
                [LocalizationKeys.BEST_KEY] = "BEST",
                [LocalizationKeys.BONUS_KEY] = "BONUS",
                [LocalizationKeys.CANCEL_KEY] = "Cancel",
                [LocalizationKeys.CONFIRM_WATCH_AD_KEY] = "Want to watch an ad?",
                [LocalizationKeys.SOLD_KEY] = "SOLD OUT",
                [LocalizationKeys.WAIT_AD_KEY] = "Ads will be back shortly!",
                [LocalizationKeys.GUN_KEY] = "Gun tower",
                [LocalizationKeys.ROCKET_KEY] = "Rocket tower",
                [LocalizationKeys.LASER_KEY] = "Laser tower",
                [LocalizationKeys.SNIPER_KEY] = "Sniper tower",
                [LocalizationKeys.ELECTRIC_KEY] = "Electric tower",
                [LocalizationKeys.IMPACT_KEY] = "Impact tower",
                [LocalizationKeys.RAPID_KEY] = "Rapid tower",
                [LocalizationKeys.GOD_KEY] = "Divine tower",
                [LocalizationKeys.ROCKET_INFO_KEY] = "Fires rockets that hit multiple enemies at once, dealing area damage.",
                [LocalizationKeys.LASER_INFO_KEY] = "Gradually increases damage when continuously attacking the same target.",
                [LocalizationKeys.RAPID_INFO_KEY] = "Extremely low damage but compensates with very high attack speed.",
                [LocalizationKeys.GOD_INFO_KEY] = "??? This tower seems to hide its true power...",
                [LocalizationKeys.SNIPER_INFO_KEY] = "Boasts massive range and damage but has slow reload time. Can pierce through multiple enemies.",
                [LocalizationKeys.ELECTRIC_INFO_KEY] = "Shoots lightning that jumps from one enemy to another, hitting a chain of targets.",
                [LocalizationKeys.IMPACT_INFO_KEY] = "Hits all enemies around it, dealing decent area damage.",
            };
        }
        
        public string GetTranslation(string translateKey)
        {
            if (_translationMap.TryGetValue(translateKey, out string translation) == false)
                throw new KeyNotFoundException($"Translation asset ({nameof(RussianLocalizationAsset)}) missing translation (key: {translateKey})");

            return translation;
        }
    }
}