﻿using System.Collections.Generic;

namespace TowerMergeTD.Game.State
{
    public class RussianLocalizationAsset : ILocalizationAsset
    {
        private readonly Dictionary<string, string> _translationMap;
        
        public RussianLocalizationAsset()
        {
            _translationMap = new Dictionary<string, string>()
            {
                [LocalizationKeys.TOWER_MERGE_TD_KEY] = "Соедини башни ТД",
                [LocalizationKeys.SHOP_KEY] = "Mагазин",
                [LocalizationKeys.SETTINGS_KEY] = "Настройки",
                [LocalizationKeys.SOUND_KEY] = "звук",
                [LocalizationKeys.MUSIC_KEY] = "музыка",
                [LocalizationKeys.LEVEL_KEY] = "Уровень",
                [LocalizationKeys.LEVEL_LOCK_DESCRIPTION_KEY] = "Этот уровень закрыт.",
                [LocalizationKeys.TOWER_KEY] = "башни",
                [LocalizationKeys.COIN_KEY] = "монеты",
                [LocalizationKeys.GEM_KEY] = "кристаллы",
                [LocalizationKeys.EPISODE_KEY] = "Эпизод",
                [LocalizationKeys.PAUSE_KEY] = "пауза",
                [LocalizationKeys.CONTINUE_KEY] = "продолжить",
                [LocalizationKeys.RESTART_KEY] = "заного",
                [LocalizationKeys.EXIT_KEY] = "выход",
                [LocalizationKeys.LOSE_KEY] = "поражение",
                [LocalizationKeys.COMPLETE_KEY] = "победа!",
                [LocalizationKeys.SCORE_KEY] = "счет",
                [LocalizationKeys.TIME_KEY] = "время",
                [LocalizationKeys.LEVEL_1_TUTORIAL_TEXT_1_KEY] = "Нажмите, чтобы поставить башню",
                [LocalizationKeys.LEVEL_1_TUTORIAL_TEXT_2_KEY] = "Нажмите, чтобы выбрать и поставить башню. Для этого требуются ресурсы.",
                [LocalizationKeys.LEVEL_1_TUTORIAL_TEXT_3_KEY] = "Следите за количеством волн",
                [LocalizationKeys.LEVEL_1_TUTORIAL_TEXT_4_KEY] = "Добывайте ресурсы, уничтожая врагов",
                [LocalizationKeys.LEVEL_1_TUTORIAL_TEXT_5_KEY] = "Не дайте противникам добраться до базы, чтобы сохранить здоровье.",
                [LocalizationKeys.LEVEL_1_TUTORIAL_TEXT_6_KEY] = "Нажмите, чтобы поставить вторую башню",
                [LocalizationKeys.LEVEL_1_TUTORIAL_TEXT_7_KEY] = "Нажмите, чтобы поставить вторую башню",
                [LocalizationKeys.LEVEL_1_TUTORIAL_TEXT_8_KEY] = "Объедините башни",
                [LocalizationKeys.LEVEL_1_TUTORIAL_TEXT_9_KEY] = "Объединяйте одинаковые башни, чтобы получить их усиленную версию!",
                [LocalizationKeys.LEVEL_1_TUTORIAL_TEXT_10_KEY] = "Обучение завершено, удачной игры!",
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