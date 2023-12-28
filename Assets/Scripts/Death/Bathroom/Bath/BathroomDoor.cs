using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomDoor : MonoBehaviour
{
    [SerializeField] private BathroomDoorTrigger trig;
    [SerializeField] private GameObject openedDoor;
    private Hero playerScript;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
    }

    private void Update()
    {
        if (trig.isPlayerInArea && Input.GetKeyUp(KeyCode.F) && playerScript.inventory.ContainsKey("Bathroom key"))
        {
            InventoryLogic.UseItem(playerScript.inventory["Bathroom key"]);
            openedDoor.SetActive(true);
            gameObject.SetActive(false);
            PlayOpenSound(); // звук открытия двери
            playerScript.StopPointerAiming();
        }

        if (playerScript.inventory.ContainsKey("Bathroom key"))
            playerScript.ChangePointerAim(transform);
    }

    private void PlayOpenSound()
    {

    }
}
