using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderInteraction : MonoBehaviour
{
    [SerializeField] GameObject anotherLadderPlace;

    private GameObject player;
    private Hero playerScript;
    private bool isPlayerInArea;

    private void Start()
    {
        isPlayerInArea = false;
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
    }

    private void Update()
    {
        if (isPlayerInArea && playerScript.cutSceneIndex != 0 && Input.GetKeyDown(KeyCode.F))
        {
            var pos = anotherLadderPlace.transform.position;
            pos.z = player.transform.position.z;
            player.transform.position = pos;
        }
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
