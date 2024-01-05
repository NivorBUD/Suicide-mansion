using UnityEngine;

public class FallDeath : MonoBehaviour
{
    public GameObject CaF2;
    public AudioClip triggerSound;
    private bool isTriggered = false;

    private GameObject player;
    private Hero playerScript;
    private Vector3 teleportPosition = new (59.39f, -2.7f, 0);
    private string[] dialog;
    private Ghost ghostScript;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
        ghostScript = GameObject.FindWithTag("Ghost").GetComponent<Ghost>();
        dialog = new string[]{"Деревянный пол в ванной?", "Это провал… Да какой огромный!", 
            "Ты видишь? Растения оплели дверь!", "Нужно от них избавиться",
            "Тут есть пара склянок с химикатами", "Смешай их в котле", "Ничего сложного, правда?",
            "Ты же встречал вид <I>лианиус резняриус</I>", "Если так не выйдет, то...", "Попробуй что-то другое"};
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && !isTriggered && playerScript.levelComplete == 4)
        {
            ghostScript.ChangeDialog(dialog);
            ghostScript.mission = "Смешать в котле химикаты и вылить на растения";
            isTriggered = true;
            player.SetActive(false);
            player.GetComponent<Hero>().StopPointerAiming();
            AudioSource.PlayClipAtPoint(triggerSound, transform.position);
            Invoke(nameof(ResetTrigger), 2f);
        }
    }

    private void ResetTrigger()
    {
        player.transform.position = teleportPosition;
        player.SetActive(true);
        playerScript.ChangePointerAim(CaF2.transform);
        playerScript.levelComplete = 5;
        ghostScript.ChangeDialog(dialog);
        ghostScript.mission = "Смешать в котле химикаты и вылить на растения";
        playerScript.SaveSave();
        ghostScript.Show();
    }
}