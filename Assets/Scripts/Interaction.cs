using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private bool IsHeroInArea = false;
    private SpriteRenderer[] Platform_render;
    private BoxCollider2D[] Platform_collider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        Platform_render = GetComponentsInChildren<SpriteRenderer>();
        Platform_collider = GetComponentsInChildren<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsHeroInArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsHeroInArea = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsHeroInArea && Input.GetKeyDown(KeyCode.E))
        {
            sprite.flipX = !sprite.flipX;
            foreach (var collider in Platform_collider)
            {
                Destroy(collider);
            }
            //foreach (var renderer in Platform_render)
            //{
            //    Destroy(renderer);
            //}
        }
    }
}
