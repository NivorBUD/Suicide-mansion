using System.Collections;
using UnityEngine;

public class BathDeath : MonoBehaviour
{
    [SerializeField] private Trigger trigger, scaleTrigger;
    [SerializeField] private BathBomb bathBomb;
    [SerializeField] private ParticleSystem bubbles;
    [SerializeField] private GameObject ghostSon, blackOut, downPosAtticLadder, ghostPlace, electricShield;
    [SerializeField] private Bath bath;
    [SerializeField] private ChangeImage deathopediaImage;

    private Ghost ghostScript;
    private GhostSon ghostSonScript;
    private Hero playerScript;
    private CameraController cameraScript;
    private GameObject player;
    private Vector3 respawnPlace;
    private string[] dialog;
    private ButtonHint hint;

    public bool ReadyToDeath()
    {
        return playerScript.inventory.ContainsKey("Bath bomb") && trigger.isTriggered;
    }

    public void StartDeath()
    {
        StartCoroutine(CutScene1());
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
        ghostScript = playerScript.ghostScript;
        ghostSonScript = ghostSon.GetComponent<GhostSon>();
        cameraScript = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        dialog = new string[] {"Опять ты за своё?!", "Выходи, не прячься!", "Сколько можно?!", 
            "Тебе уже как никак 163 года!", "Прости его, любит он поиграть…", "<I>До смерти</I>", "Выйди на веранду, отдышись",
            "Только включи рубильник на чердаке", "Иначе на веранду ты не попадёшь"};
        hint = trigger.gameObject.GetComponent<ButtonHint>();
    }

    void Update()
    {
        if (ReadyToDeath() && Input.GetKeyDown(KeyCode.F))
        {
            StartDeath();
        }

        hint.isOn = playerScript.inventory.ContainsKey("Bath bomb");
    }

    IEnumerator CutScene1()
    {
        blackOut.SetActive(false);
        respawnPlace = player.transform.position;
        playerScript.isCutScene = true;
        cameraScript.ChangeAim(bathBomb.transform);
        cameraScript.ZoomIn(2);
        InventoryLogic.UseItem(playerScript.inventory["Bath bomb"]);
        playerScript.StopPointerAiming();
        bathBomb.GetAndMoveToHand();
        while (!bathBomb.isReady) 
            yield return new WaitForSeconds(0.1f);
        
        bathBomb.ThrowToBath();
        while (bathBomb)
            yield return new WaitForSeconds(0.1f);
        
        cameraScript.ChangeAim(ghostSon.transform);
        cameraScript.ZoomIn(2);

        bath.ChangeSprite();
        bubbles.Play();

        yield return new WaitForSeconds(1f);

        ghostSonScript.GetOutAndMoveToPlayer();
        while (!ghostSonScript.isNearThePlayer)
            yield return new WaitForSeconds(0.1f);

        ghostSonScript.DrawnPlayer();
        while (!scaleTrigger.isTriggered)
            yield return null;

        player.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        while (!ghostSonScript.isEnd)
            yield return null;

        yield return new WaitForSeconds(1.5f);

        deathopediaImage.ChangeSprite();
        playerScript.Death();
        ghostSonScript.StopDrawn();
        player.transform.position = respawnPlace;
        ghostScript = playerScript.ghostScript;
        dialog = new string[] {"Опять ты за своё?!", "Выходи, не прячься!", "Сколько можно?!",
            "Тебе уже как никак 163 года!", "Прости его, любит он поиграть…", "<I>До смерти</I>", "Выйди на веранду, отдышись",
            "Только включи рубильник на чердаке", "Иначе на веранду ты не попадёшь"};
        ghostScript.ChangeDialog(dialog);
        ghostScript.Show();
        bubbles.Stop();
        ghostScript.mission = "Попасть на чердак";
        ghostScript.ChangeAim(ghostPlace.transform, 2, 0);
        while (ghostScript.phraseIndex < 1)
            yield return null;

        ghostScript.speed = 2;
        ghostSonScript.GoToMom();
        while (ghostScript.phraseIndex != 5)
            yield return new WaitForSeconds(0.1f);

        ghostSonScript.Hide();
        downPosAtticLadder.GetComponent<BoxCollider2D>().enabled = true;
        blackOut.SetActive(true);
        while (ghostScript.isDialog)
            yield return new WaitForSeconds(0.1f);

        playerScript.ChangePointerAim(electricShield.transform);
    }
}
