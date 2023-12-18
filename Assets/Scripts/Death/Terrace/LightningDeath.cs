using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LightningDeath : MonoBehaviour
{
    [SerializeField] private GameObject pantaloons, rope, treasureKey;
    [SerializeField] private Clouds cloudsScript;

    private Hero playerScript;
    private Pantaloons pantaloonsScript;
    private CameraController cameraController;
    private BoxCollider2D keyCollider;
    private Rigidbody2D keyrb;

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

        rope.SetActive(true);

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

        cameraController.ChangeAimToPlayer();

        playerScript.Death();

        cloudsScript.StopRain();
        cloudsScript.StopLightning();

        treasureKey.SetActive(true);
    }
}
