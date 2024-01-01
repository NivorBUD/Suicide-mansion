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
        names["Shovel"] = "������";
        names["Screwdriver"] = "�������";
        names["Marker"] = "������";
        names["Slingshot"] = "�������";
        names["Bathroom Key"] = "���� �� ������";
        names["Pantaloons"] = "���������";
        names["Rope"] = "������";
        names["Bath bomb"] = "�������� ��� �����";
        names["H2SO4"] = "������� (H2SO4)";
        names["CaF2"] = "������� (CaF2)";
        names["Acid"] = "�������";
        names["Flamethrower"] = "������";
        names["Screws"] = "�����";
        names["Key"] = "������� �����";
        names["Candle"] = "������";
        names["Board"] = "�����";
        names["Axe"] = "�����";
        names["Treasure key"] = "���� �� ��������";
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
