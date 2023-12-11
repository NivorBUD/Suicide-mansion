using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Item_Interaction : MonoBehaviour
{
    [SerializeField] GameObject InventoryObject;
    private bool IsHeroInArea = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            IsHeroInArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            IsHeroInArea = false;
    }


    void TryToTake()
    {
        if (InventoryLogic.InventoryItems < 3)
        {
            InventoryLogic.TakeItem(InventoryObject);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (IsHeroInArea && Input.GetKeyDown(KeyCode.E))
            TryToTake();
    }
}
