using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseLogic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI changeModButtonText;
    [SerializeField] private GameObject missionWindow, dethopediaWindow, pauseMenu, mainRedCircle, onPauseRedCircle;
    [SerializeField] private Button navigationButton;
    [SerializeField] private Image navigationButtonImage;

    private Hero playerScript;
    private bool isMission;
    private ChangeImage[] changeImages;

    void Start()
    {
        isMission = true;
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        changeImages = pauseMenu.gameObject.GetComponentsInChildren<ChangeImage>();
    }

    void Update()
    {
        onPauseRedCircle.SetActive(mainRedCircle.activeSelf && isMission);
        navigationButtonImage.color = playerScript.pointer.activeSelf ? new Color32(63, 208, 200, 128) : new Color32(63, 208, 200, 255);
        navigationButton.enabled = !playerScript.pointer.activeSelf;
    }

    public void ChangeMod()
    {
        if (isMission)
        {
            changeModButtonText.text = "Задание";
            missionWindow.SetActive(false);
        }
        else
        {
            changeModButtonText.text = "Достижения";
            missionWindow.SetActive(true);
            mainRedCircle.SetActive(false);

            foreach (var death in changeImages)
                death.isNew = false;
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
        {
            mainRedCircle.SetActive(false);
            foreach (var death in changeImages)
                death.isNew = false;
        }
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void TurnOnNavigation()
    {
        playerScript.pointer.SetActive(true);
    }
}
