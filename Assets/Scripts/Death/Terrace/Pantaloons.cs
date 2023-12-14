using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pantaloons : MonoBehaviour
{
    [SerializeField] private GameObject getPlace;
    [SerializeField] private GameObject holdingPlace;
    [SerializeField] private GameObject flagPoleDownPos;
    [SerializeField] private GameObject flagPoleUpPos;

    private bool needToMove;
    private bool needToMoveToDownPos;
    private bool needToMoveToUpPos;
    public bool isReady;
    public bool isReadyToLightning;

    void Start()
    {
        
    }

    void Update()
    {
        if (!isReady && needToMove)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, holdingPlace.transform.position, Time.deltaTime);
            gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, new Vector3(1, 1, 1), Time.deltaTime);
        }

        if (gameObject.transform.position == holdingPlace.transform.position && gameObject.transform.localScale.x == 1)
            isReady = true;

        if (needToMoveToDownPos)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, flagPoleDownPos.transform.position, Time.deltaTime);
            if (gameObject.transform.position == flagPoleDownPos.transform.position)
            {
                needToMoveToDownPos = false;
                needToMoveToUpPos = true;
            }
        }

        if (needToMoveToUpPos)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, flagPoleUpPos.transform.position, 2 * Time.deltaTime);
            if (gameObject.transform.position == flagPoleUpPos.transform.position)
            {
                isReadyToLightning = true;
                needToMoveToUpPos = false;
            }
        }
    }

    public void GetAndMoveToHand()
    {
        gameObject.SetActive(true);
        gameObject.transform.position = getPlace.transform.position;
        needToMove = true;
    }

    public void MoveToUpPos()
    {
        needToMoveToDownPos = true;
    }
}
