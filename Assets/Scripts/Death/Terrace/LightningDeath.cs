using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningDeath : MonoBehaviour
{
    [SerializeField] private GameObject pantaloons;
    [SerializeField] private GameObject rope;
    [SerializeField] private Clouds cloudsScript;

    private Hero playerScript;
    private Pantaloons pantaloonsScript;
    private CameraController cameraController;

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

        playerScript.EndCutScene();
    }

    
}
