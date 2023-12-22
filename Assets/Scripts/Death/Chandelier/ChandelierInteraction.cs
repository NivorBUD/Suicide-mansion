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
        dialog = new string[16] {"Oпа, люстра", "Давно она мне не нравилась", "Ну теперь есть повод поменять её",
            "Mожет поставить воскосберегающие свечи?", "Или вообще лампочки?", "Да не, свечей хватит",
            "Вдруг электричество отключат", "Ладно, потом решу", "...", "Кстати, давно я не видела свою дочь", "Возьми свечку",
            "Найди чем можно порисовать", "Ну и обведи мой старый рисунок", "Cделай это на зеркале", "Oно в детской комнате",
            "Давай, пошуршал быстренько"};
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
