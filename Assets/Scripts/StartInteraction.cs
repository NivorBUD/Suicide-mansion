using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject LeverPlatform;
    private SpriteRenderer sprite;
    private bool IsHeroInArea = false;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsHeroInArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsHeroInArea = false;
    }

    void Update()
    {
        if (IsHeroInArea && Input.GetKeyDown(KeyCode.F))
        {
            PlayRingSound();
            Invoke(nameof(Teleport), 0.5f);
        }
    }

    private void Teleport()
    {
        LeverPlatform.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void PlayRingSound()
    {

    }
}
