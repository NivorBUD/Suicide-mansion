using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHint : MonoBehaviour
{
    public GameObject hint;
    public bool isOn;

    private Trigger trigger;

    void Start()
    {
        trigger = GetComponent<Trigger>();
        hint.SetActive(false);
    }

    void Update()
    {
        if (trigger.isTriggered && isOn)
            hint.SetActive(true);
        else
            hint.SetActive(false);
    }
}
