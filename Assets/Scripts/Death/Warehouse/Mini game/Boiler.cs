using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boiler : MonoBehaviour
{
    [SerializeField] GameObject gameArea, acid;
    [SerializeField] Sprite emptySprite;
    public bool isPlayerInArea;

    private Hero playerScript;
    private SpriteRenderer sr;
    private ButtonHint hint;

    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        sr = GetComponent<SpriteRenderer>();
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

    void Update()
    {
        if (isPlayerInArea && playerScript.inventory.ContainsKey("CaF2") 
            && playerScript.inventory.ContainsKey("H2SO4") && Input.GetKeyDown(KeyCode.F))
        {
            InventoryLogic.UseItem(playerScript.inventory["H2SO4"]);
            InventoryLogic.UseItem(playerScript.inventory["CaF2"]);
            gameArea.GetComponent<GameLogic>().StartGame();
            playerScript.StopPointerAiming();
        }

        if (isPlayerInArea && Input.GetKeyDown(KeyCode.E) && sr.sprite.name == "BoilerAcid")
        {
            sr.sprite = emptySprite;
            playerScript.StopPointerAiming();
        }
            
        hint.isOn = playerScript.inventory.ContainsKey("CaF2") && playerScript.inventory.ContainsKey("H2SO4");
    }
}
