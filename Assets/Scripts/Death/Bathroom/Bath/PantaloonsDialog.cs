using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantaloonsDialog : MonoBehaviour
{
    private Ghost ghostScript;
    private string[] dialog;

    void Start()
    {
        ghostScript = GameObject.FindWithTag("Player").GetComponent<Hero>().ghostScript;
        dialog = new string[] {"Ой, не очень-то это похоже на флаг", "Хотя, мой муж любил эти панталоны…",
            "Ну, иного выхода у нас всё равно нет"};
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ghostScript.ChangeDialog(dialog);
            ghostScript.Show();
            ghostScript.StartDialog();
            gameObject.SetActive(false);
        }
    }
}
