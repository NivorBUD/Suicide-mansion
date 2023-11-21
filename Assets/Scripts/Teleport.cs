using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] Hero hero;
    [SerializeField] GameObject ghost;
    [SerializeField] GameObject tpPlace;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        Vector3 pos = tpPlace.transform.position;
        hero.gameObject.transform.position = pos;
        ghost.SetActive(true);
    }
}
