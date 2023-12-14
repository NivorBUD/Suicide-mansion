using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningDeath : MonoBehaviour
{
    [SerializeField] private GameObject pantaloons;
    [SerializeField] private GameObject rope;
    [SerializeField] private GameObject clouds;
    [SerializeField] private TriggerByName cloudsTrigger;

    private Hero playerScript;
    private Pantaloons pantaloonsScript;
    private Clouds cloudsScript;
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
        cloudsScript = clouds.GetComponent<Clouds>();
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




    }
}
