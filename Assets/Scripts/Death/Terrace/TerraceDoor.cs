using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerraceDoor : MonoBehaviour
{
    public Transform anotherPlayerPosition;
    public Transform playerPosition;

    [SerializeField] private Sprite openedDoor;
    public bool isOpened;
    private Trigger trigger;
    private Hero playerScript;
    private GameObject mainCamera;
    private string[] dialog;

    public void Open()
    {
        PlayOpenSound(); // звук открытия двери
        isOpened = true;
        GetComponent<SpriteRenderer>().sprite = openedDoor;
    }

    private void PlayOpenSound()
    {

    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Terrace and backyard")
            isOpened = true;
        trigger = GetComponent<Trigger>();

        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        mainCamera = GameObject.FindWithTag("MainCamera");
        dialog = new string[] {"Осталось одно дело", "Найди веревку и...", "Какой-то флаг", 
            "Или что-то похожее", "В ванной точно есть что-то", "И повесь это на флагшток"};
    }

    void Update()
    {
        if (isOpened && trigger.isTriggered && Input.GetKeyDown(KeyCode.F))
        {
            playerPosition.position = playerScript.gameObject.transform.position;
            playerScript.gameObject.transform.position = anotherPlayerPosition.position;
            
            var camPos = playerScript.gameObject.transform.position;
            camPos.y = mainCamera.transform.position.y;
            mainCamera.transform.position = camPos;

            if (playerScript.ghostScript.needTerraceDialog)
            {
                playerScript.ghostScript.needTerraceDialog = false;
                playerScript.ghostScript.ChangeDialog(dialog);
                playerScript.ghostScript.Show();
            }
        }
    }
}
