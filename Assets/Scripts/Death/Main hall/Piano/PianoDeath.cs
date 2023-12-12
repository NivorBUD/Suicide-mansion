using UnityEngine;

public class PianoDeath : DeathClass
{
    private static bool isPlayerInPlace = false;
    private Hero playerScript;
    private GameObject player;

    public GameObject blackOut;
    public GameObject prop;
    public GameObject piano;
    public PolygonCollider2D col;
    public GameObject midPos;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
    }

    public override void StartDeath()
    {
        Destroy(prop);
        LadderHorizontalInteraction.StopUsingMidPos();
        col.enabled = true;
        InventoryLogic.UseItem(playerScript.inventory["Shovel"]);
        piano.GetComponent<Rigidbody2D>().simulated = true;
        midPos.SetActive(false);
    }

    public override bool ReadyToDeath()
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
        if (ReadyToDeath() && Input.GetKeyDown(KeyCode.F)) 
            StartDeath();
    }
}
