using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    private bool isReady;
    [SerializeField] GameObject getPlace;
    [SerializeField] GameObject holdingPlace;
    [SerializeField] Sprite fallSprite;
    private bool needToMove;

    void Update()
    {
        if (!isReady && needToMove)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, holdingPlace.transform.position, Time.deltaTime);
            gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, new Vector3(0.25f, 0.25f, 0.25f), Time.deltaTime);
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

    public void ChangeSpriteToFall()
    {
        GetComponent<SpriteRenderer>().sprite = fallSprite;
    }
}
