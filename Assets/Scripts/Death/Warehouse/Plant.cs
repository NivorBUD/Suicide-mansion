using System.Collections;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private Trigger trigger;
    [SerializeField] private GameObject acid;
    [SerializeField] private GameObject flamethrower;
    [SerializeField] private GameObject downLiana;
    [SerializeField] private GameObject upLiana;
    [SerializeField] private GameObject downLianaWay;
    [SerializeField] private GameObject upLianaWay;
    
    [SerializeField] private Wardrobe wardrobe; 
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject axe;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject blackOut;

    private ParticleSystem smoke;
    private Acid acidScript;
    private Flamethrower flamethrowerScript;
    private Hero playerScript;
    private GameObject player;
    private CameraController cameraController;
    private bool needToMoveAcid;
    private LianaHead downLianaScript;
    private LianaHead upLianaScript;

    [SerializeField] private Transform upPos;
    [SerializeField] private Transform cutscenePos;
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
        smoke = gameObject.GetComponent<ParticleSystem>();
        downLianaScript = downLiana.GetComponent<LianaHead>();
        upLianaScript = upLiana.GetComponent<LianaHead>();
    }

    void Update()
    {
        if (!playerScript.isCutScene && ReadyToStart())
            StartDeath();

        if (!playerScript.isCutScene && ReadyToKillThePlant() && Input.GetKeyUp(KeyCode.F))
            KillThePlant();

        if (needToMoveAcid)
        {
            var pos = upLianaScript.lianaHead.transform.position;
            pos.y -= 0.2f;
            acid.transform.position = pos;
        }
            
        if (acidAngle != 0)
            acid.transform.rotation = Quaternion.Slerp(acid.transform.rotation, Quaternion.Euler(acidAngle, 0, 0), 3f * Time.deltaTime);
    }

    IEnumerator CutScene1()
    {
        door.GetComponent<BoxCollider2D>().isTrigger = true;
        blackOut.SetActive(false);

        cameraController.ZoomIn(2);
        cameraController.ChangeAim(cutscenePos);
        acidScript.GetAndMoveToHand();
        yield return new WaitForSeconds(1f);

        playerScript.rb.simulated = false;
        playerScript.GetComponent<BoxCollider2D>().enabled = false;
        downLianaWay.SetActive(true);
        downLiana.SetActive(true);
        upLianaWay.SetActive(true);
        upLiana.SetActive(true);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        downLianaScript.StartMove();
        upLianaScript.StartMove();
        
        while (!upLianaScript.trigger.isTriggered) 
            yield return new WaitForSeconds(0.2f);
        

        upLianaScript.trigger.isTriggered = false;
        upLianaScript.trigger.enabled = false;
        yield return new WaitForEndOfFrame();

        needToMoveAcid = true;
        InventoryLogic.UseItem(playerScript.inventory["Acid"]);

        upLianaScript.MoveUp();
        upLianaScript.trigger.transform.position = upPos.position;
        yield return new WaitForSeconds(1f);

        upLianaScript.trigger.enabled = true;

        while (!upLianaScript.trigger.isTriggered)
            yield return new WaitForSeconds(0.02f);


        acidAngle = 180;
        while (acid.transform.rotation.eulerAngles.z <= 179)
            yield return new WaitForSeconds(0.02f);

        playerScript.Death();
        playerScript.rb.simulated = true;
        playerScript.GetComponent<BoxCollider2D>().enabled = true;

        downLianaScript.StartReverseMove();
        upLianaScript.StartReverseMove();

        acid.SetActive(false);
        wardrobe.ChangeSprite();
        wardrobe.DropFlammenwerfer();
        needToMoveAcid = false;

        yield return new WaitForSeconds(2.5f);
        downLianaWay.SetActive(false);
        upLianaWay.SetActive(false);

        yield return new WaitForSeconds(1f);
        blackOut.SetActive(true);
        Destroy(upLiana);
        Destroy(downLiana);
        door.GetComponent<BoxCollider2D>().isTrigger = false;
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
        yield return new WaitForSeconds(3f);

        smoke.Play();
        yield return new WaitForSeconds(3f);


        while (flamethrowerScript.needToRotate || flamethrowerScript.rotateNum != 4)
            yield return new WaitForSeconds(0.1f);
        
        yield return new WaitForSeconds(1f);

        body.SetActive(false);
        door.SetActive(false);
        flamethrower.SetActive(false);
        smoke.Stop();

        axe.SetActive(true);
        playerScript.EndCutScene();
        InventoryLogic.UseItem(playerScript.inventory["Flamethrower"]);
    }
}
