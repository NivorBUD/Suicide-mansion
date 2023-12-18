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
    public bool needToRotate;
    private float zAngle;
    public int rotateNum;

    void Update()
    {
        if (!isReady && needToMove)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, holdingPlace.transform.position, Time.deltaTime);
            gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, new Vector3(0.8f, 0.8f, 0.8f), Time.deltaTime);
        }

        if (gameObject.transform.position == holdingPlace.transform.position)
            isReady = true;

        if (needToRotate)
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(0, 0, zAngle), Time.deltaTime);

        if (needToRotate && gameObject.transform.rotation.eulerAngles.z <= 330.3)
        {
            zAngle = 30;
            rotateNum++;
        }
            

        if (needToRotate && gameObject.transform.rotation.eulerAngles.z >= 359.7 && rotateNum % 2 == 1)
        {
            zAngle = -30;
            rotateNum++;
            if (rotateNum == 4)
                needToRotate = false;
        }
    }

    public void GetAndMoveToHand()
    {
        gameObject.SetActive(true);
        gameObject.transform.position = getPlace.transform.position;
        needToMove = true;
    }

    public void Fire()
    {
        rotateNum = 0;
        zAngle = -30;
        fire.transform.position = firePlace.position;
        fire.SetActive(true);
        Invoke(nameof(Rotate), 2f);
    }

    private void Rotate()
    {
        needToRotate = true;
    }
}