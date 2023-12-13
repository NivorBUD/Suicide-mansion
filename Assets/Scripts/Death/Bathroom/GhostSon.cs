using System;
using UnityEngine;

public class GhostSon : MonoBehaviour
{
    [SerializeField]
    Transform pos1, center, endPos, bathPos;

    [SerializeField]
    TriggerByName trigger;

    public bool isNearThePlayer;
    public bool isEnd;

    private GameObject player;
    private bool needToCircleMove;
    private bool needToMove;
    private bool needToMoveToPlayer;
    private bool needToDrawn;
    private float radius;
    private double angle = 0;
    private float speed = 5;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        radius = Vector3.Distance(pos1.position, center.position);
        trigger.interactionName = gameObject.name;
    }

    void Update()
    {
        if (needToMove)
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, pos1.position, speed * Time.deltaTime);

        if (needToMove && gameObject.transform.position == pos1.position)
        {
            needToMove = false;
            needToCircleMove = true;
        }

        if (needToCircleMove)
        {
            float posX = center.position.x + (float)Math.Cos(angle) * radius;
            float posY = center.position.y + (float)Math.Sin(angle) * radius;
            gameObject.transform.position = new Vector3(posX, posY, center.position.z);
            angle += speed * Time.deltaTime;
        }

        if (trigger.isTriggered)
        {
            needToCircleMove = false;
            needToMove = false;
            needToMoveToPlayer = true;
            var pos = player.transform.position;
            pos.x -= 0.5f;
            pos.y -= 0.1f;
            pos1.position = pos;
        }

        if (needToMoveToPlayer)
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, pos1.position, speed * Time.deltaTime);

        if (needToMoveToPlayer && gameObject.transform.position == pos1.position)
        {
            isNearThePlayer = true;
            needToMoveToPlayer = false;
        }

        if (needToDrawn)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, endPos.position, speed * Time.deltaTime);
            player.transform.position = new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y + 0.1f, 0);
        }

        if (gameObject.transform.position == endPos.position)
        {
            endPos = bathPos;
        }

        if (needToDrawn && gameObject.transform.position == bathPos.position)
        {
            isEnd = true;
        }
    }

    public void GetOutAndMoveToPlayer()
    {
        needToMove = true;
    }

    public void DrawnPlayer()
    {
        needToDrawn = true;
    }

    public void StopDrawn()
    {
        needToDrawn = false;
    }
}
