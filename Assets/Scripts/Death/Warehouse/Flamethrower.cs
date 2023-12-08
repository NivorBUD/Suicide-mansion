using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    private bool isReady;
    [SerializeField] GameObject getPlace;
    [SerializeField] GameObject holdingPlace;
    private bool needToMove;

    void Update()
    {
        if (!isReady && needToMove)
        {
            var pos = holdingPlace.transform.position;
            pos.z = 0;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, pos, Time.deltaTime);
            gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, new Vector3(0.3f, 0.3f, 0.3f), Time.deltaTime);
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
}
