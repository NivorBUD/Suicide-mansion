using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorDeath : DeathClass
{
    public GameObject mary;
    public GameObject drawing;
    public GameObject button;

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

    public override bool ReadyToDeath()
    {
        return isPlayerInArea && !isPlay && player.inventory.ContainsKey("Marker") && player.inventory.ContainsKey("Candle");
    }

    public override void StartDeath()
    {
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
        var pl = GameObject.FindWithTag("Player");
        var playerPos = pl.transform.position;
        Vector3 maryNewPos = playerPos;
        maryNewPos.x += 2; 
        maryNewPos.y += 1.5f; 
        mary.SetActive(true);
        mary.transform.position = maryNewPos;
        player.DeadlyScare();
        mary.GetComponent<Mary>().StartDialog();
        button.SetActive(false);
    }

    private void Update()
    {
        if (!isEnd && DrawingLogic.paintedPartsCount == 36)
        {
            button.SetActive(true);
            isEnd = true;
        }
    }
}
