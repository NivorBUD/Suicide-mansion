using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float speed = 2f;
    public Transform player;
    private SpriteRenderer sprite;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Vector3 pos = player.position;
        pos.x -= 1.5f;
        pos.y += 1.5f;
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        sprite.flipX = transform.position.x < player.position.x;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
