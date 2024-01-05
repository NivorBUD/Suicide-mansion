using System;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] GameObject speechBox;
    [SerializeField] TextMeshPro textBox, dialogHint;
    public float speed = 3f;
    public bool isDialog, needToStartDialog, needTerraceDialog, dialogIsStarting;
    public int phraseIndex = 0;
    public bool canChangePhraseByButton;
    public string mission;
    public SpriteRenderer sprite;

    public Transform aim;
    private float aimXDelta, aimYDelta;
    private string[] actualDialog;
    private static GameObject player;
    private static Hero playerScript;
    private bool needToHide, needToShow;

    public void Start()
    {
        needTerraceDialog = true;
        needToStartDialog = true;
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        if (playerScript.levelComplete == 0)
            ChangeAim(gameObject.transform, 0, 0);
        canChangePhraseByButton = true;
        actualDialog = new string[] { "�-�-�-�-�!", "��� �� �������� � ���� ����!", "����������� � ����� ��������!",
            "��������... ��������� �����?", "����� ���� ��������, ������ �...", "��� �� ��� ����������...",
            "�� �� ������...", "� ����� ��?", "�, ���!", "���... ������", "���-���-���, ��� �� ��� ���� ",
            "���������, ����������, ����� � ���� ���!", "����������� ����������!", 
            "�� ��, ��� � ����������� ��������� ����������!", "����, ���������", "��, ��������, ������ ��������� ������?",
            "��� �, � ���� ������", "��� ������ �� ��������� ��� ��� �������", 
            "�� ������� �������� ������ � ����������", "� �� ������ �������� ��� � �������", "�� ��������?",
            "�� �������, � ����, ��� ��!", "����, ����� ���������� ������ �� ������ ����", 
            "����� ���� ����� �� ��������", "������� ������ � ������� ������ F", "������ ������� � ������ �� ��������",
            "��������� F ����� ��������� �������"};
    }

    public void ChangeAim(Transform newAim, float newXDelta, float newYDelta)
    {
        aimXDelta = newXDelta;
        aimYDelta = newYDelta;
        aim = newAim;
    }

    public void ChangeAimToPlayer()
    {
        aimXDelta = 1.5f;
        aimYDelta = 0.4f;
        aim = player.transform;
    }

    public void Hide()
    {
        aim = transform;
        needToHide = true;
        isDialog = false;
        needToShow = false;
        needToStartDialog = false;
    }

    public void Show()
    {
        needToStartDialog = true;
        var pos = player.transform.position;
        pos.x += 6;
        pos.y += 3;
        pos.z = -4.5f;
        transform.position = pos;
        needToShow = true;
        ChangeAimToPlayer();
        InventoryLogic.canGetItems = false;
    }

    private void Update()
    {
        if (playerScript.isPause)
            return;

        CheckDialogHint();

        var pos = aim.position;
        pos.x += aimXDelta;
        pos.y += aimYDelta;
        pos.z = -4.5f;
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        sprite.flipX = transform.position.x > aim.position.x;

        if (needToStartDialog && !isDialog && CheckIsNearTheAim())
            StartDialog();

        if (canChangePhraseByButton && dialogIsStarting && Input.GetKeyDown(KeyCode.F))
            ChangePhrase();

        if (needToHide) 
        {
            sprite.color = new Color(255, 255, 255, Mathf.MoveTowards(sprite.color.a, 0, Time.deltaTime));
            if (sprite.color.a == 0)
                needToHide = false;
        }

        if (needToShow)
        {
            sprite.color = new Color(255, 255, 255, Mathf.MoveTowards(sprite.color.a, 1, 0.5f * Time.deltaTime));
            if (sprite.color.a == 1)
                needToShow = false;
        }
    }

    private void CheckDialogHint()
    {
        dialogHint.enabled = canChangePhraseByButton && speechBox.activeSelf;
    }

    public void StartDialog()
    {
        isDialog = true;
        dialogIsStarting = true;
        needToStartDialog = false;
        speechBox.SetActive(true);
        textBox.text = actualDialog[phraseIndex];
    }

    private void EndDialog()
    {
        dialogIsStarting = false;
        isDialog = false;
        needToStartDialog = false;
        speechBox.SetActive(false);
        textBox.text = actualDialog[phraseIndex];
        Hide();
        actualDialog = null;
        InventoryLogic.canGetItems = true;
        playerScript.ChangeMission(mission);
    }

    public bool CheckIsNearTheAim()
    {
        return gameObject && Math.Abs(transform.position.x - (aim.position.x + aimXDelta)) <= 1.2f && 
            Math.Abs(transform.position.y - (aim.position.y + aimYDelta)) <= 1.2f && aim != transform;
    }

    public void ChangeDialog(string[] newDialog)
    {
        actualDialog = newDialog;
        phraseIndex = 0;
    }

    public string[] GetDialog()
    {
        return actualDialog;
    }

    public void ChangePhrase()
    {
        if (actualDialog == null)
            return;
        if (phraseIndex == actualDialog.Length - 1)
            EndDialog();
        else
        {
            phraseIndex++;
            textBox.text = actualDialog[phraseIndex];
        }
    }
}
