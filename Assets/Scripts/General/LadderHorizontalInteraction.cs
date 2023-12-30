using UnityEngine;

public class LadderHorizontalInteraction : MonoBehaviour
{
    public GameObject anotherLadderPlace;
    public GameObject midLadderPos;
    public GameObject Railing;
    public static bool isUseMidPos;

    private Hero playerScript;
    private GameObject player;
    public bool isPlayerInArea;

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
        if (isPlayerInArea && Input.GetKeyDown(KeyCode.F) && 
            !playerScript.isCutScene && LadderInteraction.canUseLadders)
        {
            var pos = isUseMidPos ? midLadderPos.transform.position : anotherLadderPlace.transform.position;
            pos.z = player.transform.position.z;
            playerScript.StartLift(true, pos);
        }

        if (playerScript.isHorizontalLift || isUseMidPos)
            Railing.transform.localPosition = new Vector3(Railing.transform.localPosition.x, Railing.transform.localPosition.y, -1);
        else
            Railing.transform.localPosition = new Vector3(Railing.transform.localPosition.x, Railing.transform.localPosition.y, 0.1f);
    }
}
