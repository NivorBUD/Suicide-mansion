using System.Data;
using UnityEngine;

public class RottenFloor : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Trigger trigger;
    private bool hasTriggered = false; 
    private SpriteRenderer render;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        render = gameObject.GetComponent<SpriteRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (!hasTriggered && trigger.isPlayerInArea)
        {
            // Включаем звук
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Делаем объект невидимым
            render.enabled = false;
            boxCollider.enabled = false;

            hasTriggered = true;
        }
    }
}