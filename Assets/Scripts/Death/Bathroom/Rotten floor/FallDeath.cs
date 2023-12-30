using UnityEngine;

public class FallDeath : MonoBehaviour
{
    public GameObject CaF2;
    public AudioClip triggerSound;
    private bool isTriggered = false;

    private GameObject player;
    private Vector3 teleportPosition = new (59.39f, -2.7f, 0);
    private string[] dialog;
    private Ghost ghostScript;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        ghostScript = GameObject.FindWithTag("Ghost").GetComponent<Ghost>();
        dialog = new string[]{"���������� ��� � ������?", "��� ������ �� ����� ��������!", 
            "�� ������? �������� ������ �����!", "����� �� ��� ����������",
            "��� ���� ���� ������� � ����������", "������ �� � �����", "������ ��������, ������?",
            "�� �� �������� ��� <I>������� ���������</I>", "���� ��� �� ������, ��...", "�������� ���-�� ������"};
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && !isTriggered)
        {
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
        player.GetComponent<Hero>().ChangePointerAim(CaF2.transform);
        ghostScript.ChangeDialog(dialog);
        ghostScript.Show();
        ghostScript.mission = "������� � ����� �������� � ������ �� ��������";
    }
}