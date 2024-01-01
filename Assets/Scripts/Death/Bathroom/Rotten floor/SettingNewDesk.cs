using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingNewDesk : MonoBehaviour
{
    public Trigger trigger;
    public GameObject desk, bath, bathBomb;
    public Desk deskScript;
    public AudioClip putBoard;

    private Hero playerScript;

    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
    }

    void Update()
    {
        if (trigger.isTriggered && playerScript.inventory.ContainsKey("Board") && Input.GetKeyUp(KeyCode.F))
            StartCoroutine(CutScene());
    }

    IEnumerator CutScene()
    {
        playerScript.isCutScene = true;
        AudioSource.PlayClipAtPoint(putBoard, transform.position);
        InventoryLogic.UseItem(playerScript.inventory["Board"]);
        playerScript.StopPointerAiming();
        deskScript.GetAndMoveToHand();
        while (!deskScript.isReady)
            yield return new WaitForSeconds(0.1f);

        deskScript.SetAndDrop();
        while (!deskScript.isInstall)
            yield return new WaitForSeconds(0.1f);

        deskScript.rb.bodyType = RigidbodyType2D.Static;

        playerScript.ChangeMission("����������� �����, ����� ���� � ����� ��������");
        playerScript.isCutScene = false;

        if (!playerScript.inventory.ContainsKey("Bath bomb"))
            playerScript.ChangePointerAim(bathBomb.transform);

        while (!playerScript.inventory.ContainsKey("Bath bomb"))
            yield return new WaitForSeconds(0.1f);

        playerScript.ChangePointerAim(bath.transform);
    }
}
