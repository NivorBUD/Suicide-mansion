using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBreak : MonoBehaviour
{
    public GameObject board;
    private Hero playerScript;
    [SerializeField] private Trigger trigger;
    [SerializeField] private GameObject blackOut;
    [SerializeField] private Axe axe;
    public AudioClip doorBreakSound; // The sound clip for breaking the door
    private bool hasTriggered;
    private ConstantForce2D force;
    private Rigidbody2D rb;
    private CameraController cameraController;
    private string[] dialog;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        cameraController = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        force = gameObject.GetComponent<ConstantForce2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        dialog = new string[] { "Здорово ты её!", "Напомнил моего знакомого, Джонни", "Славный был парень",
            "Жаль, с семейкой не повезло…", "И с лабиринтом…" };
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            hasTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            hasTriggered = false;
    }

    private void Update()
    {
        if (hasTriggered && playerScript.inventory.ContainsKey("Axe") && Input.GetKeyUp(KeyCode.F))
        {
            if (doorBreakSound != null)
            {
                AudioSource.PlayClipAtPoint(doorBreakSound, transform.position);
            }
            StartCoroutine(BreakDoor());
        }
    }

    private IEnumerator BreakDoor()
    {
        playerScript.isCutScene = true;

        cameraController.ChangeAim(playerScript.gameObject.transform);
        cameraController.ZoomIn(3);

        blackOut.SetActive(false);

        if (playerScript.inventory.ContainsKey("Axe"))
        {
            InventoryLogic.UseItem(playerScript.inventory["Axe"]);
        }
        else
        {
            Debug.LogError("Axe not found in inventory!");
            yield break;
        }

        playerScript.StopPointerAiming();

        axe.GetAndMoveToHand();
        while (!axe.isReady)
            yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < 3; i++)
        {
            axe.Hit();
            while (!axe.isReady)
                yield return new WaitForSeconds(0.1f);
        }

        Rigidbody2D doorRigidbody = gameObject.GetComponent<Rigidbody2D>();
        doorRigidbody.bodyType = RigidbodyType2D.Dynamic;
        axe.gameObject.SetActive(false);

        force.force = new Vector2(-0.5f, 0);
        yield return new WaitForSeconds(1f);

        while (rb.angularVelocity >= 0.01)
            yield return new WaitForSeconds(0.1f);

        board.SetActive(true);
        playerScript.ChangePointerAim(board.transform);
        GetComponent<BoxCollider2D>().isTrigger = true;
        rb.simulated = false;

        cameraController.ChangeAimToPlayer();

        playerScript.isCutScene = false;
        yield return new WaitForSeconds(1);

        blackOut.SetActive(true);

        playerScript.ghostScript.Show();
        playerScript.ghostScript.ChangeDialog(dialog);
        playerScript.ghostScript.mission = "Положить доску поперек дыры в ванной";
    }
}