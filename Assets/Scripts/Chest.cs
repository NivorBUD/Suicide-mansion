using UnityEngine;

public class Chest : MonoBehaviour
{
    public Sprite openSprite;
    public EndCutScene endCutScene;

    private bool isPlayerInArea;
    private Hero playerScript;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
    }

    private void Update()
    {
        if (isPlayerInArea && playerScript.inventory.ContainsKey("Treasure key") && Input.GetKeyUp(KeyCode.F))
        {
            InventoryLogic.UseItem(playerScript.inventory["Treasure key"]);
            GetComponent<SpriteRenderer>().sprite = openSprite;
            Invoke(nameof(EndGame), 1);
        }
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
