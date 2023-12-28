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
        sprite.enabled = playerScript.inventory.ContainsKey("Flamethrower");
        hint.isOn = playerScript.inventory.ContainsKey("Flamethrower");

        if (hint.isOn)
            playerScript.ChangePointerAim(transform);
    }
}
