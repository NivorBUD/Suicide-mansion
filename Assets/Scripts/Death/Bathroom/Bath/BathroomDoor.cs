using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomDoor : MonoBehaviour
{
    [SerializeField] private BathroomDoorTrigger trig;
    [SerializeField] private GameObject openedDoor;
    public AudioClip doorOpenSound;
    private Hero playerScript;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
    }

    private void Update()
    {
        if (playerScript.levelComplete >= 5 && !openedDoor.activeSelf)
        {
            openedDoor.SetActive(true);
            gameObject.SetActive(false);
        }

        if (trig.isPlayerInArea && Input.GetKeyUp(KeyCode.F) && playerScript.inventory.ContainsKey("Bathroom key"))
        {
            InventoryLogic.UseItem(playerScript.inventory["Bathroom key"]);
            openedDoor.SetActive(true);
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(doorOpenSound, transform.position);
        }
    }
}
