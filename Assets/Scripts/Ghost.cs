using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Ghost : MonoBehaviour
{
    [SerializeField] GameObject speechBox;
    [SerializeField] TextMeshPro textBox;
    public float speed = 2f;
    public bool isDialog = false;
    public int phraseIndex = 0;
    public bool canChangePhraseByButton;

    private Transform aim;
    private float aimXDelta;
    private float aimYDelta;
    private SpriteRenderer sprite;
    private string[] firstDialog = new string[9] { "У-у-у-у-у!", "Зря ты очутился в этом доме!", "Приготовься к своей погибели!", 
                                                    "Злостный... доставщик пиццы?", "Прошу меня извинить, сейчас я...", "Как же это остановить...",
                                                    "Не та кнопка...", "А может та?", "А, вот!"};
    private int dialogNum = 1;
    private string[] actualDialog;
    private static GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        sprite = gameObject.GetComponent<SpriteRenderer>();
        ChangeAim(gameObject.transform, 0, 0);
        canChangePhraseByButton = true;
    }

    void FixedUpdate()
    {
        var pos = aim.position;
        pos.x += aimXDelta;
        pos.y += aimYDelta;
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        sprite.flipX = transform.position.x < pos.x;
        if (!isDialog && CheckIsNearThePlayer())
        {
            ChangeDialog();
            StartDialog();
        }
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
        aimYDelta = 1.5f;
        aim = player.transform;
    }

    private void Update()
    {
        if (canChangePhraseByButton && isDialog && Input.GetKeyDown(KeyCode.F))
            ChangePhrase();
    }

    public void StartDialog()
    {
        isDialog = true;
        speechBox.SetActive(true);
        textBox.text = actualDialog[phraseIndex];
    }

    private void EndDialog()
    {
        isDialog = false;
        speechBox.SetActive(false);
        textBox.text = actualDialog[phraseIndex];
        gameObject.SetActive(false);
    }

    public bool CheckIsNearThePlayer()
    {
        return gameObject && transform.position.x == player.transform.position.x + 1.5f && transform.position.y == player.transform.position.y + 1.5f;
    }

    private void ChangeDialog()
    {
        switch (dialogNum)
        {
            case 1:
                actualDialog = firstDialog;
                break;
        }
        phraseIndex = 0;
    }

    public void ChangePhrase()
    {
        if (phraseIndex == actualDialog.Length - 1)
            EndDialog();
        else
        {
            phraseIndex++;
            textBox.text = actualDialog[phraseIndex];
        }
    }
}
