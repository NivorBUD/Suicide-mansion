using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public GameObject activeBall;
    public SpriteRenderer activeBallRenderer;
    public AudioClip potionReadySound;
    [SerializeField] GameObject acid;
    [SerializeField] ParticleSystem poof;
    [SerializeField] SpriteRenderer boilerSR;
    [SerializeField] Sprite acidBoilerSprite;

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

        cameraController.ChangeAimToPlayer();
        playerScript.isCutScene = false;
        boilerSR.sprite = acidBoilerSprite;
        acid.SetActive(true);
    }

    public void SetActiveBall(Sprite sprite)
    {
        if (sprite == null)
            return;
        activeBallRenderer.sprite = sprite;
    }

    public void ReadyBottle()
    {
        readyBottlesCount++;
    }

    void Update()
    {
        if (!isEnd && readyBottlesCount == 3)
        {
            gameObject.SetActive(false);
            poof.Play();
            AudioSource.PlayClipAtPoint(potionReadySound, transform.position);
            Invoke(nameof(EndGame), 1f);
        }
    }
}
