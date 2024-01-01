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
        dialog = new string[] {"��, ������ �� �� ������ ��������", "<I>����� ���</I>", 
            "�� ���� �������, ���������� �� �������"};
    }

    void Update()
    {
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

    public void StartDeath()
    {   
        AudioSource.PlayClipAtPoint(ElectroDeathSound, transform.position);
        blackOut.SetActive(false);
        PlayElectricShockSound(); // ���� ����� ����� �� 3 �������
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
        terraceDoor.Open();
        pantaloonsCollider.enabled = true;
        ropeCollider.enabled = true;
        yield return new WaitForSeconds(2);

        playerScript.ghostScript.ChangeDialog(dialog);
        playerScript.ghostScript.ChangeAimToPlayer();
        playerScript.ghostScript.Show();
        playerScript.ghostScript.mission = "����� �� �������";
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
