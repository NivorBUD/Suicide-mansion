using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenDoorInteraction : MonoBehaviour
{
    public bool isBroke;

    private Rigidbody2D rb;
    private BoxCollider2D bc;

    public void Break()
    {
        isBroke = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(new Vector2(-10, 5), ForceMode2D.Impulse);
    }

    void Start()
    {
        isBroke = false;    
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (isBroke && rb.velocity.x == 0)
        {
            rb.simulated = false;
            bc.enabled = false;
        }
    }
}
