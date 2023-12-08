using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    [SerializeField] Sprite brakeSprite;
    [SerializeField] GameObject flammenwerfer;
    public void ChangeSprite()
    {
        //gameObject.GetComponent<SpriteRenderer>().sprite = brakeSprite;
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
    }

    public void DropFlammenwerfer()
    {
        flammenwerfer.SetActive(true);
    }
}
