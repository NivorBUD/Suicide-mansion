using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlace_Logic : MonoBehaviour
{
    private Hero player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Hero>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.EnterShootPlace();
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        player.ExitShootPlace();
    }
}
