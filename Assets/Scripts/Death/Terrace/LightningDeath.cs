using System.Collections;
using UnityEngine;

public class LightningDeath : MonoBehaviour
{   
    public AudioClip RainDeathSound;
    [SerializeField] private GameObject pantaloons, treasureKey, requestPlace;
    [SerializeField] private Clouds cloudsScript;     
    [SerializeField] private Sprite ropeSprite, lightningSprite;
    [SerializeField] private ChangeImage deathopediaImage;

    private Hero playerScript;
    private Pantaloons pantaloonsScript;
    private CameraController cameraController;
    private BoxCollider2D keyCollider;
    private Rigidbody2D keyrb;
    private SpriteRenderer sprite;
    private string[] dialog;
    private ButtonHint hint;

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
        dialog = new string[] {"Простите, о великие души предков", "Я верю, он это не со зла",
            "Ну вот, это было последнее задание", "Я <I>до конца смерти</I> тебе благодарна!", "Знаешь, я тут подумала…",
            "Нам всё равно уже ничего не нужно", "Возьми этот ключ",
            "А откуда он… Я думаю, ты догадаешься", "Удачи, и спасибо тебе за всё!"};
        hint = GetComponent<ButtonHint>();
    }

    private void Update()
    {
        if (treasureKey != null && treasureKey.transform.localPosition.y <= -3 && keyrb.velocity.y == 0)
        {
            keyCollider.isTrigger = true;
            keyrb.bodyType = RigidbodyType2D.Static;
        }
        hint.isOn = playerScript.inventory.ContainsKey("Pantaloons") && playerScript.inventory.ContainsKey("Rope");
    }

    IEnumerator CutScene()
    {
        InventoryLogic.UseItem(playerScript.inventory["Pantaloons"]);
        InventoryLogic.UseItem(playerScript.inventory["Rope"]);
        AudioSource.PlayClipAtPoint(RainDeathSound, transform.position);
        playerScript.StopPointerAiming();
        playerScript.ChangePointerAim(treasureKey.transform);

        sprite.sprite = ropeSprite;

        cameraController.ChangeAim(playerScript.gameObject.transform);
        cameraController.ZoomIn(2);

        pantaloons.SetActive(true);
        pantaloonsScript.GetAndMoveToHand();

        while (!pantaloonsScript.isReady)
            yield return new WaitForSeconds(0.1f);

        cameraController.ChangeAim(pantaloons.transform);

        pantaloonsScript.MoveToUpPos();
        playerScript.UpHands();

        while (!pantaloonsScript.isReadyToLightning)
            yield return new WaitForSeconds(0.1f);

        cameraController.ZoomIn(10);
        cameraController.ChangeAim(cloudsScript.gameObject.transform);
        cloudsScript.Move();

        while (!cloudsScript.isReady)
            yield return new WaitForSeconds(0.1f);

        cloudsScript.StartRain();
        playerScript.StopHandsUp();
        yield return new WaitForSeconds(2);

        cameraController.ZoomIn(5);
        cameraController.ChangeAim(playerScript.gameObject.transform);
        yield return new WaitForSeconds(2); 

        cloudsScript.StartLightning();
        sprite.sprite = lightningSprite;
        playerScript.EletricSchock();
        yield return new WaitForSeconds(3);

        deathopediaImage.ChangeSprite();
        playerScript.NoRespawnDeath();
        cloudsScript.StopLightning();
        sprite.sprite = ropeSprite;
        yield return new WaitForSeconds(2);

        playerScript.ghostScript.ChangeDialog(dialog);
        playerScript.ghostScript.Show();
        playerScript.ghostScript.mission = "Открыть ключом королевский сундук";
        playerScript.ghostScript.ChangeAim(requestPlace.transform, 0, 0);
        playerScript.ghostScript.canChangePhraseByButton = false;
        yield return new WaitForSeconds(5);

        playerScript.ghostScript.ChangePhrase();
        yield return new WaitForSeconds(3);

        playerScript.ghostScript.ChangeAimToPlayer();
        playerScript.ghostScript.ChangePhrase();
        playerScript.RespawnPoof();
        playerScript.EndCutScene();
        cloudsScript.StopRain();

        yield return new WaitForSeconds(1.5f);
        playerScript.ghostScript.canChangePhraseByButton = true;

        while (playerScript.ghostScript.phraseIndex < 6)
            yield return new WaitForSeconds(0.1f);

        treasureKey.SetActive(true);
    }
}
