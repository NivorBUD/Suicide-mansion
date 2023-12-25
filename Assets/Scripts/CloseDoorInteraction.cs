using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorInteraction : MonoBehaviour
{
    [SerializeField] GameObject anotherDoor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ChangeDoor();
    }

    private void ChangeDoor()
    {
        gameObject.SetActive(false);
        anotherDoor.SetActive(true);
    }
}
