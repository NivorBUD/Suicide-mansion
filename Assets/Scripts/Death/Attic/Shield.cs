using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Trigger trigger;
    public ElectricMinigameLogic gameLogic;
    public TerraceDoor terraceDoor;
    public BoxCollider2D ropeCollider, pantaloonsCollider;
    public Sprite playerElectric, skeletonElectric;
    public GameObject blackOut;

    private CameraController mainCamera;
    private GameObject player;
    private Hero playerScript;
    private bool isUsed;
    private string[] dialog;

    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
        dialog = new string[] {"Мда, из тебя электрик", "Как из меня человек", "Перчатки бы надел", 
            "Ладно, иди на веранду", "Там встретимся" };
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
        blackOut.SetActive(false);
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
        yield return new WaitForSeconds(2);

        playerScript.ghostScript.ChangeDialog(dialog);
        playerScript.ghostScript.ChangeAimToPlayer();
        playerScript.ghostScript.Show();
        yield return new WaitForSeconds(2);

        blackOut.SetActive(true);
    }

    private void PlayElectricShockSound()
    {

    }
}
