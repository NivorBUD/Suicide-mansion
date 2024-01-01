using UnityEngine;

public class TerraceDoor : MonoBehaviour
{
    public Transform anotherPlayerPosition;
    public Transform playerPosition;
    public GameObject pantaloonsDialogTrigger, rope;
    public AudioClip TeleportDoorSound;

    [SerializeField] private Sprite openedDoor;
    public bool isOpened;
    private Trigger trigger;
    private Hero playerScript;
    private GameObject mainCamera;
    private string[] dialog;
    private ButtonHint hint;

    public void Open()
    {
        PlayOpenSound(); // ���� �������� �����
        isOpened = true;
        GetComponent<SpriteRenderer>().sprite = openedDoor;
        hint.isOn = true;
    }

    private void PlayOpenSound()
    {

    }

    void Start()
    {
        trigger = GetComponent<Trigger>();
        hint = GetComponent<ButtonHint>();

        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        mainCamera = GameObject.FindWithTag("MainCamera");
        dialog = new string[] {"� ���, ��� ����!", "����� ������������ ����� ������ ����!", "����� ������� �...", 
            "�����-�� ����", "��� ���-�� �������", "� ������ ����� ���� ���-��", "����������� � ���� ����"};
    }

    void Update()
    {
        if (isOpened && trigger.isTriggered && Input.GetKeyDown(KeyCode.F) && !playerScript.ghostScript.isDialog)
        {   
            AudioSource.PlayClipAtPoint(TeleportDoorSound, transform.position);
            playerPosition.position = playerScript.gameObject.transform.position;
            playerScript.gameObject.transform.position = anotherPlayerPosition.position;
            playerScript.isAtTerrace = !playerScript.isAtTerrace;

            var camPos = playerScript.gameObject.transform.position;
            camPos.y = mainCamera.transform.position.y;
            mainCamera.transform.position = camPos;

            if (playerScript.pointerAimTransform == transform)
                playerScript.StopPointerAiming();

            if (playerScript.ghostScript.needTerraceDialog)
            {
                playerScript.ghostScript.ChangeDialog(dialog);
                playerScript.ghostScript.needTerraceDialog = false;
                playerScript.ghostScript.Show();
                playerScript.ghostScript.mission = "����� ������ � ���� � ������� ��� �� �������";
                pantaloonsDialogTrigger.SetActive(true);
                playerScript.StopPointerAiming();
                playerScript.ChangePointerAim(rope.transform);
            }
        }
    }
}
