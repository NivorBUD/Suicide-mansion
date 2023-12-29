using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChandelierDeath : MonoBehaviour
{
    public GameObject blackOut1, blackOut2, bullet;
    public BoxCollider2D markerCollider;

    private ChandelierInteraction chandelierInteraction;
    private static bool isShoot = false;
    private static bool isPlayerInShootPlace = false;
    private static Hero playerScript;

    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        chandelierInteraction = gameObject.GetComponent<ChandelierInteraction>();
    }

    public bool ReadyToDeath()
    {
        return !isShoot && isPlayerInShootPlace && playerScript.inventory.ContainsKey("Keys") && playerScript.inventory.ContainsKey("Slingshot");
    }

    public static void EnterShootPlace()
    {
        isPlayerInShootPlace = true;
    }

    public static void ExitShootPlace()
    {
        isPlayerInShootPlace = false;
    }

    public void StartDeath()
    {
        blackOut1.SetActive(false);
        blackOut2.SetActive(false);
        playerScript.isCutScene = true;

        InventoryLogic.UseItem(playerScript.inventory["Keys"]);
        InventoryLogic.UseItem(playerScript.inventory["Slingshot"]);
        playerScript.StopPointerAiming();
        markerCollider.enabled = true;

        isShoot = true;
        bullet.transform.position = playerScript.bulletPlace.position;
        bullet.GetComponent<Bullet>().isStart = true;

        GameObject.FindWithTag("MainCamera").GetComponent<CameraController>().ZoomIn(2);
        GameObject.FindWithTag("MainCamera").GetComponent<CameraController>().ChangeAim(bullet.transform);
        
    }

    private void Update()
    {
        if (ReadyToDeath() && Input.GetKeyDown(KeyCode.F))
            StartDeath();

        if (chandelierInteraction.isDrop)
        {
            chandelierInteraction.isDrop = false;
            Invoke(nameof(TurnOnBlackOut), 3f);
        }
    }

    private void TurnOnBlackOut()
    {
        blackOut1.SetActive(true);
        blackOut2.SetActive(true);
    }
}
