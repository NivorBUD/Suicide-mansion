using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class PauseLogic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI changeModButtonText;
    [SerializeField] private GameObject missionWindow, dethopediaWindow, pauseMenu, mainRedCircle, onPauseRedCircle;
    [SerializeField] private Button navigationButton;
    [SerializeField] private Image navigationButtonImage;
    [SerializeField] GameObject menuCanvas, gameCanvas;

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
        if (!playerScript.isPause && pauseMenu.activeSelf)
            pauseMenu.SetActive(false);

        onPauseRedCircle.SetActive(mainRedCircle.activeSelf && isMission);

        navigationButton.gameObject.SetActive(!playerScript.isCutScene && playerScript.levelComplete >= 1);
        navigationButtonImage.color = playerScript.pointer.activeSelf ? new Color32(63, 208, 200, 128) : new Color32(63, 208, 200, 255);
        navigationButton.enabled = !playerScript.pointer.activeSelf;
    }

    public void ChangeMod()
    {
        if (isMission)
        {
            changeModButtonText.text = "Задания";
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
        menuCanvas.SetActive(true);
        gameCanvas.SetActive(false);
    }

    public void TurnOnNavigation()
    {
        YandexGame.RewVideoShow(1);
        
    }

    private void Reward(int id)
    {
        playerScript.pointer.SetActive(true);
    }

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Reward;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Reward;
    }
}
