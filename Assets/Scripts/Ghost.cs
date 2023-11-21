using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Ghost : MonoBehaviour
{
    [SerializeField] GameObject speechBox;
    [SerializeField] GameObject Button1;
    [SerializeField] GameObject Button2;
    [SerializeField] GameObject Button3;
    [SerializeField] GameObject Button4;
    [SerializeField] TextMeshPro textBox;
    public DeathClass deathScript;
    public int deathNum = 1;

    private float speed = 2f;
    private Transform player;
    private SpriteRenderer sprite;
    private string[] firstDialog = new string[9] { "У-у-у-у-у!", "Зря ты очутился в этом доме!", "Приготовься к своей погибели!", 
                                                    "Злостный... доставщик пиццы?", "Прошу меня извинить, сейчас я...", "Как же это остановить...",
                                                    "Не та кнопка...", "А может та?", "А, вот!"};
    public int phraseIndex = 0;
    private int dialogNum = 1;
    private bool isDialog = false;
    private string[] actualDialog;
    private bool isCutScene;
    private static GameObject actualDeathRoom;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Vector3 pos = GetPos();
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        sprite.flipX = transform.position.x < player.position.x;
        if (!isDialog && CheckToDialog())
        {
            ChangeDialog();
            StartDialog();
        }
    }

    private Vector3 GetPos()
    {
        Vector3 res;
        if (isCutScene)
        {
            if (0 <= phraseIndex && phraseIndex <= 3)
            {
                res = Button1.transform.position;
                res.x -= 0.55f;
                res.y -= 0.2f;
            }
            else if (phraseIndex == 4 || phraseIndex == 5)
            {
                res = Button2.transform.position;
                res.x += 0.7f;
                res.y += 0.2f;
            }
            else if (phraseIndex == 6 || phraseIndex == 7)
            {
                res = Button3.transform.position;
                res.x += 0.55f;
            }
            else
            {
                res = Button4.transform.position;
                res.x -= 0.7f;
                res.y -= 0.4f;
            }
        }
        else
        {
            res = player.position;
            res.x += 1.5f;
            res.y += 1.5f;
        }
        return res;
    }

    private void Update()
    {
        if (!isCutScene && isDialog && Input.GetKeyUp(KeyCode.F))
            ChangePhrase();
        if (!isCutScene && dialogNum == 1 && phraseIndex == 1)
        {
            phraseIndex = 0;
            StartDeath();
        }
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

    private bool CheckToDialog()
    {
        if (gameObject && transform.position.x == player.position.x + 1.5f && transform.position.y == player.position.y + 1.5f)
            return true;
        return false;
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

    private void ChangePhrase()
    {
        if (phraseIndex == actualDialog.Length - 1)
            EndDialog();
        else
        {
            phraseIndex++;
            textBox.text = actualDialog[phraseIndex];
        }
    }

    private void StartDeath()
    {
        speed = 3.0f;
        switch (deathNum)
        {
            case 1:
                isCutScene = true;
                deathScript.StartDeath();
                StartCoroutine(FirstDeath_COR());
                break;
        }
    }

    public static void ChangeDeath(GameObject newDeathRoom)
    {
        actualDeathRoom = newDeathRoom;
    }

    IEnumerator FirstDeath_COR()
    {
        ChangePhrase(); //1
        yield return new WaitForSeconds(1f);
        ChangePhrase(); //2
        yield return new WaitForSeconds(1f);
        ChangePhrase(); //3
        yield return new WaitForSeconds(1.5f);
        Destroy(Button1);
        Basement_Death.speed = 0.6f;
        
        ChangePhrase(); //4
        yield return new WaitForSeconds(1.5f);
        ChangePhrase(); //5
        yield return new WaitForSeconds(1.5f);
        Destroy(Button2);
        Basement_Death.speed = 0.9f;
        
        ChangePhrase(); //6
        yield return new WaitForSeconds(1.5f);
        ChangePhrase(); //7
        yield return new WaitForSeconds(1.5f);
        Destroy(Button3);
        Basement_Death.speed = 0.5f;
        ChangePhrase(); //8
        yield return new WaitForSeconds(1.5f);
        Destroy(Button4);
        Basement_Death.speed = 2f;

        yield return new WaitForSeconds(1.5f);
        ChangePhrase();
    }
}
