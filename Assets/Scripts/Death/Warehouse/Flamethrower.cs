using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    public bool isReady;
    [SerializeField] GameObject getPlace;
    [SerializeField] GameObject holdingPlace;
    [SerializeField] GameObject fire;
    [SerializeField] Transform firePlace;
    private bool needToMove;

    void Update()
    {
        if (!isReady && needToMove)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, holdingPlace.transform.position, Time.deltaTime);
            gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, new Vector3(0.8f, 0.8f, 0.8f), Time.deltaTime);
        }

        if (gameObject.transform.position == holdingPlace.transform.position)
            isReady = true;
    }

    public void GetAndMoveToHand()
    {
        gameObject.SetActive(true);
        gameObject.transform.position = getPlace.transform.position;
        needToMove = true;
    }

    public void Fire()
    {
        fire.transform.position = firePlace.position;
        fire.SetActive(true);
    }
}
