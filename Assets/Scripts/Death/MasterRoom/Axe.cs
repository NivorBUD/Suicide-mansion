using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public GameObject getPlace, holdingPlace;
    public bool isReady = false;

    private bool needToMove, needToHit;
    private float zAngle;

    void Start()
    {
        
    }

    void Update()
    {
        if (!isReady && needToMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, holdingPlace.transform.position, Time.deltaTime);
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(0.48f, 0.48f, 0.48f), 3 * Time.deltaTime);
        }

        if (needToMove && transform.position == holdingPlace.transform.position && transform.localScale.x == 0.48f)
        {
            isReady = true;
            needToMove = false;
        }

        if (needToHit)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, zAngle), 5 * Time.deltaTime);

        if (needToHit && Math.Abs(transform.rotation.eulerAngles.z - 330) <= 0.5f)
            zAngle = 40;

        if (needToHit && Math.Abs(transform.rotation.eulerAngles.z - 30) <= 0.5f)
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
        zAngle = -30;
    }
}
