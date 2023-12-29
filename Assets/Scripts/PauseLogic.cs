using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseLogic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI changeModButtonText;
    [SerializeField] private GameObject missionWindow, dethopediaWindow, pauseMenu, mainRedCircle, onPauseRedCircle;

    private Hero playerScript;
    private bool isMission;

    void Start()
    {
        isMission = true;
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
    }

    void Update()
    {
        onPauseRedCircle.SetActive(mainRedCircle.activeSelf && isMission);
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
            mainRedCircle.SetActive(false);
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
        if (!isMission) 
            mainRedCircle.SetActive(false);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
