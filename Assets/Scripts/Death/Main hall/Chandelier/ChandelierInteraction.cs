using System;
using UnityEngine;

public class ChandelierInteraction : MonoBehaviour
{
    public GameObject[] targets;
    public GameObject candle;
    public Sprite breakSprite;
    public SpriteRenderer render;
    public bool isDrop = false;

    private Hero playerScript;
    private GameObject player;
    private Rigidbody2D rb;
    private string[] dialog;

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
        if (rb.simulated && transform.position.y <= -1.8 && transform.position.y > -3.6)
        {
            var sc = player.transform.localScale;
            var mult = (1.8f - Math.Abs(-1.8f - transform.localPosition.y)) / 1.8f;
            sc.y = 0.4f * mult;
            player.transform.localScale = sc;
            player.transform.localScale = sc;
        }

        if (rb.simulated && !isDrop && transform.position.y < -3.1)
            EndDeath();
    }

    private void EndDeath()
    {
        playerScript.ghostScript.Show();
        playerScript.ghostScript.ChangeDialog(dialog);
        isDrop = true;
        rb.simulated = false;
        GetComponent<BoxCollider2D>().enabled = false;
        PlayBreakSound(); // звук ломания люстры
        render.sprite = breakSprite;
        playerScript.Death();
        GameObject.FindWithTag("Mirror").GetComponent<MirrorDeath>().Prepare();
        Invoke(nameof(SpawnCandle), 0.25f);
    }

    private void PlayBreakSound() 
    { 

    }
}
