using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PianoDeath : MonoBehaviour
{
    public Heap heap;
    public Shovel shovel;

    private static bool isPlayerInPlace = false;
    private Hero playerScript;
    private GameObject player;
    private ButtonHint hint;

    public GameObject blackOut;
    public GameObject piano;
    public PolygonCollider2D col;
    public GameObject midPos;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
        hint = GetComponent<ButtonHint>();
    }

    public bool ReadyToDeath()
    {
        return isPlayerInPlace && playerScript.inventory.ContainsKey("Shovel") && !playerScript.ghostScript.isDialog;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerInPlace = true;
        if (playerScript.ghostScript.phraseIndex == 25)
            playerScript.ghostScript.ChangePhrase();
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
        hint.isOn = false;
        playerScript.StopPointerAiming();
        playerScript.isCutScene = true;
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
