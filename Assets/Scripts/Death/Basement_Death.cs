using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Basement_Death : DeathClass
{
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject ghost;
    public static float speed = 0.0f;
    public bool isStart = false;
    public bool isEnd = false;


    private Vector3 leftWallNewPos;
    private Vector3 rightWallNewPos;
    private Vector3 leftWallStartPos;
    private Vector3 rightWallStartPos;
    private Vector3 deathLeftWallNewPos;
    private Vector3 deathRightWallNewPos;
    private GameObject player;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }


    public override void StartDeath()
    {
        leftWallStartPos = leftWall.transform.position;
        rightWallStartPos = rightWall.transform.position;

        isStart = true;
        leftWallNewPos = leftWall.transform.position;
        leftWallNewPos.x += 7.2f;
        rightWallNewPos = rightWall.transform.position;
        rightWallNewPos.x -= 7.2f;

        deathLeftWallNewPos = leftWall.transform.position;
        deathLeftWallNewPos.x += 6.7f;
        deathRightWallNewPos = rightWall.transform.position;
        deathRightWallNewPos.x -= 6.7f;
    }

    public void DeathHero()
    {
        var heroScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        heroScript.Death();
        isEnd = true;
        isStart = false;
        Invoke("MoveWallToStart", 1);
    }

    private void MoveWallToStart()
    {
        leftWall.transform.position = Vector3.MoveTowards(leftWall.transform.position, leftWallStartPos, 3 * speed * Time.deltaTime);
        rightWall.transform.position = Vector3.MoveTowards(rightWall.transform.position, rightWallStartPos, 3 * speed * Time.deltaTime);
    }


    private void Update()
    {
        if (isStart)
        {
            leftWall.transform.position = Vector3.MoveTowards(leftWall.transform.position, leftWallNewPos, speed * Time.deltaTime);
            rightWall.transform.position = Vector3.MoveTowards(rightWall.transform.position, rightWallNewPos, speed * Time.deltaTime);
        }

        if (!isEnd && leftWall.transform.position.x >= leftWallNewPos.x && rightWall.transform.position.x <= rightWallNewPos.x)
            DeathHero();

        if (isEnd && player)
            MoveWallToStart();
    }
}
