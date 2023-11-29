using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackoutInteraction : MonoBehaviour
{
    private SpriteRenderer sr;

    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sr.enabled = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sr.enabled = true;
    }
}
