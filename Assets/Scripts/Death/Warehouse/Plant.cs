using System.Collections;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private PlantTrigger trigger;
    [SerializeField] private GameObject acid;
    [SerializeField] private GameObject flamethrower;
    [SerializeField] private LineRenderer liana1;
    [SerializeField] private LineRenderer liana2;
    [SerializeField] private Wardrobe wardrobe;
    [SerializeField] private GameObject smoke;
    [SerializeField] private GameObject door;
    
    private Acid acidScript;
    private Flamethrower flamethrowerScript;
    private Hero playerScript;
    private GameObject player;
    private CameraController cameraController;
    private bool needToMoveLianas;
    private bool needToMoveAcid;

    [SerializeField] private Transform upPos;
    [SerializeField] private Transform cutscenePos;
    private Vector3 pos1;
    private Vector3 pos2;
    private Vector3 endPos1;
    private Vector3 endPos2;
    private float acidAngle = 0;

    private bool ReadyToStart()
    {
        return trigger.isPlayerInArea && playerScript.inventory.ContainsKey("Acid");
    }

    private void StartDeath()
    {
        playerScript.isCutScene = true;
        acidAngle = 0;
        StartCoroutine(CutScene1());
    }
    
    private bool ReadyToKillThePlant()
    {
        return trigger.isPlayerInArea && playerScript.inventory.ContainsKey("Flamethrower");
    }

    private void KillThePlant()
    {
        playerScript.isCutScene = true;
        StartCoroutine(CutScene2());
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
        cameraController = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        acidScript = acid.GetComponent<Acid>();
        flamethrowerScript = flamethrower.GetComponent<Flamethrower>();
    }

    void Update()
    {
        if (!playerScript.isCutScene && ReadyToStart())
            StartDeath();

        if (!playerScript.isCutScene && ReadyToKillThePlant() && Input.GetKeyUp(KeyCode.F))
            KillThePlant();

        if (needToMoveAcid)
            acid.transform.position = Vector3.MoveTowards(acid.transform.position, endPos1, 1f * Time.deltaTime);

        if (needToMoveLianas)
        {
            pos1 = Vector3.MoveTowards(pos1, endPos1, 1f * Time.deltaTime);
            liana1.SetPosition(1, pos1);

            pos2 = Vector3.MoveTowards(pos2, endPos2, 1.2f * Time.deltaTime);
            liana2.SetPosition(1, pos2);
        }

        if (acidAngle != 0)
            acid.transform.rotation = Quaternion.Slerp(acid.transform.rotation, Quaternion.Euler(acidAngle, 0, 0), 3f * Time.deltaTime);
    }

    IEnumerator CutScene1()
    {
        cameraController.ZoomIn(2);
        cameraController.ChangeAim(cutscenePos);
        acidScript.GetAndMoveToHand();
        yield return new WaitForSeconds(1f);

        liana1.SetPosition(0, liana1.transform.position);
        liana2.SetPosition(0, liana2.transform.position);
        pos1 = liana1.transform.position;
        pos2 = liana2.transform.position;
        endPos1 = acid.transform.position;
        endPos2 = player.transform.position;
        endPos2.y += 0.5f;
        endPos2.x += 0.2f;
        needToMoveLianas = true;

        while (pos1 != acid.transform.position)
            yield return new WaitForSeconds(0.1f);

        needToMoveAcid = true;
        InventoryLogic.UseItem(playerScript.inventory["Acid"]);
        endPos1 = upPos.position;
        endPos1.x = player.transform.position.x;
        while (acid.transform.position != endPos1)
            yield return new WaitForSeconds(0.1f);

        acidAngle = 180;
        while (acid.transform.rotation.eulerAngles.z <= 179)
            yield return new WaitForSeconds(0.1f);

        playerScript.Death();
        playerScript.EndCutScene();

        endPos1 = liana1.transform.position;
        endPos2= liana2.transform.position;
        while (pos1 != endPos1)
            yield return new WaitForSeconds(0.1f);
        acid.SetActive(false);
        wardrobe.ChangeSprite();
        wardrobe.DropFlammenwerfer();
        needToMoveAcid = false;
        needToMoveLianas = false;
    }

    IEnumerator CutScene2()
    {
        cameraController.ZoomIn(2);
        cameraController.ChangeAim(cutscenePos);
        flamethrowerScript.GetAndMoveToHand();
        yield return new WaitForSeconds(1f);

        while (!flamethrowerScript.isReady)
            yield return new WaitForSeconds(0.1f);

        flamethrowerScript.Fire();
        yield return new WaitForSeconds(1f);

        smoke.SetActive(true);
        yield return new WaitForSeconds(3f);

        gameObject.SetActive(false);
        door.SetActive(false);
        flamethrower.SetActive(false);
        playerScript.EndCutScene();
        InventoryLogic.UseItem(playerScript.inventory["Flamethrower"]);
    }
}
