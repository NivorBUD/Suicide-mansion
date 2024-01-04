using System;
using UnityEngine;
using YG;

public class ChandelierInteraction : MonoBehaviour
{
    public GameObject[] targets;
    public GameObject candle;
    public Sprite breakSprite;
    public SpriteRenderer render;
    public bool isDrop = false;
    public AudioClip fallSound;
    [SerializeField] private ChangeImage deathopediaImage;

    private Hero playerScript;
    private GameObject player;
    private Rigidbody2D rb;
    private string[] dialog;
    private bool isEnd;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        dialog = new string[] {"Вот незадача", "Вернее задача…", "На ускорение свободного падения", "Ну ничего…",
            "Теперь есть повод поменять эту люстру", "Может поставить светодиодные свечи?",
            "Или воскосберегающие лампочки?", "...", "Знаешь, давно я не видела свою дочь…", "Нужно навестить её",
            "Возьми свечку", "Найди чем можно порисовать", "Ну и обведи мой старый рисунок",
            "Сделай это на зеркале в детской", "Мне кажется, Мэри тебе понравится"};
    }

    public void Fall()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
        AudioSource.PlayClipAtPoint(fallSound, transform.position);
    }

    private void SpawnCandle()
    {   
        
        candle.SetActive(true);
        var pos = transform.position;
        pos.x += 1.5f;
        pos.y -= 0.45f;
        candle.transform.position = pos;
    }

    private void Update()
    {
        if (playerScript.levelComplete >= 3 && !isEnd)
        {
            transform.position = new Vector3(YandexGame.savesData.chandelierPos[0],
                YandexGame.savesData.chandelierPos[1], YandexGame.savesData.chandelierPos[2]);
            EndDeath();
        }

        if (rb.simulated && transform.position.y <= -1.8 && transform.position.y > -3.6)
        {   
            var sc = player.transform.localScale;
            var mult = (1.8f - Math.Abs(-1.8f - transform.localPosition.y)) / 1.8f;
            sc.y = 0.4f * mult;
            player.transform.localScale = sc;
            player.transform.localScale = sc;
        }

        if (rb.simulated && !isDrop && transform.position.y < -3.1)
        {
            playerScript.levelComplete = 3;
            playerScript.Death();
            YandexGame.savesData.chandelierPos = new float[3] { transform.position.x, 
                transform.position.y , transform.position.z };
            playerScript.SaveSave();
            EndDeath();
        }
            
    }

    private void EndDeath()
    {
        if (playerScript.levelComplete == 3)
        {
            isEnd = true;
            playerScript.ghostScript.Show();
            playerScript.ghostScript.ChangeDialog(dialog);

            isDrop = true;
            rb.simulated = false;
            GetComponent<BoxCollider2D>().enabled = false;

            render.sprite = breakSprite;
            deathopediaImage.ChangeSprite();

            playerScript.ghostScript.mission = "Найти маркер и свечу для призыва Мэри";

            GameObject.FindWithTag("Mirror").GetComponent<MirrorDeath>().Prepare();
            Invoke(nameof(SpawnCandle), 0.25f);
        }
        
        if (playerScript.levelComplete > 3) 
        {
            render.sprite = breakSprite;
            deathopediaImage.ChangeSprite();
        }
    }

    private void PlayBreakSound() 
    { 

    }
}
