using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingDoorPoint : MonoBehaviour
{
    private Hero playerScript;
    private SpriteRenderer sprite;
    private ButtonHint hint;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        sprite = GetComponent<SpriteRenderer>();
        hint = GetComponent<ButtonHint>();
    }

    private void Update()
    {
        sprite.enabled = playerScript.inventory.ContainsKey("Axe");
        hint.isOn = playerScript.inventory.ContainsKey("Axe");

        if (hint.isOn)
            playerScript.ChangePointerAim(transform);
    }
}
