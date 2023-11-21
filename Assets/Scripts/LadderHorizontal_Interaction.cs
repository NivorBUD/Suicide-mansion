using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderHorizontal_Interaction : MonoBehaviour
{
    [SerializeField] Hero player;
    float speed = 5;

    private void OnTriggerStay2D(Collider2D collision)
    {
        player.rb.gravityScale = 0;
        if (Input.GetKey(KeyCode.W))
            player.rb.velocity = new Vector2(speed, speed);
        else if (Input.GetKey(KeyCode.S))
            player.rb.velocity = new Vector2(-speed, -speed);
        else
            player.rb.velocity = new Vector2(0, 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.rb.gravityScale = 3;
    }

    void Update()
    {
        
    }
}
