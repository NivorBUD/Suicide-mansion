using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    public AudioSource cast;

    private void OnMouseDown()
    {
        gameObject.GetComponentInParent<MirrorDeath>().SpawnMary();
        PlayCastSound();// ���� ������������ ���������� - �������
    }

    private void PlayCastSound()
    {
        cast.Play();
    }
}
