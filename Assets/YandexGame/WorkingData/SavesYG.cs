
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Ваши сохранения
        public bool needToLoadSave = false;

        public float[] playerPos = new float[3] { -14.94f, -3.14f, 0f};
        public float[] playerHintDirection = new float[3];
        public bool canUseLadders;
        public bool canGetItems;
        public int levelComplete; // 1 - пройдена смерть в подвале, 2 - от рояля, 3 - от люстры, 4 - от мэри,
                                  // 5 - от растения, 6 - от утопленя, 7 - на чердаке, 8 - от молнии,
                                  // 9 - игра пройдена

        public string[] inventoryItems = new string[2];
        public string mission = "Войти в дом";
        public bool[] isUsedInventoryItems = new bool[18];
        public bool[] activeColliderInventoryItems = new bool[18];

        public string[] ghostDialog = new string[] { "У-у-у-у-у!", "Зря ты очутился в этом доме!", "Приготовься к своей погибели!",
            "Злостный... доставщик пиццы?", "Прошу меня извинить, сейчас я...", "Как же это остановить...",
            "Не та кнопка...", "А может та?", "А, вот!", "Упс... Извини", "Так-так-так, как же там было… ",
            "Воскресни, несчастный, приди в этот мир!", "Абракадабра симсалабим!",
            "Ух ты, вот и пригодились бабушкины заклинания!", "Итак, Доставщик…", "Ты, наверное, хочешь выбраться отсюда?",
            "Что ж, я тебя отпущу…", "Как только ты выполнишь все мои задания",
            "Моё родовое поместье пришло в запустение", "И ты должен привести его в порядок", "Ты согласен?",
            "Не отвечай, я знаю, что да!", "Итак, нужно расчистить проход на второй этаж",
            "Убери кучу хлама на лестнице", "Подбери лопату с помощью кнопки F", "Теперь подойди к мусору на лестнице",
            "Используй F чтобы применить предмет"};
        public float[] ghostPos = new float[3];
        public bool isGhostDialog;
        public int ghostPhraseIndex;
        public bool ghostCanChangePhraseByButton;

        public bool isMaryActive;
        public int maryPhraseIndex;

        public float[] pianoPos = new float[3];
        public float[] chandelierPos = new float[3];
        public float[] masterRoomDoorPos = new float[3];

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива
            openLevels[1] = true;
        }
    }
}
