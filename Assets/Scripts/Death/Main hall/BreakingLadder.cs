using System;
using UnityEngine;

public class BreakingLadder : MonoBehaviour
{
    [SerializeField] private Sprite brokenLadder, fixedLadder;
    [SerializeField] private LadderInteraction ladderInteraction;
    [SerializeField] private Trigger breakTrigger, upTrigger, downTrigger, repairTrigger;
    [SerializeField] private PolygonCollider2D mainLadderCollider;
    [SerializeField] private BoxCollider2D floorCollider, screwdriverCollider, screwsCollider;

    private GameObject player;
    private Hero playerScript;
    private bool isStart;
    private bool isEnd = false;
    private bool needToSetAngularVelocity;
    private CameraController cameraController;


    void Start()
    {
        fixedLadder = GetComponent<SpriteRenderer>().sprite;
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
        cameraController = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
    }

    void Update()
    {
        if (breakTrigger.isTriggered)
            Break();

        if (needToSetAngularVelocity)
            playerScript.rb.angularVelocity = -300;

        if (!isStart && !isEnd && !ladderInteraction.enabled && playerScript.rb.velocity.y == 0 && player.transform.position.y < 3.6)
        {
            needToSetAngularVelocity = true;
            isStart = true;
            player.GetComponent<BoxCollider2D>().enabled = false;
            player.GetComponent<CircleCollider2D>().enabled = true;
        }

        if (isStart)
        {
            if (!isEnd)
            {
                if (downTrigger.isTriggered && !floorCollider.enabled)
                    if (Math.Abs(player.transform.localEulerAngles.z - 90) <= 8f)
                        EndDeath();
                
                if (upTrigger.isTriggered && floorCollider.enabled)
                {
                    floorCollider.enabled = false;
                    mainLadderCollider.enabled = true;
                    needToSetAngularVelocity = false;
                    playerScript.rb.angularVelocity = -100;
                }
            }

            if (isEnd && playerScript.rb.velocity.x == 0)
            {
                playerScript.Up();
                isStart = false;
            }
        }

        if (repairTrigger.isTriggered && isEnd && !isStart && playerScript.inventory.ContainsKey("Screws") 
            && playerScript.inventory.ContainsKey("Screwdriver") && Input.GetKeyUp(KeyCode.F))
            RepairStairs();
    }

    private void Break()
    {
        playerScript.StopLift();
        playerScript.isCutScene = true;
        cameraController.ZoomIn(2);
        cameraController.ChangeAim(player.transform);
        playerScript.rb.freezeRotation = false;
        GetComponent<SpriteRenderer>().sprite = brokenLadder;
        ladderInteraction.enabled = false;
    }

    private void EndDeath()
    {
        cameraController.ChangeAimToPlayer();
        player.transform.eulerAngles = new Vector3(0, 0, 90);
        playerScript.rb.freezeRotation = true;
        playerScript.rb.angularVelocity = 0;
        player.GetComponent<BoxCollider2D>().enabled = true;
        player.GetComponent<CircleCollider2D>().enabled = false;
        floorCollider.enabled = true;
        mainLadderCollider.enabled = false;
        isEnd = true;
        screwdriverCollider.enabled = true;
        screwsCollider.enabled = true;
        breakTrigger.gameObject.SetActive(false);
    }

    private void RepairStairs()
    {
        InventoryLogic.UseItem(playerScript.inventory["Screws"]);
        InventoryLogic.UseItem(playerScript.inventory["Screwdriver"]);
        GetComponent<SpriteRenderer>().sprite = fixedLadder;
        ladderInteraction.enabled = true;
    }
}
