using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject menuCanvas, gameCanvas;

    private Hero playerScript;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        playerScript.isPause = true;
        menuCanvas.SetActive(true);
        gameCanvas.SetActive(false);
    }

    public void StartGame()
    {
        YandexGame.FullscreenShow();
        menuCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        playerScript.isPause = false;
    }

    public void StartNewGame()
    {
        YandexGame.FullscreenShow();
        YandexGame.ResetSaveProgress();
        YandexGame.savesData = new SavesYG();
        InventoryLogic.canGetItems = true;
        YandexGame.SaveProgress();
        YandexGame.SaveLocal();

        SceneManager.UnloadScene("GameScene");
        SceneManager.LoadScene("GameScene");

        menuCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        playerScript.isPause = false;
    }
}

public class SettingsMenuController : MonoBehaviour
{
    public Dropdown resolutinonDropDown;

    public void GoToMainMenu()
    {   
        SceneManager.LoadScene("MainMenuScene"); 
    }
}