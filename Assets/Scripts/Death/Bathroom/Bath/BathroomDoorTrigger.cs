using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomDoorTrigger : MonoBehaviour
{
    public bool isPlayerInArea;

    private Hero playerScript;
    private ButtonHint hint;

    void Start()
    {
        isPlayerInArea = false;
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        hint = GetComponent<ButtonHint>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerInArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerInArea = false;
    }

    private void Update()
    {
        hint.isOn = playerScript.inventory.ContainsKey("Bathroom key");
    }
}
