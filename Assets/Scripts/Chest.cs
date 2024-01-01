using UnityEngine;

public class Chest : MonoBehaviour
{   
    public AudioClip openedChestSound;
    public Sprite openSprite;
    public EndCutScene endCutScene;

    private bool isPlayerInArea;
    private Hero playerScript;
    private ButtonHint hint;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        hint = GetComponent<ButtonHint>();
    }

    private void Update()
    {
        if (isPlayerInArea && playerScript.inventory.ContainsKey("Treasure key") && Input.GetKeyUp(KeyCode.F))
        {
            InventoryLogic.UseItem(playerScript.inventory["Treasure key"]);
            AudioSource.PlayClipAtPoint(openedChestSound, transform.position);
            GetComponent<SpriteRenderer>().sprite = openSprite;
            Invoke(nameof(EndGame), 1);
        }

        hint.isOn = playerScript.inventory.ContainsKey("Treasure key");
    }

    private void EndGame()
    {
        endCutScene.StartEndCutScene();
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
