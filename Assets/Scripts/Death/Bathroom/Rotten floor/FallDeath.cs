using UnityEngine;

public class FallDeath : MonoBehaviour
{
    public GameObject CaF2;
    public AudioClip triggerSound;
    private bool isTriggered = false;

    private GameObject player;
    private Hero playerScript;
    private Vector3 teleportPosition = new (59.39f, -2.7f, 0);
    private string[] dialog;
    private Ghost ghostScript;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
        ghostScript = GameObject.FindWithTag("Ghost").GetComponent<Ghost>();
        dialog = new string[]{"���������� ��� � ������?", "��� ������ �� ����� ��������!", 
            "�� ������? �������� ������ �����!", "����� �� ��� ����������",
            "��� ���� ���� ������� � ����������", "������ �� � �����", "������ ��������, ������?",
            "�� �� �������� ��� <I>������� ���������</I>", "���� ��� �� ������, ��...", "�������� ���-�� ������"};
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && !isTriggered && playerScript.levelComplete == 4)
        {
            ghostScript.ChangeDialog(dialog);
            ghostScript.mission = "������� � ����� �������� � ������ �� ��������";
            isTriggered = true;
            player.SetActive(false);
            player.GetComponent<Hero>().StopPointerAiming();
            AudioSource.PlayClipAtPoint(triggerSound, transform.position);
            Invoke(nameof(ResetTrigger), 2f);
        }
    }

    private void ResetTrigger()
    {
        player.transform.position = teleportPosition;
        player.SetActive(true);
        playerScript.ChangePointerAim(CaF2.transform);
        playerScript.levelComplete = 5;
        ghostScript.ChangeDialog(dialog);
        ghostScript.mission = "������� � ����� �������� � ������ �� ��������";
        playerScript.SaveSave();
        ghostScript.Show();
    }
}