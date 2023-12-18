using UnityEngine;

public class FallDeath : MonoBehaviour
{
    public GameObject character;
    public AudioClip triggerSound;
    private bool isTriggered = false;

    private Vector3 teleportPosition = new Vector3(59.39f, -2.7f, 0.1f);

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
    }
}