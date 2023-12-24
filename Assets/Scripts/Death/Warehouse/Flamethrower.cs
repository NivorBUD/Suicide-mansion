using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    public bool isReady;
    [SerializeField] GameObject getPlace;
    [SerializeField] GameObject holdingPlace;
    [SerializeField] SpriteRenderer fire;
    [SerializeField] Sprite[] fireSprites;
    public bool needToRotate;
    public int rotateNum;

    private float zAngle;
    private bool needToMove;
    private int fireIndex;

    void Update()
    {
        if (!isReady && needToMove)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, holdingPlace.transform.position, Time.deltaTime);
            gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, new Vector3(0.9f, 0.9f, 0.9f), Time.deltaTime);
        }

        if (gameObject.transform.position == holdingPlace.transform.position && gameObject.transform.localScale.x == 0.9f)
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

    IEnumerator Fireing()
    {
        while (true)
        {
            fireIndex = (fireIndex + 1) % 8;
            fire.sprite = fireSprites[fireIndex];
            yield return new WaitForSeconds(0.1f);
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
        fire.gameObject.SetActive(true);
        StartCoroutine(Fireing());
        Invoke(nameof(Rotate), 2f);
    }

    private void Rotate()
    {
        needToRotate = true;
    }
}
