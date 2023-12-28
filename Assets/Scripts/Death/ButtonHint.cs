using UnityEngine;

public class ButtonHint : MonoBehaviour
{
    public GameObject hint;
    public bool isOn;

    private Trigger trigger;
    private Hero playerScript;

    void Start()
    {
        trigger = GetComponent<Trigger>();
        hint.SetActive(false);
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
    }

    void Update()
    {
        if (trigger.isTriggered && isOn && !playerScript.isCutScene)
            hint.SetActive(true);
        else
            hint.SetActive(false);
    }
}
