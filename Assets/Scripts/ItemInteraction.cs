using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] GameObject InventoryObject;
    public bool IsHeroInArea = false;

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
        if (InventoryLogic.canGetItems && InventoryLogic.InventoryItems < 2)
        {
            PlayGetSound(); // звук подбора предмета
            InventoryLogic.TakeItem(InventoryObject);
            Destroy(gameObject);
        }
    }

    private void PlayGetSound()
    {

    }

    void Update()
    {
        if (IsHeroInArea && Input.GetKeyDown(KeyCode.E))
            TryToTake();
    }
}
