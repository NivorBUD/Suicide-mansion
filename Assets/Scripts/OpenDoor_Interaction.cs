using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor_Interaction : MonoBehaviour
{
    [SerializeField] GameObject anotherDoor;


    private void OnTriggerExit2D(Collider2D collision)
    {
        ChangeDoor();
    }

    private void ChangeDoor()
    {
        gameObject.SetActive(false);
        anotherDoor.SetActive(true);
    }
}
