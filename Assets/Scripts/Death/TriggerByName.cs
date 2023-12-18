using UnityEngine;

public class TriggerByName : MonoBehaviour
{
    public string interactionName;
    public bool isTriggered;
    public bool isEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isEnter = true;
        if (collision.gameObject.name == interactionName)
            isTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isEnter = false;
        if (collision.gameObject.name == interactionName)
            isTriggered = false;
    }
}
