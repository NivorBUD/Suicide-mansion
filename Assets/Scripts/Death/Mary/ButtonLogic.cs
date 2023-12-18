using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    public AudioSource cast;

    private void OnMouseDown()
    {
        gameObject.GetComponentInParent<MirrorDeath>().SpawnMary();
        PlayCastSound();// звук произнесения заклинания - ПОФИКСИ
    }

    private void PlayCastSound()
    {
        cast.Play();
    }
}
