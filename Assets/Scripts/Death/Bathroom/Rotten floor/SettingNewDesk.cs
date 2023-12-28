using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingNewDesk : MonoBehaviour
{
    public Trigger trigger;
    public GameObject desk, bath;
    public Desk deskScript;

    private Hero playerScript;

    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
    }

    void Update()
    {
        if (trigger.isTriggered && playerScript.inventory.ContainsKey("Board") && Input.GetKeyUp(KeyCode.F))
            StartCoroutine(CutScene());
    }

    IEnumerator CutScene()
    {
        playerScript.isCutScene = true;
        
        InventoryLogic.UseItem(playerScript.inventory["Board"]);
        playerScript.StopPointerAiming();
        deskScript.GetAndMoveToHand();
        while (!deskScript.isReady)
            yield return new WaitForSeconds(0.1f);

        deskScript.SetAndDrop();
        while (!deskScript.isInstall)
            yield return new WaitForSeconds(0.1f);

        deskScript.rb.bodyType = RigidbodyType2D.Static;
        
        playerScript.isCutScene = false;
        playerScript.ChangePointerAim(bath.transform);
    }
}
