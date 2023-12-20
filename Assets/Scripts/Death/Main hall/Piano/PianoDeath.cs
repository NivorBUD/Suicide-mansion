using System.Collections;
using UnityEngine;

public class PianoDeath : MonoBehaviour
{
    public Heap heap;
    public Shovel shovel;

    private static bool isPlayerInPlace = false;
    private Hero playerScript;
    private GameObject player;

    public GameObject blackOut;
    public GameObject piano;
    public PolygonCollider2D col;
    public GameObject midPos;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
    }

    public bool ReadyToDeath()
    {
        return isPlayerInPlace && playerScript.inventory.ContainsKey("Shovel");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerInPlace = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerInPlace = false;
    }

    private void Update()
    {
        if (ReadyToDeath() && Input.GetKeyDown(KeyCode.F) && !playerScript.isCutScene) 
            StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        playerScript.isCutScene = true;
        LadderHorizontalInteraction.StopUsingMidPos();
        shovel.GetAndMoveToHand();

        while (!shovel.isReady)
            yield return new WaitForSeconds(0.1f);

        while (!heap.isReady)
        {
            shovel.Hit();

            while (!shovel.isReady)
                yield return new WaitForSeconds(0.1f);

            heap.ChangeSprite();
        }

        Destroy(shovel.gameObject);
        playerScript.isCutScene = false;

        col.enabled = true;
        InventoryLogic.UseItem(playerScript.inventory["Shovel"]);
        piano.GetComponent<Rigidbody2D>().simulated = true;
        midPos.SetActive(false);
    }
}
