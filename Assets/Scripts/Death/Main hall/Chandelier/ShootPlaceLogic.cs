using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ShootPlaceLogic : MonoBehaviour
{
    private ButtonHint hint;
    private Hero playerScript;
    private SpriteRenderer sprite;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        sprite = GetComponent<SpriteRenderer>();
        hint = GetComponent<ButtonHint>();
    }

    private void Update()
    {
        if (playerScript.inventory.ContainsKey("Keys") && playerScript.inventory.ContainsKey("Slingshot"))
        {
            hint.isOn = true;
            playerScript.ChangePointerAim(transform);
            sprite.enabled = true;
        }
        else
        {
            hint.isOn = false;
            sprite.enabled = false;
        }
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
