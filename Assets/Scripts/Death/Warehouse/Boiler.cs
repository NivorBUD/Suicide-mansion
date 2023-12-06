using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boiler : MonoBehaviour
{
    private Hero playerScript;
    private bool isPlayerInArea;

    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
    }

    private void StartGame()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerInArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerInArea = false;
    }

    void Update()
    {
        if (isPlayerInArea && playerScript.inventory.ContainsKey("CaF2") && playerScript.inventory.ContainsKey("H2SO4"))
        {

        }
    }
}
