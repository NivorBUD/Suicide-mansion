using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButtonHint : MonoBehaviour
{
    public GameObject hint;

    private ItemInteraction trigger;

    void Start()
    {
        trigger = GetComponent<ItemInteraction>();
    }

    void Update()
    {
        if (trigger.IsHeroInArea && InventoryLogic.canGetItems)
            hint.SetActive(true);
        else
            hint.SetActive(false);
    }
}
