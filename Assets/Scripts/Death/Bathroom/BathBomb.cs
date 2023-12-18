using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathBomb : MonoBehaviour
{
    [SerializeField] private GameObject getPlace;
    [SerializeField] private GameObject holdingPlace;
    [SerializeField] private GameObject throwPlace;
    [SerializeField] private TriggerByName trigger;
    private bool needToMove;
    public bool isReady;
    private Rigidbody2D rb;
    private bool needToThrow;
    

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        trigger.interactionName = gameObject.name;
    }

    void Update()
    {
        if (needToThrow && isReady && rb.bodyType == RigidbodyType2D.Kinematic)
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, throwPlace.transform.position, Time.deltaTime);

        if (!isReady && needToMove)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, holdingPlace.transform.position, Time.deltaTime);
            gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, new Vector3(0.8f, 0.8f, 0.8f), Time.deltaTime);
        }

        if (gameObject.transform.position == holdingPlace.transform.position)
            isReady = true;

        if (gameObject.transform.position == throwPlace.transform.position)
            rb.bodyType = RigidbodyType2D.Dynamic;

        if (trigger.isTriggered)
        {
            Destroy(gameObject);
        }
    }

    public void GetAndMoveToHand()
    {
        gameObject.SetActive(true);
        gameObject.transform.position = getPlace.transform.position;
        needToMove = true;
    }

    public void ThrowToBath()
    {
        needToThrow = true;
    }
}
