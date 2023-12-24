using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boiler : MonoBehaviour
{
    [SerializeField] GameObject gameArea;
    [SerializeField] GameObject acid;
    [SerializeField] Sprite emptySprite;
    private Hero playerScript;
    private SpriteRenderer sr;

    public bool isPlayerInArea;

    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        sr = GetComponent<SpriteRenderer>();
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
        }

        if (isPlayerInArea && Input.GetKeyDown(KeyCode.E) && sr.sprite.name == "BoilerAcid")
            sr.sprite = emptySprite;
    }
}
