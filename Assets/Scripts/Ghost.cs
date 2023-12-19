using System;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] GameObject speechBox;
    [SerializeField] TextMeshPro textBox;
    public float speed = 2f;
    public bool isDialog, needToStartDialog;
    public int phraseIndex = 0;
    public bool canChangePhraseByButton;

    private Transform aim;
    private float aimXDelta, aimYDelta;
    private SpriteRenderer sprite;
    private string[] actualDialog;
    private static GameObject player;
    private bool needToHide, needToShow;

    void Start()
    {
        needToStartDialog = true;
        player = GameObject.FindWithTag("Player");
        sprite = gameObject.GetComponent<SpriteRenderer>();
        ChangeAim(gameObject.transform, 0, 0);
        canChangePhraseByButton = true;
        actualDialog = new string[14] { "�-�-�-�-�!", "��� �� �������� � ���� ����!", "����������� � ����� ��������!", 
            "��������... ��������� �����?", "����� ���� ��������, ������ �...", "��� �� ��� ����������...",
            "�� �� ������...", "� ����� ��?", "�, ���!", "���... ������", "�����, �� ���� ���� ������", 
            "������ �������� ����� �� ��������", "��� ����� ���� ����������� ������", "������� � � ������� ������ �"};
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
    }

    public void Show()
    {
        var pos = player.transform.position;
        pos.x += 8;
        pos.y += 4;
        pos.z = -1;
        transform.position = pos;
        needToShow = true;
        ChangeAimToPlayer();
    }

    private void Update()
    {
        var pos = aim.position;
        pos.x += aimXDelta;
        pos.y += aimYDelta;
        pos.z = -1;
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        sprite.flipX = transform.position.x < pos.x;

        if (needToStartDialog && !isDialog && CheckIsNearThePlayer())
            StartDialog();

        if (canChangePhraseByButton && isDialog && Input.GetKeyDown(KeyCode.F))
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
            {
                needToShow = false;
                ChangeAimToPlayer();
            }
        }
    }

    public void StartDialog()
    {
        isDialog = true;
        speechBox.SetActive(true);
        textBox.text = actualDialog[phraseIndex];
    }

    private void EndDialog()
    {
        isDialog = false;
        needToStartDialog = false;
        speechBox.SetActive(false);
        textBox.text = actualDialog[phraseIndex];
        Hide();
        actualDialog = null;
        InventoryLogic.canGetItems = true;
    }

    public bool CheckIsNearThePlayer()
    {
        return gameObject && Math.Abs(transform.position.x - (player.transform.position.x + 1.5f)) <= 0.1f && Math.Abs(transform.position.y - (player.transform.position.y + 0.4f)) <= 0.1f;
    }

    public void ChangeDialog(string[] newDialog)
    {
        actualDialog = newDialog;
        phraseIndex = 0;
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
