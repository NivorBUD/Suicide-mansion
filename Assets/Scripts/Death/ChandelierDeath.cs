using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChandelierDeath : DeathClass
{
    public GameObject bullet;
    private static bool isShoot = false;
    private static bool isPlayerInShootPlace = false;
    private static Hero player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Hero>();
    }

    public override bool ReadyToDeath()
    {
        return (!isShoot && isPlayerInShootPlace && player.inventory.ContainsKey("Keys Inv") 
            && player.inventory.ContainsKey("Slingshot Inv"));
    }

    public static void EnterShootPlace()
    {
        isPlayerInShootPlace = true;
    }

    public static void ExitShootPlace()
    {
        isPlayerInShootPlace = false;
    }

    public override void StartDeath()
    {
        InventoryLogic.UseItem(player.inventory["Keys Inv"]);
        InventoryLogic.UseItem(player.inventory["Slingshot Inv"]);
        isShoot = true;
        bullet.transform.position = player.bulletPlace.position;
        bullet.GetComponent<Bullet>().isStart = true;
        player.isCutScene = true;
        GameObject.FindWithTag("MainCamera").GetComponent<CameraController>().ChangeAim(bullet.transform);
    }
}
