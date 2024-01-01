using UnityEngine;

public class InventoryLogic : MonoBehaviour
{
    [SerializeField] GameObject EmptySlot1;
    [SerializeField] GameObject EmptySlot2;

    public static int InventoryItems = 0;
    public static bool canGetItems = true;

    void Update()
    {
        SetEmptySlots();
    }

    public static void TakeItem([SerializeField] GameObject InventoryObject)
    {
        GameObject.FindWithTag("Player").GetComponent<Hero>().AddToInventiry(InventoryObject);
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
                break;  
            case 1:
                EmptySlot1.SetActive(true);
                EmptySlot2.SetActive(false);
                break;
            case 2:
                EmptySlot1.SetActive(false);
                EmptySlot2.SetActive(false);
                break;
        }
    }

    public static void UseItem([SerializeField] GameObject InventoryObject)
    {
        GameObject.FindWithTag("Player").GetComponent<Hero>().DelFromInventiry(InventoryObject);
        InventoryObject.SetActive(false);
        InventoryItems--;
    }
}
