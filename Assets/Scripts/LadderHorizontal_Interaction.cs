using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LadderHorizontalInteraction : MonoBehaviour
{
    public GameObject anotherLadderPlace;
    public GameObject midLadderPos;
    public static bool isUseMidPos;

    private Hero playerScript;
    private GameObject player;
    private bool isPlayerInArea;

    private void Start()
    {
        isUseMidPos = true;
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isPlayerInArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isPlayerInArea = false;
    }

    public static void StopUsingMidPos()
    {
        isUseMidPos = false;
    }

    void Update()
    {
        if (isPlayerInArea && Input.GetKeyDown(KeyCode.F))
        {
            var pos = isUseMidPos ? midLadderPos.transform.position : anotherLadderPlace.transform.position;
            pos.z = player.transform.position.z;
            player.transform.position = pos;
        }
    }
}
