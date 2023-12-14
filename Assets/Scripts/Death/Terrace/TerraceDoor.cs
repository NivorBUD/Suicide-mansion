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

    public void Open()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        isOpened = true;
        //GetComponent<SpriteRenderer>().sprite = openedDoor;
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Terrace and backyard")
            isOpened = true;
        trigger = GetComponent<Trigger>();

        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        mainCamera = GameObject.FindWithTag("MainCamera");
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
        }
    }
}
