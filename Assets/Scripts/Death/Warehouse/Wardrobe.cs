using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    [SerializeField] Sprite brakeSprite, acidSprite;
    [SerializeField] GameObject flamethrower;
    [SerializeField] GameObject bathBomb;

    public void Break()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = brakeSprite;
    }
    
    public void Acid()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = acidSprite;
    }

    public void DropFlamethrower()
    {
        flamethrower.SetActive(true);
        bathBomb.SetActive(true);
        bathBomb.GetComponent<Rigidbody2D>().AddForce(new Vector2(3, 0));
    }
}
