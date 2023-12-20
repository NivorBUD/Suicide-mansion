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
    private bool needToHide, needToShow;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.color = new Color(255, 255, 255, 0);

        dialog = new string[15] { "Привет!", "Oй...", "Да знаю, знаю",
            "Давно не видел таких красоток", "Mог бы просто сказать", "Hезачем в обморок падать",
            "А как ты здесь вообще оказался?", "Ладно, не отвечай", "Ты наш сотый посетитель", "Hу или не сотый", 
            "B общем, у меня для тебя подарок", "Держи",
            "Это ключ от ванной наверху", "Cходи, посмотри, что там происходит", "Hу а я пошла отдыхать, прощай"};
    }

    public void Hide()
    {
        needToHide = true;
    }

    public void Show()
    {
        var pos = player.transform.position;
        pos.x += 4;
        pos.y += 2;
        pos.z = -1;
        transform.position = pos;
        needToShow = true;
    }

    private void Update()
    {
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
            sprite.color = new Color(255, 255, 255, Mathf.MoveTowards(sprite.color.a, 1, 0.5f * Time.deltaTime));
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
        InventoryLogic.canGetItems = true;
    }

    public void ChangeDialog(string[] newDialog)
    {
        dialog = newDialog;
        phraseIndex = 0;
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
            if (phraseIndex == 10)
                GiveTheKey();
        }
    }

    private void GiveTheKey()
    {
        bathroomKey.SetActive(true);
    }
}
