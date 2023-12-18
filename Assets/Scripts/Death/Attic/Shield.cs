using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Trigger trigger;
    public ElectricMinigameLogic gameLogic;
    public TerraceDoor terraceDoor;
    public BoxCollider2D ropeCollider, pantaloonsCollider;
    public Sprite playerElectric, skeletonElectric;

    private CameraController mainCamera;
    private GameObject player;
    private Hero playerScript;
    private bool isUsed;

    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
    }

    void Update()
    {
        if (!isUsed && trigger.isTriggered && Input.GetKeyDown(KeyCode.F))
        {
            isUsed = true;
            playerScript.isCutScene = true;
            gameLogic.StartGame();
            mainCamera.ZoomIn(1);
            mainCamera.ChangeAim(gameLogic.gameObject.transform);
        }
    }

    public void StartDeath()
    {
        PlayElectricShockSound(); // звук удара током на 3 секунды
        mainCamera.ZoomIn(2);
        mainCamera.ChangeAim(player.transform);

        StartCoroutine(DeathCutScene());
    }

    IEnumerator DeathCutScene()
    {
        playerScript.EletricSchock();
        yield return new WaitForSeconds(3);

        playerScript.Death();
        terraceDoor.Open();
        pantaloonsCollider.enabled = true;
        ropeCollider.enabled = true;
    }

    private void PlayElectricShockSound()
    {

    }
}
