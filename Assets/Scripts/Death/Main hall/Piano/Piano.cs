using System;
using System.Collections;
using UnityEngine;

public class Piano : MonoBehaviour
{
    public BrokenDoorInteraction door;
    public GameObject key;
    public Transform keyPos;
    public PianoDeath deathScript;
    public bool isEnd;
    public BoxCollider2D floorCollider;
    public Sprite[] breakSprites;
    [SerializeField] private ChangeImage deathopediaImage;

    private Hero playerScript;
    private GameObject player;
    private Rigidbody2D rb;
    private PolygonCollider2D col;
    private string[] dialog = new string[] { "Ого!", "Спустить рояль было следующим заданием…",
        "Но ты схватываешь всё на лету!", "Молодец, слушай дальше", "Видишь ту летучую мышь на перилах?",
        "За эти 150 лет она меня уже достала!", "Можешь прогнать её оттуда?", "Кажется в детской лежит рогатка",
        "А на счёт снаряда…", "Поищи сам что-нибудь подходящее", "Вперёд!"};


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        col = gameObject.GetComponent<PolygonCollider2D>();

        rb.centerOfMass = new Vector2(0, -1.5f);
    }

    void Update()
    {
        if (transform.localPosition.x <= -10.7f && transform.localPosition.x > -11.35f)
        {
            var sc = player.transform.localScale;
            var mult = Math.Max((0.65f - Math.Abs(-10.7f - transform.localPosition.x)) / 0.65f, 0.2f);
            sc.x = 0.4f * mult;
            player.transform.localScale = sc;
        }

        if (!door.isBroke && transform.localPosition.x <= -11)
        {
            BreakDoor();
            StartCoroutine(Breaking());
            PlayBreakPianoSound(); //звук ломания пианино
        }

        if (!isEnd && playerScript.isCutScene && Math.Abs(playerScript.rb.velocity.x) <= 0.5f &&
            (Math.Abs(Math.Round(player.transform.rotation.eulerAngles.z, 1) - 90) < 0.1f || Math.Abs(Math.Round(player.transform.rotation.eulerAngles.z, 1) - 270) < 0.1f))
        {
            deathopediaImage.ChangeSprite();
            playerScript.Death();
            Invoke(nameof(ShowGhost), 2.5f);
            Invoke(nameof(TurnOnBlackOut), 3.5f);
        }

        if (!isEnd && transform.localPosition.x < -11 && rb.velocity.x == 0)
            EndDeath();
    }

    private void PlayBreakPianoSound()
    {

    }

    IEnumerator Breaking()
    {
        LadderHorizontalInteraction.StopUsingMidPos();
        var spriteRender = GetComponent<SpriteRenderer>();

        foreach (var sprite in breakSprites)
        {
            spriteRender.sprite = sprite;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void ShowGhost()
    {
        playerScript.ghostScript.Show();
        playerScript.ghostScript.ChangeDialog(dialog);
        playerScript.ghostScript.mission = "Взять рогатку и какой-то патрон";
    }

    private void BreakDoor()
    {
        var speed = rb.velocity;
        speed.x /= 2;
        rb.velocity = speed;

        door.Break();

        playerScript.rb.AddForce(new Vector2(-10, 5), ForceMode2D.Impulse);
        playerScript.rb.freezeRotation = false;
        playerScript.isCutScene = true;
        deathScript.blackOut.SetActive(false);
    }

    private void EndDeath()
    {
        floorCollider.enabled = true;
        key.transform.position = keyPos.position;
        key.SetActive(true);
        
        isEnd = true;
        rb.simulated = false;
        col.enabled = false;
        deathScript.col.enabled = false;
    }

    private void TurnOnBlackOut()
    {
        deathScript.blackOut.SetActive(true);
    }
}
