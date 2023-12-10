using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderInteraction : MonoBehaviour
{
    [SerializeField] GameObject anotherLadderPlace;
    public static bool canUseLadders;

    private GameObject player;
    private bool isPlayerInArea;

    private void Start()
    {
        isPlayerInArea = false;
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (isPlayerInArea && canUseLadders && Input.GetKeyDown(KeyCode.F))
        {
            var pos = anotherLadderPlace.transform.position;
            pos.z = player.transform.position.z;
            player.transform.position = pos;
        }
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
}
