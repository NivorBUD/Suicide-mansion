using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingLadder : MonoBehaviour
{
    public Sprite brokenLadder;
    public Sprite fixedLadder;

    [SerializeField]
    private Trigger breakTrigger;
    [SerializeField]
    private LadderInteraction ladderInteraction;

    private GameObject player;
    private Hero playerScript;


    void Start()
    {
        fixedLadder = GetComponent<SpriteRenderer>().sprite;
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
    }

    void Update()
    {
        if (breakTrigger.isTriggered)
        {
            playerScript.StopLift();
            GetComponent<SpriteRenderer>().sprite = brokenLadder;
            ladderInteraction.enabled = false;
        }
    }
}
