using System;
using UnityEngine;

public class Chandelier_Interaction : MonoBehaviour
{
    public GameObject[] targets;
    public GameObject candle;

    private Hero playerScript;
    private GameObject player;
    private bool isDrop = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
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
            var sc = player.transform.localScale;
            sc.y = Math.Abs(-3.1f - gameObject.transform.position.y);
            player.transform.localScale = sc;
        }

        if (playerScript.rb.freezeRotation && !isDrop && gameObject.transform.position.y < -3.1)
        {
            isDrop = true;
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            playerScript.EndCutScene();
            playerScript.Death();
            Invoke(nameof(SpawnCandle), 0.25f);
        }
    }
}
