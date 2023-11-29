using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorDeath : DeathClass
{
    public GameObject mary;
    private bool isPlay;
    private static bool isPlayerInArea = false;

    private Hero player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Hero>();
        isPlay = false;
    }

    public override bool ReadyToDeath()
    {
        return isPlayerInArea && !isPlay && player.inventory.ContainsKey("Marker Inv") && player.inventory.ContainsKey("Candle Inv");
    }

    public override void StartDeath()
    {
        InventoryLogic.UseItem(player.inventory["Marker Inv"]);
        InventoryLogic.UseItem(player.inventory["Candle Inv"]);
        isPlay = true;
        player.isCutScene = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerInArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerInArea = false;
    }
}
