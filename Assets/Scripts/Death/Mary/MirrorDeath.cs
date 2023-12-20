using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorDeath : MonoBehaviour
{
    public GameObject mary;
    public GameObject drawing;
    public GameObject button;
    public GameObject blackOut;

    private GameObject mainCamera;
    private CameraController cameraController;
    private bool isPlay;
    private bool isEnd;
    private static bool isPlayerInArea = false;
    private Hero player;

    public void Prepare()
    {
        drawing.SetActive(true);
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Hero>();
        isPlay = false;
        isEnd = false;
        mainCamera = GameObject.FindWithTag("MainCamera");
        cameraController = mainCamera.GetComponent<CameraController>();
    }

    public bool ReadyToDeath()
    {
        return isPlayerInArea && !isPlay && player.inventory.ContainsKey("Marker") && player.inventory.ContainsKey("Candle");
    }

    public void StartDeath()
    {
        blackOut.SetActive(false);
        InventoryLogic.UseItem(player.inventory["Marker"]);
        InventoryLogic.UseItem(player.inventory["Candle"]);
        isPlay = true;
        player.isCutScene = true;
        StartDraw();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerInArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerInArea = false;
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
        player.DeadlyScare();
        mary.GetComponent<Mary>().StartDialog();
        button.SetActive(false);
        Invoke(nameof(TurnOnBlackOut), 4.2f);
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

        if (ReadyToDeath() && Input.GetKeyDown(KeyCode.F))
            StartDeath();
    }
}
