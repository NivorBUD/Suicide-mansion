using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    [SerializeField] Sprite brakeSprite, acidSprite;
    [SerializeField] GameObject flamethrower;
    [SerializeField] GameObject bathBomb;

    private Hero playerScript;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
    }

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
        playerScript.ChangePointerAim(flamethrower.transform);
        flamethrower.SetActive(true);
        bathBomb.SetActive(true);
    }
}
