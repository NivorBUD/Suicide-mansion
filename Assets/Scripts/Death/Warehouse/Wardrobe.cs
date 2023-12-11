using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    [SerializeField] Sprite brakeSprite;
    [SerializeField] GameObject flammenwerfer;
    [SerializeField] GameObject bathBomb;

    public void ChangeSprite()
    {
        //gameObject.GetComponent<SpriteRenderer>().sprite = brakeSprite;
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
    }

    public void DropFlammenwerfer()
    {
        flammenwerfer.SetActive(true);
        bathBomb.SetActive(true);
        bathBomb.GetComponent<Rigidbody2D>().AddForce(new Vector2(3, 0));
    }

    private void Update()
    {
            
            
    }
}
