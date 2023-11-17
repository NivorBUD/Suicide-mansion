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

    //[SerializeField] GameObject Shovel;
    //[SerializeField] GameObject Keys;
    //[SerializeField] GameObject Slingshot;
    //[SerializeField] GameObject Candle;
    //[SerializeField] GameObject Marker;
    //[SerializeField] GameObject BathroomKey;
    //[SerializeField] GameObject Bottles;
    //[SerializeField] GameObject Acid;
    //[SerializeField] GameObject Flamethrower;
    //[SerializeField] GameObject Bomb;
    //[SerializeField] GameObject Axe;
    //[SerializeField] GameObject Board;
    //[SerializeField] GameObject Screwdriver;
    //[SerializeField] GameObject Bolts;
    //[SerializeField] GameObject Rope;
    //[SerializeField] GameObject Pantaloons;
    //[SerializeField] GameObject Petrol;
    //[SerializeField] GameObject Wheel;
    //[SerializeField] GameObject Food;
    //[SerializeField] GameObject Grass;
    //[SerializeField] GameObject TreasureKey;

    public static int InventoryItems = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetEmptySlots();
    }

    public static void TakeItem([SerializeField] GameObject InventoryObject)
    {
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
        InventoryObject.SetActive(false);
    }
}
