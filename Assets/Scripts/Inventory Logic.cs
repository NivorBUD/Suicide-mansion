using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryLogic : MonoBehaviour
{
    [SerializeField] GameObject EmptySlot1;
    [SerializeField] GameObject EmptySlot2;
    [SerializeField] GameObject EmptySlot3;

    public static int InventoryItems = 0;

    // Update is called once per frame
    void Update()
    {
        SetEmptySlots();
    }

    public static void TakeItem([SerializeField] GameObject InventoryObject)
    {
        GameObject.FindWithTag("Player").GetComponent<Hero>().addToInventiry(InventoryObject);
        InventoryObject.SetActive(true);
        InventoryItems++;
    }

    private void SetEmptySlots()
    {
        switch (InventoryItems)
        {
            case 0:
                EmptySlot1.SetActive(true);
                EmptySlot2.SetActive(true);
                EmptySlot3.SetActive(true);
                break;  
            case 1:
                EmptySlot1.SetActive(true);
                EmptySlot2.SetActive(true);
                EmptySlot3.SetActive(false);
                break;
            case 2:
                EmptySlot1.SetActive(true);
                EmptySlot2.SetActive(false);
                EmptySlot3.SetActive(false);
                break;
            case 3:
                EmptySlot1.SetActive(false);
                EmptySlot2.SetActive(false);
                EmptySlot3.SetActive(false);
                break;
        }
    }

    public static void UseItem([SerializeField] GameObject InventoryObject)
    {
        GameObject.FindWithTag("Player").GetComponent<Hero>().delFromInventiry(InventoryObject);
        InventoryObject.SetActive(false);
        InventoryItems--;
    }
}
