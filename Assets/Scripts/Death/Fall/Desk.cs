using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Renderer render;
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            // Включаем звук
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Делаем объект невидимым
            if (render != null)
            {
                render.enabled = false;
            }

            hasTriggered = true;
        }
    }
}