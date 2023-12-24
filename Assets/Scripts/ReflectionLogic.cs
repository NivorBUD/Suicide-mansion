using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionLogic : MonoBehaviour
{
    public GameObject wall;

    private GameObject player;
    private Vector3 startPos;
    private SpriteRenderer sr;
    private SpriteRenderer playerSr;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        startPos = gameObject.transform.position;
        sr = gameObject.GetComponent<SpriteRenderer>();
        playerSr = player.GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        var playerPos = player.transform.position;
        if (15 >= playerPos.x && playerPos.x >= 13.1)
        {
            var xPos = wall.transform.position.x - Mathf.Abs(wall.transform.position.x - playerPos.x);
            var newPos = gameObject.transform.position;
            newPos.x = xPos;
            gameObject.transform.position = newPos;
            sr.flipX = !playerSr.flipX;
        }
        else
        {
            gameObject.transform.position = startPos;
        }
    }
}
