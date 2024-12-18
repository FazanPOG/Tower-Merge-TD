using System.Collections.Generic;

namespace TowerMergeTD.Game.State
{
    public class RussianLocalizationAsset : ILocalizationAsset
    {
        private readonly Dictionary<string, string> _translationMap;
        
        public RussianLocalizationAsset()
        {
            _translationMap = new Dictionary<string, string>()
            {
                [LocalizationKeys.TOWER_MERGE_TD_KEY] = "Объединяй Башни ТД",
                [LocalizationKeys.SHOP_KEY] = "Mагазин",
                [LocalizationKeys.SETTINGS_KEY] = "Настройки",
                [LocalizationKeys.SOUND_KEY] = "Звук",
                [LocalizationKeys.MUSIC_KEY] = "Музыка",
                [LocalizationKeys.LEVEL_KEY] = "Уровень",
                [LocalizationKeys.LEVEL_LOCK_DESCRIPTION_KEY] = "Этот уровень закрыт.",
                [LocalizationKeys.TOWER_KEY] = "Башни",
                [LocalizationKeys.COIN_KEY] = "Монеты",
                [LocalizationKeys.GEM_KEY] = "Алмазы",
                [LocalizationKeys.EPISODE_KEY] = "Эпизод",
                [LocalizationKeys.PAUSE_KEY] = "Пауза",
                [LocalizationKeys.CONTINUE_KEY] = "Продолжить",
                [LocalizationKeys.RESTART_KEY] = "Заного",
                [LocalizationKeys.EXIT_KEY] = "Выход",
                [LocalizationKeys.LOSE_KEY] = "Поражение",
                [LocalizationKeys.COMPLETE_KEY] = "Победа!",
                [LocalizationKeys.SCORE_KEY] = "Счет",
                [LocalizationKeys.TIME_KEY] = "Время",
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
                [LocalizationKeys.FREE_KEY] = "Бесплатно!",
                [LocalizationKeys.BEST_KEY] = "Выгодно!",
                [LocalizationKeys.BONUS_KEY] = "Бонус",
                [LocalizationKeys.SOLD_KEY] = "Продано",
                [LocalizationKeys.WAIT_AD_KEY] = "Реклама скоро появится!",
                [LocalizationKeys.CANCEL_KEY] = "Отмена",
                [LocalizationKeys.CONFIRM_WATCH_AD_KEY] = "Хотите посмотреть рекламу?",
                [LocalizationKeys.GUN_KEY] = "Пулеметная башня",
                [LocalizationKeys.ROCKET_KEY] = "Ракетная башня",
                [LocalizationKeys.LASER_KEY] = "Лазерная башня",
                [LocalizationKeys.SNIPER_KEY] = "Снайперская башня",
                [LocalizationKeys.ELECTRIC_KEY] = "Электрическая башня",
                [LocalizationKeys.IMPACT_KEY] = "Ударная башня",
                [LocalizationKeys.RAPID_KEY] = "Пулеметная башня",
                [LocalizationKeys.GOD_KEY] = "Разрушитель",
                [LocalizationKeys.ROCKET_INFO_KEY] = "Стреляет ракетами, которые поражают нескольких врагов одновременно, нанося урон по области.",
                [LocalizationKeys.LASER_INFO_KEY] = "Постепенно увеличивает урон при непрерывной атаке одной цели.",
                [LocalizationKeys.RAPID_INFO_KEY] = "Невероятно низкий урон, но компенсирует это высокой скорострельностью.",
                [LocalizationKeys.GOD_INFO_KEY] = "??? Кажется, эта башня скрывает свою истинную мощь...",
                [LocalizationKeys.SNIPER_INFO_KEY] = "Имеет огромную дальность и урон, но медленную перезарядку. Простреливает несколько врагов насквозь.",
                [LocalizationKeys.ELECTRIC_INFO_KEY] = "Стреляет молнией, которая отскакивает от одного врага к другому, поражая цепочку целей.",
                [LocalizationKeys.IMPACT_INFO_KEY] = "Бьёт всех врагов в радиусе вокруг себя, нанося неплохой урон по площади.",
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