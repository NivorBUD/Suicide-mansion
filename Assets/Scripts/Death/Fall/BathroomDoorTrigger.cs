using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomDoorTrigger : MonoBehaviour
{
    public bool isPlayerInArea;

    void Start()
    {
        isPlayerInArea = false;
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
