using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        YandexGame.FullscreenShow();
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
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