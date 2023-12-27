using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillLianaPoint : MonoBehaviour
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
        if (playerScript.inventory.ContainsKey("Flamethrower"))
            sprite.enabled = true;
        else
            sprite.enabled = false;
    }
}
