using System;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] GameObject speechBox;
    [SerializeField] TextMeshPro textBox;
    public float speed = 3f;
    public bool isDialog, needToStartDialog, needTerraceDialog, dialogIsStarting;
    public int phraseIndex = 0;
    public bool canChangePhraseByButton;

    private Transform aim;
    private float aimXDelta, aimYDelta;
    private SpriteRenderer sprite;
    private string[] actualDialog;
    private static GameObject player;
    private bool needToHide, needToShow;

    void Start()
    {
        needTerraceDialog = true;
        needToStartDialog = true;
        player = GameObject.FindWithTag("Player");
        sprite = gameObject.GetComponent<SpriteRenderer>();
        ChangeAim(gameObject.transform, 0, 0);
        canChangePhraseByButton = true;
        actualDialog = new string[] { "У-у-у-у-у!", "Зря ты очутился в этом доме!", "Приготовься к своей погибели!",
            "Злостный... доставщик пиццы?", "Прошу меня извинить, сейчас я...", "Как же это остановить...",
            "Не та кнопка...", "А может та?", "А, вот!", "Упс... Извини", "Так-так-так, как же там было… ",
            "Воскресни, несчастный, приди в этот мир!", "Абракадабра симсалабим!", 
            "Ух ты, вот и пригодились бабушкины заклинания!", "Итак, Доставщик…", "Ты, наверное, хочешь выбраться отсюда?",
            "Что ж, я тебя отпущу…", "Как только ты выполнишь все мои задания", 
            "Моё родовое поместье пришло в запустение", "И ты должен привести его в порядок", "Ты согласен?",
            "Не отвечай, я знаю, что да!", "Итак, нужно расчистить проход на второй этаж", 
            "Убери кучу хлама на лестнице", "Подбери лопату с помощью кнопки E", "Теперь подойди к мусору на лестнице",
            "Используй F чтобы применить предмет"};
    }

    public void ChangeAim(Transform newAim, float newXDelta, float newYDelta)
    {
        aimXDelta = newXDelta;
        aimYDelta = newYDelta;
        aim = newAim;
    }

    public void ChangeAimToPlayer()
    {
        aimXDelta = 1.5f;
        aimYDelta = 0.4f;
        aim = player.transform;
    }

    public void Hide()
    {
        aim = transform;
        needToHide = true;
        isDialog = false;
    }

    public void Show()
    {
        needToStartDialog = true;
        var pos = player.transform.position;
        pos.x += 6;
        pos.y += 3;
        pos.z = -4.5f;
        transform.position = pos;
        needToShow = true;
        ChangeAimToPlayer();
        InventoryLogic.canGetItems = false;
    }

    private void Update()
    {
        var pos = aim.position;
        pos.x += aimXDelta;
        pos.y += aimYDelta;
        pos.z = -1;
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        sprite.flipX = transform.position.x > aim.position.x;

        if (needToStartDialog && !isDialog && CheckIsNearTheAim())
            StartDialog();

        if (canChangePhraseByButton && dialogIsStarting && Input.GetKeyDown(KeyCode.F))
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
        dialogIsStarting = true;
        needToStartDialog = false;
        speechBox.SetActive(true);
        textBox.text = actualDialog[phraseIndex];
    }

    private void EndDialog()
    {
        dialogIsStarting = false;
        isDialog = false;
        needToStartDialog = false;
        speechBox.SetActive(false);
        textBox.text = actualDialog[phraseIndex];
        Hide();
        actualDialog = null;
        InventoryLogic.canGetItems = true;
    }

    public bool CheckIsNearTheAim()
    {
        return gameObject && Math.Abs(transform.position.x - (aim.position.x + aimXDelta)) <= 0.3f && 
            Math.Abs(transform.position.y - (aim.position.y + aimYDelta)) <= 0.3f && aim != transform;
    }

    public void ChangeDialog(string[] newDialog)
    {
        actualDialog = newDialog;
        phraseIndex = 0;
    }

    public void ChangePhrase()
    {
        if (actualDialog == null)
            return;
        if (phraseIndex == actualDialog.Length - 1)
            EndDialog();
        else
        {
            phraseIndex++;
            textBox.text = actualDialog[phraseIndex];
        }
    }
}
