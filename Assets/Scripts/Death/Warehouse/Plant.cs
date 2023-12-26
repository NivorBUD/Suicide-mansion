using System.Collections;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public BoxCollider2D axeCollider;

    [SerializeField] private Trigger trigger;
    [SerializeField] private GameObject acid, flamethrower, downLiana, upLiana, downLianaWay, upLianaWay;
    
    [SerializeField] private Wardrobe wardrobe; 
    [SerializeField] private GameObject door, axe, body, blackOut;

    private ParticleSystem smoke;
    private Acid acidScript;
    private Flamethrower flamethrowerScript;
    private Hero playerScript;
    private GameObject player;
    private CameraController cameraController;
    private bool needToMoveAcid;
    private LianaHead downLianaScript, upLianaScript;

    [SerializeField] private Transform upPos, cutscenePos;
    private float acidAngle = 0;
    private string[] dialog;

    private bool ReadyToStart()
    {
        return trigger.isTriggered && playerScript.inventory.ContainsKey("Acid");
    }

    private void StartDeath()
    {
        playerScript.isCutScene = true;
        acidAngle = 0;
        StartCoroutine(CutScene1());
    }
    
    private bool ReadyToKillThePlant()
    {
        return trigger.isTriggered && playerScript.inventory.ContainsKey("Flamethrower");
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
        dialog = new string[] { "Отличная работа!", "Откуда у меня огнемёт?", "Ну у меня немало скелетов в шкафу",
            "Вернее, в разных шкафах…", "Ого, это же бомбочка для ванной!", "Она тебе пригодится",
            "Теперь нужно перебраться через дыру наверху", "Сломай чем-то дверь в покои",
            "И положи доску поперёк пролома", "Да, я разрешаю", "Потом наполни ванну и...", 
            "Кинь туда бомбочку", "Давай, мне уже не терпится…"};
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

        while (!upLianaScript.isUp)
            yield return new WaitForSeconds(0.02f);


        acidAngle = 180;
        while (acid.transform.rotation.eulerAngles.z <= 176)
            yield return new WaitForSeconds(0.02f);

        playerScript.Acid();
        wardrobe.Acid();
        yield return new WaitForSeconds(1f);

        downLianaScript.StartReverseMove();
        upLianaScript.StartReverseMove();

        acid.SetActive(false);
        needToMoveAcid = false;
        yield return new WaitForSeconds(2f);

        wardrobe.Break();
        wardrobe.DropFlamethrower();
        downLianaWay.SetActive(false);
        upLianaWay.SetActive(false);

        playerScript.rb.simulated = true;
        playerScript.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(1f);
        
        Destroy(upLiana);
        Destroy(downLiana);
        door.GetComponent<BoxCollider2D>().isTrigger = false;
        yield return new WaitForSeconds(1f);

        blackOut.SetActive(true);
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

        playerScript.ghostScript.ChangeDialog(dialog);
        playerScript.ghostScript.Show();

        axe.SetActive(true);
        playerScript.EndCutScene();
        InventoryLogic.UseItem(playerScript.inventory["Flamethrower"]);
        axeCollider.enabled = true;
    }
}
