using UnityEngine;

public class TriggerActivation : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;
    private bool hasActivated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasActivated && collision.CompareTag("Player"))
        {
            objectToActivate.SetActive(true);
            hasActivated = true;
        }
    }
}