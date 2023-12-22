using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bath : MonoBehaviour
{
    [SerializeField] private Sprite bubblesSprite;
    private SpriteRenderer sprite;
    
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite()
    {
        sprite.sprite = bubblesSprite;
        gameObject.transform.localPosition = new Vector3(0, 0.42f, gameObject.transform.localPosition.z);
    }
}
