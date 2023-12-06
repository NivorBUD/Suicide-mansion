using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomDoor : MonoBehaviour
{
    [SerializeField] private BathroomDoorTrigger trig;
    [SerializeField] private GameObject openedDoor;
    private bool isOpen = false;
    private Hero playerScript;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
    }

    private void Update()
    {
        if (trig.isPlayerInArea && Input.GetKeyUp(KeyCode.F) && playerScript.inventory.ContainsKey("Bathroom key Inv"))
        {
            InventoryLogic.UseItem(playerScript.inventory["Bathroom key Inv"]);
            isOpen = true;
            openedDoor.SetActive(true);
            gameObject.SetActive(false);
        }
    }


}
