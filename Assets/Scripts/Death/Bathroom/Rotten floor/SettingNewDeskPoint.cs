using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingNewDeskPoint : MonoBehaviour
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
        hint.isOn = playerScript.inventory.ContainsKey("Board");
        sprite.enabled = playerScript.inventory.ContainsKey("Board");

        if (hint.isOn)
            playerScript.ChangePointerAim(transform);
    }
}
