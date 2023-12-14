using UnityEngine;

public class Shield : MonoBehaviour
{
    public Trigger trigger;
    public ElectricMinigameLogic gameLogic;
    public TerraceDoor terraceDoor;
    public BoxCollider2D ropeCollider, pantaloonsCollider;

    private CameraController mainCamera;
    private GameObject player;
    private Hero playerScript;

    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
    }

    void Update()
    {
        if (trigger.isTriggered && Input.GetKeyDown(KeyCode.F))
        {
            playerScript.isCutScene = true;
            gameLogic.StartGame();
            mainCamera.ZoomIn(1);
            mainCamera.ChangeAim(gameLogic.gameObject.transform);
        }
    }

    public void StartDeath()
    {
        mainCamera.ChangeAim(player.transform);

        terraceDoor.Open();

        playerScript.EndCutScene();

        pantaloonsCollider.enabled = true;
        ropeCollider.enabled = true;
    }
}
