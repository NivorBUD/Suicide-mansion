using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillLianaPoint : MonoBehaviour
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
        if (playerScript.inventory.ContainsKey("Flamethrower") && !sprite.enabled)
            Invoke(nameof(TurnOnHints), 0.5f);
        
    }

    private void TurnOnHints()
    {
        sprite.enabled = true;
        hint.isOn = true;
    }
}
