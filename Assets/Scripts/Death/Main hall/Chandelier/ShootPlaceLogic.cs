using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlaceLogic : MonoBehaviour
{
    private Hero playerScript;
    private SpriteRenderer sprite;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerScript.inventory.ContainsKey("Keys") && playerScript.inventory.ContainsKey("Slingshot"))
            sprite.enabled = true;
        else
            sprite.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChandelierDeath.EnterShootPlace();
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        ChandelierDeath.ExitShootPlace();
    }
}