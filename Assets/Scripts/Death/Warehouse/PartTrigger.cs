using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var render = collision.gameObject.GetComponent<SpriteRenderer>();
        if (collision.CompareTag("Liana") || collision.CompareTag("Liana part"))
            render.enabled = !render.enabled;
    }
}
