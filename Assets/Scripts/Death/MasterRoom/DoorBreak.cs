using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBreak : MonoBehaviour
{
    public GameObject door;
    public GameObject board;
    public GameObject object1;
    public GameObject object2;
    private Hero playerScript;
    private bool hasTriggered = false;
    private CameraController cameraController;
    private float originalCameraSize;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        cameraController = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        originalCameraSize = cameraController.GetComponent<Camera>().orthographicSize;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player"))
        {
            StartCoroutine(BreakDoor());
            hasTriggered = true;
        }
    }

    private IEnumerator BreakDoor()
    {
        // Блок управления перса
        playerScript.enabled = false;

        // Зум камеры
        cameraController.ZoomIn(originalCameraSize * 0.75f);

        // Поворот двери (чтобы она по физике падала)
        door.transform.rotation = Quaternion.Euler(0, 0, 100);

        // Отключение затемнений в комнате(во избежании багов)
        object1.SetActive(false);
        object2.SetActive(false);

        // Включение физики у двери
        Rigidbody2D doorRigidbody = door.GetComponent<Rigidbody2D>();
        doorRigidbody.constraints = RigidbodyConstraints2D.None;

        yield return new WaitForSeconds(1.5f); // Ожидание 1.5 сек

        // Вкл управление перса
        playerScript.enabled = true;
        // Возращение камеры в исходное состояние
        cameraController.ZoomIn(originalCameraSize);

        // Дверь пропадает, появляется доска
        door.SetActive(false);
        board.SetActive(true);

        yield return new WaitForSeconds(2f); // Ожидание две секунды чтобы не было затемнения в комнатах

        // Включаем логику затемнения
        object1.SetActive(true);
        object2.SetActive(true);

    }
}