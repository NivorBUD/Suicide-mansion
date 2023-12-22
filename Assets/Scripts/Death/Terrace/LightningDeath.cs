using System.Collections;
using UnityEngine;

public class LightningDeath : MonoBehaviour
{
    [SerializeField] private GameObject pantaloons, treasureKey;
    [SerializeField] private Clouds cloudsScript;   
    [SerializeField] private Sprite ropeSprite, lightningSprte;

    private Hero playerScript;
    private Pantaloons pantaloonsScript;
    private CameraController cameraController;
    private BoxCollider2D keyCollider;
    private Rigidbody2D keyrb;
    private SpriteRenderer sprite;
    private string[] dialog;

    public void StartDeath()
    {
        StartCoroutine(CutScene());
        playerScript.isCutScene = true;
    }

    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        cameraController = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        pantaloonsScript = pantaloons.GetComponent<Pantaloons>();
        keyCollider = treasureKey.GetComponent<BoxCollider2D>();
        keyrb = treasureKey.GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        dialog = new string[] {"Ну вот и всё", "Хоть и не повезло с молнией", "Но спасибо за помощь", "Этот ключ тебе",
            "Он откроет сокровища", "Но их надо найти в доме"};
    }

    private void Update()
    {
        if (treasureKey != null && treasureKey.transform.localPosition.y <= -3 && keyrb.velocity.y == 0)
        {
            keyCollider.isTrigger = true;
            keyrb.bodyType = RigidbodyType2D.Static;
        }
    }

    IEnumerator CutScene()
    {
        InventoryLogic.UseItem(playerScript.inventory["Pantaloons"]);
        InventoryLogic.UseItem(playerScript.inventory["Rope"]);

        sprite.sprite = ropeSprite;

        cameraController.ChangeAim(playerScript.gameObject.transform);
        cameraController.ZoomIn(2);

        pantaloons.SetActive(true);
        pantaloonsScript.GetAndMoveToHand();

        while (!pantaloonsScript.isReady)
            yield return new WaitForSeconds(0.1f);

        cameraController.ChangeAim(pantaloons.transform);

        pantaloonsScript.MoveToUpPos();

        while (!pantaloonsScript.isReadyToLightning)
            yield return new WaitForSeconds(0.1f);

        cameraController.ZoomIn(10);
        cameraController.ChangeAim(cloudsScript.gameObject.transform);
        cloudsScript.Move();

        while (!cloudsScript.isReady)
            yield return new WaitForSeconds(0.1f);

        cloudsScript.StartRain();
        cloudsScript.StartLightning();
        yield return new WaitForSeconds(3);

        cameraController.ZoomIn(5);
        cameraController.ChangeAim(playerScript.gameObject.transform);
        playerScript.EletricSchock();
        yield return new WaitForSeconds(3);

        playerScript.Death();
        cloudsScript.StopLightning();
        yield return new WaitForSeconds(2);

        playerScript.ghostScript.ChangeDialog(dialog);
        playerScript.ghostScript.Show();

        yield return new WaitForSeconds(7);

        cloudsScript.StopRain();
        treasureKey.SetActive(true);
    }
}
