using System;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class GhostSon : MonoBehaviour
{
    [SerializeField]
    Transform pos1, center, endPos, bathPos, ghostPos, hidePos;

    [SerializeField]
    TriggerByName trigger;

    public bool isNearThePlayer;
    public bool isEnd;

    private GameObject player;
    private bool needToCircleMove, needToMove, needToMoveToPlayer, needToDrawn, needToGoToMom, needToHide;
    private float radius;
    private double angle = 0;
    private float speed = 5;
    private SpriteRenderer sprite;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        radius = Vector3.Distance(pos1.position, center.position);
        trigger.interactionName = gameObject.name;
        sprite = GetComponent<SpriteRenderer>();
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

        if (needToGoToMom)
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, ghostPos.position, speed * Time.deltaTime);

        if (gameObject.transform.position == ghostPos.position)
            needToGoToMom = false;

        if (needToHide)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, hidePos.position, speed * Time.deltaTime);
            sprite.color = new Color(255, 255, 255, Mathf.MoveTowards(sprite.color.a, 0, 0.5f * Time.deltaTime));
            if (sprite.color.a == 0)
                Destroy(gameObject);
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
            Destroy(trigger.gameObject);
        }

        if (needToDrawn)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, endPos.position, speed * Time.deltaTime);
            player.transform.position = new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y + 0.1f, 0);
        }

        if (gameObject.transform.position == endPos.position)
            endPos = bathPos;

        if (needToDrawn && gameObject.transform.position == bathPos.position)
            isEnd = true;
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

    public void GoToMom()
    {
        needToGoToMom = true;
    }

    public void Hide()
    {
        needToHide = true;
    }
}
