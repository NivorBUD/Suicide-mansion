using UnityEngine;

public class MirrorDeath : MonoBehaviour
{
    public GameObject mary, drawing, button, blackOut;
    public Trigger trigger;
    public ButtonHint hint;
    [SerializeField] private ChangeImage deathopediaImage;

    private GameObject mainCamera;
    private CameraController cameraController;
    private bool isPlay;
    private bool isEnd;
    private Hero playerScript;

    public void Prepare()
    {
        drawing.SetActive(true);
    }

    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        isPlay = false;
        isEnd = false;
        mainCamera = GameObject.FindWithTag("MainCamera");
        cameraController = mainCamera.GetComponent<CameraController>();
    }

    public bool ReadyToDeath()
    {
        return trigger.isTriggered && !isPlay && playerScript.inventory.ContainsKey("Marker") && playerScript.inventory.ContainsKey("Candle");
    }

    public void StartDeath()
    {
        blackOut.SetActive(false);
        InventoryLogic.UseItem(playerScript.inventory["Marker"]);
        InventoryLogic.UseItem(playerScript.inventory["Candle"]);
        playerScript.StopPointerAiming();
        isPlay = true;
        playerScript.isCutScene = true;
        StartDraw();
    }

    private void StartDraw()
    {
        cameraController.ChangeAim(gameObject.transform);
        cameraController.ZoomIn(1);
        drawing.GetComponent<DrawingLogic>().StartDraw();
    }

    public void SpawnMary()
    {
        cameraController.ZoomIn(5);
        mary.GetComponent<Mary>().Show();

        deathopediaImage.ChangeSprite();
        playerScript.DeadlyScare();

        mary.GetComponent<Mary>().StartDialog();
        button.SetActive(false);
        Invoke(nameof(TurnOnBlackOut), 5f);
    }

    private void TurnOnBlackOut()
    {
        blackOut.SetActive(true);
    }

    private void Update()
    {
        if (!isEnd && DrawingLogic.paintedPartsCount == 36)
        {
            button.SetActive(true);
            isEnd = true;
        }

        hint.isOn = playerScript.inventory.ContainsKey("Marker") && playerScript.inventory.ContainsKey("Candle");

        if (ReadyToDeath() && Input.GetKeyDown(KeyCode.F))
            StartDeath();
    }
}
