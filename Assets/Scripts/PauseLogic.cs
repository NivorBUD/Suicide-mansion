using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseLogic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI changeModButtonText;
    [SerializeField] private GameObject missionWindow, dethopediaWindow, pauseMenu;

    private Hero playerScript;
    private bool isMission;

    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
    }

    void Update()
    {
        
    }

    public void ChangeMod()
    {
        if (isMission)
        {
            changeModButtonText.text = "Задание";
            missionWindow.SetActive(false);
            dethopediaWindow.SetActive(true);
        }
        else
        {
            changeModButtonText.text = "Достижения";
            missionWindow.SetActive(true);
            dethopediaWindow.SetActive(false);
        }

        isMission = !isMission;
    }

    public void Pause()
    {
        if (playerScript.isCutScene || !playerScript.canPause)
            return;

        playerScript.isPause = true;
        pauseMenu.SetActive(true);
    }

    public void Play()
    {
        playerScript.isPause = false;
        pauseMenu.SetActive(false);
    }
}
