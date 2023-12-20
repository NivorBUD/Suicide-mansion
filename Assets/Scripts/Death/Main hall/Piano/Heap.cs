using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heap : MonoBehaviour
{
    public bool isReady;

    [SerializeField] private Sprite secondSprite;
    [SerializeField] private Sprite thirdSprite;

    private SpriteRenderer sprite;
    private int spriteIndex;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite()
    {
        if (spriteIndex == 2)
        {
            isReady = true;
            Destroy(gameObject);
            return;
        }
            
        spriteIndex++;
        switch (spriteIndex)
        {
            case 1:
                sprite.sprite = secondSprite;
                break;
            case 2:
                sprite.sprite = thirdSprite;
                break;
        }
    }
}
