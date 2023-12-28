using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] GameObject tpPlace;

    private Hero playerScript;
    public GameObject ghost;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerScript.gameObject.transform.position = tpPlace.transform.position;
        playerScript.ChangeMission("Выслушать привидение");
        Invoke(nameof(ShowGhost), 2);
    }

    private void ShowGhost()
    {
        ghost.GetComponent<Ghost>().Show();
    }
}
