using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LianaTrigger : MonoBehaviour
{
    public bool isTriggered;

    void Start()
    {
        isTriggered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Liana"))
            isTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Liana"))
            isTriggered = false;
    }
}
