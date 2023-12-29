using UnityEngine;

public class FallDeath : MonoBehaviour
{
    public GameObject character;
    public AudioClip triggerSound;
    private bool isTriggered = false;

    private Vector3 teleportPosition = new (59.39f, -2.7f, 0);
    private string[] dialog;
    private Ghost ghostScript;

    private void Start()
    {
        ghostScript = GameObject.FindWithTag("Ghost").GetComponent<Ghost>();
        dialog = new string[]{"���������� ��� � ������?", "��� ������ �� ����� ��������!", 
            "�� ������? �������� ������ �����!", "����� �� ��� ����������",
            "��� ���� ���� ������� � ����������", "������ �� � �����", "������ ��������, ������?",
            "�� �� �������� ��� <I>������� ���������</I>", "���� ��� �� ������, ��...", "�������� ���-�� ������"};
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == character && !isTriggered)
        {
            isTriggered = true;
            character.SetActive(false);
            AudioSource.PlayClipAtPoint(triggerSound, transform.position);
            Invoke(nameof(ResetTrigger), 2f);
        }
    }

    private void ResetTrigger()
    {
        character.transform.position = teleportPosition;
        character.SetActive(true);
        ghostScript.ChangeDialog(dialog);
        ghostScript.Show();
        ghostScript.mission = "������� � ����� �������� � ������ �� ��������";
    }
}