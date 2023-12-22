using UnityEngine;

public class FallDeath : MonoBehaviour
{
    public GameObject character;
    public AudioClip triggerSound;
    private bool isTriggered = false;

    private Vector3 teleportPosition = new (59.39f, -2.7f, 0);
    private string[] dialog;
    private Ghost ghost;

    private void Start()
    {
        ghost = GameObject.FindWithTag("Ghost").GetComponent<Ghost>();
        dialog = new string[12]{"Деревянный пол?", "Ошибка!", "Деревянный пол в ванной?", "Фатальная ошибка!", 
            "Еще и семечки из шкафа проросли", "Надо бы избавиться от этого", "Думаю, кислота поможет", 
            "Тут есть пара колб с химикатами", "Смешай их в котле", "И попробуй избавиться от растения", 
            "Если так не выйдет, то...", "Попробуй что-то другое"};
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == character && !isTriggered)
        {
            isTriggered = true;
            character.SetActive(false);
            AudioSource.PlayClipAtPoint(triggerSound, transform.position);
            Invoke(nameof(ResetTrigger), 2f);
        }
    }

    private void ResetTrigger()
    {
        character.transform.position = teleportPosition;
        character.SetActive(true);
        ghost.ChangeDialog(dialog);
        ghost.Show();
    }
}