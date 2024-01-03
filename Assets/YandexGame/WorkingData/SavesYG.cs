
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
        public float[] playerPos = new float[3];
        public float[] playerHintDirection = new float[3];
        public bool canUseLadders;
        public bool canGetItems;
        public int levelComplete; // 1 - пройдена смерть в подвале, 2 - от рояля, 3 - от люстры, 4 - от мэри,
                                  // 5 - от растения, 6 - от утопленя, 7 - на чердаке, 8 - от молнии,
                                  // 9 - игра пройдена

        public string[] inventoryItems = new string[2];
        public bool[] isUsedInventoryItems = new bool[18];
        public bool[] activeColliderInventoryItems = new bool[18];

        public string[] ghostDialog;
        public float[] ghostPos = new float[3];
        public bool isGhostDialog;
        public int ghostPhraseIndex;
        public bool ghostCanChangePhraseByButton;

        public bool isMaryActive;
        public int maryPhraseIndex;

        public float[] pianoPos = new float[3];
        public float[] chandelierPos = new float[3];
        public float[] childRoomDoorPos = new float[3];
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
