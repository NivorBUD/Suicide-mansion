using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    public GameObject getPlace, holdingPlace;
    public bool isReady = false;

    private bool needToMove, needToHit;
    private float zAngle;

    void Update()
    {
        if (!isReady && needToMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, holdingPlace.transform.position, Time.deltaTime);
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(0.6f, 0.6f, 0.6f), 3 * Time.deltaTime);
        }

        if (needToMove && transform.position == holdingPlace.transform.position && transform.localScale.x == 0.6f)
        {
            isReady = true;
            needToMove = false;
        }

        if (needToHit)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, zAngle), 5 * Time.deltaTime);

        if (needToHit && Math.Abs(transform.rotation.eulerAngles.z - 30) <= 1)
            zAngle = -60;

        if (needToHit && Math.Abs(transform.rotation.eulerAngles.z - 310) <= 5)
        {
            isReady = true;
            needToHit = false;
        }
    }

    public void GetAndMoveToHand()
    {
        gameObject.SetActive(true);
        transform.position = getPlace.transform.position;
        needToMove = true;
    }

    public void Hit()
    {
        isReady = false;
        needToHit = true;
        zAngle = 30;
    }
}
