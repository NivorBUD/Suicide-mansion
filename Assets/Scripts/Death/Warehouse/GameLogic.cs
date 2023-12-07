using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public GameObject activeBall;
    public SpriteRenderer activeBallRenderer;
    [SerializeField] GameObject acid;

    [SerializeField] private Bottle bottle1;
    [SerializeField] private Bottle bottle2;
    [SerializeField] private Bottle bottle3;
    [SerializeField] private Bottle bottle4;
    private CameraController cameraController;
    private Hero playerScript;
    private int readyBottlesCount;
    private bool isEnd;

    void Start()
    {
        isEnd = false;
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        cameraController = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        activeBallRenderer = activeBall.GetComponent<SpriteRenderer>();
    }

    public void StartGame()
    {
        Start();

        gameObject.SetActive(true);
        playerScript.isCutScene = true;

        cameraController.ChangeAim(gameObject.transform);
        cameraController.ZoomIn(1);
    }

    private void EndGame()
    {
        isEnd = true;
        gameObject.SetActive(false);

        cameraController.ChangeAimToPlayer();
        playerScript.isCutScene = false;

        acid.SetActive(true);
    }

    public void SetActiveBall(Color color)
    {
        if (color == Color.black)
            return;
        activeBall.SetActive(true);
        activeBallRenderer.color = color;
    }

    public void ReadyBottle()
    {
        readyBottlesCount++;
    }

    void Update()
    {
        if (!isEnd && readyBottlesCount == 3)
            EndGame();
    }
}
