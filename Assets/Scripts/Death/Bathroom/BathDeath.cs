using System.Collections;
using UnityEngine;

public class BathDeath : MonoBehaviour
{
    [SerializeField] private Trigger trigger, scaleTrigger;
    [SerializeField] private BathBomb bathBomb;
    [SerializeField] private ParticleSystem steam, bubbles;
    [SerializeField] private GameObject ghostSon, blackOut, downPosAtticLadder, ghostPlace;
    [SerializeField] private Bath bath;

    private Ghost ghostScript;
    private GhostSon ghostSonScript;
    private Hero playerScript;
    private CameraController cameraScript;
    private GameObject player;
    private Vector3 respawnPlace;
    private string[] dialog;

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
        dialog = new string[11] {"Опять ты за своё?!", "Выходи, не прячься!", "Сколько можно?!", "Ничего не скажет и утопит!", 
            "Сказал бы что-то перед этим!", "Давай, лети отсюда!", "Ну а ты прости его", "Ему всего 113 лет", 
            "Лучше выйди на веранду", "Только включи рубильник на чердаке", "Надеюсь, там всё в порядке"};
    }

    void Update()
    {
        if (ReadyToDeath() && Input.GetKeyDown(KeyCode.F))
        {
            StartDeath();
        }
    }

    IEnumerator CutScene1()
    {
        blackOut.SetActive(false);
        respawnPlace = player.transform.position;

        // достаем бомбочку, начинаем катсцену, подстраиваем камеру
        playerScript.isCutScene = true;
        cameraScript.ChangeAim(bathBomb.transform);
        cameraScript.ZoomIn(2);
        InventoryLogic.UseItem(playerScript.inventory["Bath bomb"]);
        bathBomb.GetAndMoveToHand();
        while (!bathBomb.isReady) 
            yield return new WaitForSeconds(0.1f);
        
        // кидаем бомбочку в ванну, вода закипает 
        bathBomb.ThrowToBath();

        while (bathBomb)
            yield return new WaitForSeconds(0.1f);
        
        cameraScript.ChangeAim(ghostSon.transform);
        cameraScript.ZoomIn(2);

        bath.ChangeSprite();
        //steam.Play();
        bubbles.Play();

        yield return new WaitForSeconds(1f);

        //появляется призрак и двигается до игрока
        ghostSonScript.GetOutAndMoveToPlayer();

        while (!ghostSonScript.isNearThePlayer)
            yield return new WaitForSeconds(0.1f);

        ghostSonScript.DrawnPlayer();

        while (!scaleTrigger.isTriggered)
            yield return new WaitForSeconds(0.1f);

        player.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        while (!ghostSonScript.isEnd)
            yield return new WaitForSeconds(0.1f);

        //bubbles.Play();

        yield return new WaitForSeconds(1.5f);
        playerScript.Death();
        steam.Stop();
        ghostSonScript.StopDrawn();
        player.transform.position = respawnPlace;

        ghostScript.ChangeDialog(dialog);
        ghostScript.Show();
        ghostScript.ChangeAim(ghostPlace.transform, 2, 0);

        while (ghostScript.phraseIndex != 1)
            yield return new WaitForSeconds(0.1f);

        ghostScript.speed = 2;
        ghostSonScript.GoToMom();

        while (ghostScript.phraseIndex != 5)
            yield return new WaitForSeconds(0.1f);

        ghostSonScript.Hide();
        downPosAtticLadder.GetComponent<BoxCollider2D>().enabled = true;
        blackOut.SetActive(true);
    }
}
