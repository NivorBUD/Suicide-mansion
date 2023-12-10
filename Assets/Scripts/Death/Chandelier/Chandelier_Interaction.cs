using System;
using UnityEngine;

public class Chandelier_Interaction : MonoBehaviour
{
    public GameObject[] targets;
    public GameObject candle;

    private Hero playerScript;
    private GameObject player;
    private Rigidbody2D rb;
    public bool isDrop = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Fall()
    {
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
    }

    private void SpawnCandle()
    {
        var pos = gameObject.transform.position;
        pos.x += 1.5f;
        pos.y -= 0.45f;
        candle.transform.position = pos;
    }

    private void Update()
    {
        if (gameObject.transform.position.y <= -1.5 && gameObject.transform.position.y > -3.1)
        {
            var scale = player.transform.localScale;
            scale.y = Math.Abs(-3.1f - gameObject.transform.position.y);
            player.transform.localScale = scale;
        }

        if (rb.simulated && !isDrop && gameObject.transform.position.y < -3.1)
        {
            isDrop = true;
            rb.simulated = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            playerScript.Death();
            GameObject.FindWithTag("Mirror").GetComponent<MirrorDeath>().Prepare();
            Invoke(nameof(SpawnCandle), 0.25f);
        }
    }
}
