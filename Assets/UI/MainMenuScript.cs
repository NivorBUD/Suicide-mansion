using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); // Замените "GameScene" именем вашей игровой сцены
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

