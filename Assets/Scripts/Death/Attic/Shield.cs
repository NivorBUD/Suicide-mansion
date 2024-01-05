using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Trigger trigger;
    public ElectricMinigameLogic gameLogic;
    public TerraceDoor terraceDoor;
    public BoxCollider2D ropeCollider, pantaloonsCollider;
    public Sprite playerElectric, skeletonElectric;
    public GameObject blackOut;
    public AudioClip ElectroDeathSound;
    [SerializeField] private ChangeImage deathopediaImage;

    private CameraController mainCamera;
    private GameObject player;
    private Hero playerScript;
    private bool isUsed;
    private string[] dialog;

    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
        dialog = new string[] {"Да, похоже ты не лучший электрик…", "<I>Среди нас</I>",
            "Но дело сделано, встретимся на веранде"};
    }

    void Update()
    {
        if (playerScript.levelComplete >= 8 && !isUsed)
        {
            if (playerScript.levelComplete == 8)
            {
                playerScript.ghostScript.mission = "Выйти на веранду";
                playerScript.ghostScript.ChangeDialog(dialog);
                playerScript.ghostScript.Show();
                pantaloonsCollider.enabled = true;
                ropeCollider.enabled = true;
                StartCoroutine(OpenDoor());
            }
            else
            {
                terraceDoor.Open();
            }
            isUsed = true;
            trigger.gameObject.SetActive(false);
            deathopediaImage.ChangeSprite();
        }

        if (!isUsed && trigger.isTriggered && Input.GetKeyDown(KeyCode.F))
        {
            isUsed = true;
            trigger.gameObject.SetActive(false);
            playerScript.isCutScene = true;
            playerScript.StopPointerAiming();
            gameLogic.StartGame();
            mainCamera.ZoomIn(1);
            mainCamera.ChangeAim(gameLogic.gameObject.transform);
        }
    }

    IEnumerator OpenDoor()
    {
        while (!playerScript.ghostScript.isDialog)
            yield return null;

        while (playerScript.ghostScript.isDialog)
            yield return null;

        terraceDoor.Open();
    }

    public void StartDeath()
    {   
        AudioSource.PlayClipAtPoint(ElectroDeathSound, transform.position);
        blackOut.SetActive(false);
        PlayElectricShockSound();
        mainCamera.ZoomIn(2);
        mainCamera.ChangeAim(player.transform);

        StartCoroutine(DeathCutScene());
    }

    IEnumerator DeathCutScene()
    {
        playerScript.EletricSchock();
        yield return new WaitForSeconds(3);

        deathopediaImage.ChangeSprite();
        playerScript.Death();
        playerScript.levelComplete = 8;
        playerScript.ghostScript.mission = "Выйти на веранду";
        playerScript.ghostScript.ChangeDialog(dialog);
        terraceDoor.Open();
        pantaloonsCollider.enabled = true;
        ropeCollider.enabled = true;
        playerScript.ChangePointerAim(terraceDoor.gameObject.transform);
        playerScript.SaveSave();
        yield return new WaitForSeconds(2);

        playerScript.ghostScript.Show();
        yield return new WaitForSeconds(2);

        blackOut.SetActive(true);

        while (playerScript.ghostScript.isDialog)
            yield return new WaitForSeconds(0.1f);

        playerScript.ChangePointerAim(terraceDoor.gameObject.transform);
    }

    private void PlayElectricShockSound()
    {

    }
}
