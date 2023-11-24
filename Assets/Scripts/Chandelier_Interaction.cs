using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chandelier_Interaction : MonoBehaviour
{
    public GameObject[] targets;
    public GameObject candle;

    private Hero player_script;
    private bool isPlayerFall = false;
    private bool isDrop = false;

    private void Start()
    {
        player_script = GameObject.FindWithTag("Player").GetComponent<Hero>();
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

    private void FixedUpdate()
    {
        if (!isPlayerFall && gameObject.transform.position.y <= -1.5)
        {
            isPlayerFall = true;
            player_script.FallOnBack();
        }

        if (!isDrop && gameObject.transform.position.y < -3.2)
        {
            isDrop = true;
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Invoke(nameof(SpawnCandle), 0.5f);
        }
    }
}
