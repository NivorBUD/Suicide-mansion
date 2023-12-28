using TMPro;
using UnityEngine;

public class Mary : MonoBehaviour
{
    public GameObject bathroomKey;
    [SerializeField] GameObject speechBox;
    [SerializeField] TextMeshPro textBox;
    public bool isDialog;
    public int phraseIndex = 0;

    private SpriteRenderer sprite;
    private string[] dialog;
    private GameObject player;
    private static Hero playerScript;
    private bool needToHide, needToShow;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.color = new Color(255, 255, 255, 0);

        dialog = new string[] { "ПРИВЕ-Е-ЕТ!", "Ой...", "Да знаю, знаю",
            "Давно не видел таких красоток", "В обморок падать не обязательно", "А как ты здесь вообще оказался?",
            "Ладно, не отвечай","Ты наш первый посетитель за столько лет", "И потому, тебе вручается подарок",
            "Держи!", "Это ключ от ванной наверху", "Ну а я пошла отдыхать, прощай", "Была рада знакомству"};
    }

    public void Hide()
    {
        needToHide = true;
    }

    public void Show()
    {
        var pos = player.transform.position;
        pos.x += 1.5f;
        pos.y += 1;
        pos.z = -1;
        transform.position = pos;
        needToShow = true;
        InventoryLogic.canGetItems = false;
    }

    private void Update()
    {
        if (playerScript.isPause)
            return;

        if (isDialog && Input.GetKeyDown(KeyCode.F))
            ChangePhrase();

        if (needToHide)
        {
            sprite.color = new Color(255, 255, 255, Mathf.MoveTowards(sprite.color.a, 0, Time.deltaTime));
            if (sprite.color.a == 0)
                needToHide = false;
        }

        if (needToShow)
        {
            sprite.color = new Color(255, 255, 255, Mathf.MoveTowards(sprite.color.a, 1, Time.deltaTime));
            if (sprite.color.a == 1)
                needToShow = false;
        }
    }

    public void StartDialog()
    {
        isDialog = true;
        speechBox.SetActive(true);
        textBox.text = dialog[phraseIndex];
    }

    private void EndDialog()
    {
        isDialog = false;
        speechBox.SetActive(false);
        textBox.text = dialog[phraseIndex];
        Hide();
        dialog = null;

        string[] ghostDialog = new string[] { "Ну как она тебе?", "По-моему - <I>мертвецки</I> красивая", 
            "Ох, что-то я немного устала…", "Набери-ка мне ванную", "Она в ванной комнате… Как ни странно", 
            "Иди, чего встал-то?", "Ванная? Не для призрака? И слышать не хочу!"};
        player.GetComponent<Hero>().ghostScript.ChangeDialog(ghostDialog);
        player.GetComponent<Hero>().ghostScript.Show();
    }

    public void ChangePhrase()
    {
        if (dialog == null)
            return;
        if (phraseIndex == dialog.Length - 1)
            EndDialog();
        else
        {
            phraseIndex++;
            textBox.text = dialog[phraseIndex];
            if (phraseIndex == 11)
                GiveTheKey();
        }
    }

    private void GiveTheKey()
    {
        bathroomKey.SetActive(true);
    }
}
