using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBreak : MonoBehaviour
{
    public GameObject board;
    private Hero playerScript;
    [SerializeField] private Trigger trigger;
    [SerializeField] private GameObject blackOut;
    private bool hasTriggered;
    private ConstantForce2D force;
    private Rigidbody2D rb;
    private CameraController cameraController;
    private float originalCameraSize;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        cameraController = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        originalCameraSize = cameraController.GetComponent<Camera>().orthographicSize;
        force = gameObject.GetComponent<ConstantForce2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player") && playerScript.inventory.ContainsKey("Axe"))
        {
            StartCoroutine(BreakDoor());
            hasTriggered = true;
        }
    }

    private IEnumerator BreakDoor()
    {
        // Блок управления перса
        playerScript.isCutScene = true;

        // Зум камеры
        cameraController.ZoomIn(originalCameraSize * 0.75f);

        // Отключаем затемнение на время катсцены 
        blackOut.SetActive(false);

        // Включение физики у двери
        Rigidbody2D doorRigidbody = gameObject.GetComponent<Rigidbody2D>();
        doorRigidbody.bodyType = RigidbodyType2D.Dynamic;

        // Топор пропадает из инвентаря
        InventoryLogic.UseItem(playerScript.inventory["Axe"]);

        // Добавление силы (чтобы она по физике падала)
        force.force = new Vector2(-0.5f, 0);
        yield return new WaitForSeconds(1f);

        // Дверь пропадает, появляется доска
        while (rb.angularVelocity >= 0.01)
            yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        board.SetActive(true);

        // Возращение камеры в исходное состояние
        cameraController.ChangeAimToPlayer();

        // Вкл управление перса
        playerScript.isCutScene = false;

        //yield return new WaitForSeconds(1.5f);
        // Включаем затемнение
        blackOut.SetActive(true);
    }
}