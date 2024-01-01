using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject LeverPlatform;
    private SpriteRenderer sprite;
    private bool IsHeroInArea = false;

    // Add an AudioSource variable to play the sound
    private AudioSource audioSource;

    // Add a reference to your sound effect clip
    [SerializeField] AudioClip ringSound;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();

        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
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
        if (IsHeroInArea && Input.GetKeyDown(KeyCode.E))
        {
            PlayRingSound();
            Invoke(nameof(Teleport), 1.5f); // Delayed teleportation after 1.1 seconds
        }
    }

    private void Teleport()
    {
        LeverPlatform.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void PlayRingSound()
    {
        audioSource.clip = ringSound;
        audioSource.Play();
    }
}