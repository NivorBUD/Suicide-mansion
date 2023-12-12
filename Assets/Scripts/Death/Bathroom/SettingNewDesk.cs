using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingNewDesk : MonoBehaviour
{
    public Trigger trigger;
    public GameObject desk;
    private Hero playerScript;

    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
    }

    void Update()
    {
        if (trigger.isTriggered && !desk.activeSelf && playerScript.inventory.ContainsKey("Board"))
        {
            InventoryLogic.UseItem(playerScript.inventory["Board"]);
            desk.SetActive(true);
        }
    }
}
