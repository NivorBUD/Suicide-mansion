using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    public bool isBreak;

    [SerializeField] Sprite brakeSprite, acidSprite;
    [SerializeField] GameObject flamethrower, bathBomb;
    [SerializeField] Plant plant;

    private Hero playerScript;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
    }

    private void Update()
    {
        if (playerScript.levelComplete >= 6 && flamethrower != null)
        {
            DropFlamethrower();
        }
    }

    public void Break()
    {
        isBreak = true;
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
