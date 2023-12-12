using System.Collections;
using UnityEngine;

public class BathDeath : MonoBehaviour
{
    [SerializeField] private GameObject ghostSon;
    [SerializeField] private GameObject ghost;
    [SerializeField] private Trigger trigger;
    [SerializeField] private BathBomb bathBomb;
    [SerializeField] private ParticleSystem steam;
    [SerializeField] private ParticleSystem bubbles;
    [SerializeField] private GameObject blackOut;

    private Ghost ghostScript;
    private GhostSon ghostSonScript;
    private Hero playerScript;
    private CameraController cameraScript;
    private GameObject player;
    private Vector3 respawnPlace;

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
        ghostScript = ghost.GetComponent<Ghost>();
        ghostSonScript = ghostSon.GetComponent<GhostSon>();
        cameraScript = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
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

        steam.Play();
        bubbles.Stop();

        yield return new WaitForSeconds(1f);

        //по€вл€етс€ призрак и двигаетс€ до игрока
        ghostSonScript.GetOutAndMoveToPlayer();

        while (!ghostSonScript.isNearThePlayer)
            yield return new WaitForSeconds(0.1f);

        ghostSonScript.DrawnPlayer();

        while (!ghostSonScript.isEnd)
            yield return new WaitForSeconds(0.1f);

        bubbles.Play();
        

        yield return new WaitForSeconds(1.5f);
        playerScript.Death();
        steam.Stop();
        ghostSonScript.StopDrawn();
        player.transform.position = respawnPlace;
    }
}
