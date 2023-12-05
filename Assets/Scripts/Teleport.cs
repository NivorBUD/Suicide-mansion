using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] GameObject tpPlace;

    private Hero hero;
    public GameObject ghost;

    private void Start()
    {
        hero = GameObject.FindWithTag("Player").GetComponent<Hero>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hero.gameObject.transform.position = tpPlace.transform.position;
        ghost.GetComponent<Ghost>().ChangeAimToPlayer();
    }
}
