using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private PlantTrigger trigger;
    [SerializeField] private GameObject acid;
    [SerializeField] private LineRenderer liana;
    private Acid acidScript;
    private Hero playerScript;
    private CameraController cameraController;
    private bool needToGetAcid;
    private Vector3 pos;

    private void StartDeath()
    {
        playerScript.isCutScene = true;
        playerScript.rb.simulated = false;
        
        StartCoroutine(CutScene());
    }

    private bool ReadyToStart()
    {
        return trigger.isPlayerInArea && playerScript.inventory.ContainsKey("Acid");
    }

    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        cameraController = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        acidScript = acid.GetComponent<Acid>();
        
    }

    void Update()
    {
        if (ReadyToStart() && Input.GetKeyUp(KeyCode.F))
            StartDeath();
        if (needToGetAcid)
        {
            pos = Vector3.MoveTowards(pos, acid.transform.position, 1f * Time.deltaTime);
            liana.SetPosition(1, pos);
        }
    }

    IEnumerator CutScene()
    {
        cameraController.ZoomIn(1);
        cameraController.ChangeAim(acid.transform);
        acidScript.GetAndMoveToHand();
        yield return new WaitForSeconds(1f);

        pos = liana.transform.position;
        liana.SetPosition(0, liana.transform.position);
        needToGetAcid = true;
        yield return new WaitForSeconds(0.5f);

    }
}
