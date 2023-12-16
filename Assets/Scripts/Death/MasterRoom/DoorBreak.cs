using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBreak : MonoBehaviour
{
    public GameObject board;
    private Hero playerScript;
    [SerializeField] private Trigger trigger;
    [SerializeField] private GameObject blackOut;
    [SerializeField] private Axe axe;
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
        if (collision.CompareTag("Player"))
            hasTriggered = true;
    } 
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            hasTriggered = false;
    }

    private void Update()
    {
        if (hasTriggered && playerScript.inventory.ContainsKey("Axe") && Input.GetKeyUp(KeyCode.F))
            StartCoroutine(BreakDoor());
    }

    private void PlayBreakSound()
    {

    }

    private void PlayHitSound()
    {

    }

    private IEnumerator BreakDoor()
    {
        // Блок управления перса
        playerScript.isCutScene = true;

        // Зум камеры
        cameraController.ChangeAim(playerScript.gameObject.transform);
        cameraController.ZoomIn(3);

        // Отключаем затемнение на время катсцены 
        blackOut.SetActive(false);

        // Топор пропадает из инвентаря
        InventoryLogic.UseItem(playerScript.inventory["Axe"]);

        // достаем топор
        axe.GetAndMoveToHand();
        while (!axe.isReady)
            yield return new WaitForSeconds(0.1f);

        //делаем 3 удара топором
        for (int i = 0; i < 3; i++)
        {
            axe.Hit();
            while (!axe.isReady)
                yield return new WaitForSeconds(0.1f);
            PlayHitSound(); //звук удара топора
        }
        
        // Включение физики у двери
        Rigidbody2D doorRigidbody = gameObject.GetComponent<Rigidbody2D>();
        doorRigidbody.bodyType = RigidbodyType2D.Dynamic;
        axe.gameObject.SetActive(false);

        PlayBreakSound(); //звук удара топором по двери

        // Добавление силы (чтобы она по физике падала)
        force.force = new Vector2(-0.5f, 0);
        yield return new WaitForSeconds(1f);

        // Дверь пропадает, появляется доска
        while (rb.angularVelocity >= 0.01)
            yield return new WaitForSeconds(0.1f);
        
        board.SetActive(true);
        GetComponent<BoxCollider2D>().isTrigger = true;
        rb.simulated = false;

        // Возращение камеры в исходное состояние
        cameraController.ChangeAimToPlayer();

        // Вкл управление перса
        playerScript.isCutScene = false;

        yield return new WaitForSeconds(1);

        // Включаем затемнение
        blackOut.SetActive(true);
    }
}