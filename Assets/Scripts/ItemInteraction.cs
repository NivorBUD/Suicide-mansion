using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] GameObject inventoryObject, nextActionPlace, anotherObject;
    [SerializeField] InfoDesk infoDesk;
    [SerializeField] AudioSource pickUpSound; 
    public bool IsHeroInArea = false;

    private Hero playerScript;
    private Dictionary<string, string> names = new();

    private void Start()
    {
        names["Shovel"] = "лопата";
        names["Screwdriver"] = "отвёртка";
        names["Marker"] = "маркер";
        names["Slingshot"] = "рогатка";
        names["Bathroom Key"] = "ключ от ванной";
        names["Pantaloons"] = "панталоны";
        names["Rope"] = "верёвка";
        names["Bath bomb"] = "бомбочка для ванны";
        names["H2SO4"] = "химикат (H2SO4)";
        names["CaF2"] = "химикат (CaF2)";
        names["Acid"] = "кислота";
        names["Flamethrower"] = "огнемёт";
        names["Screws"] = "болты";
        names["Key"] = "клавиша рояля";
        names["Candle"] = "свечка";
        names["Board"] = "доска";
        names["Axe"] = "топор";
        names["Treasure key"] = "ключ от сокровищ";
    }

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
            playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
            InventoryLogic.TakeItem(inventoryObject);
            infoDesk.Show(names[gameObject.name]);
            pickUpSound.PlayOneShot(pickUpSound.clip);
            Destroy(gameObject);


            if (playerScript.pointerAimTransform == gameObject.transform)
                playerScript.StopPointerAiming();

            if (anotherObject != null)
                playerScript.ChangePointerAim(anotherObject.transform);
            else if (nextActionPlace != null)
                playerScript.ChangePointerAim(nextActionPlace.transform);
        }
    }

    void Update()
    {
        if (IsHeroInArea && Input.GetKeyUp(KeyCode.F))
            TryToTake();
    }
}
