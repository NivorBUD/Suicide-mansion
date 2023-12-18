using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class BasementDeath : DeathClass
{
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject exit;
    public static float speed = 0.0f;
    public bool isStart = false;
    public bool isEnd = false;
    [SerializeField] GameObject Button1;
    [SerializeField] GameObject Button2;
    [SerializeField] GameObject Button3;
    [SerializeField] GameObject Button4;


    private Vector3 leftWallNewPos;
    private Vector3 rightWallNewPos;
    private Vector3 leftWallStartPos;
    private Vector3 rightWallStartPos;
    private Vector3 deathLeftWallNewPos;
    private Vector3 deathRightWallNewPos;
    private GameObject player;
    private GameObject ghost;
    private Ghost ghost_script;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        ghost = GameObject.FindWithTag("Ghost");
        ghost_script = ghost.GetComponent<Ghost>();
    }


    public override void StartDeath()
    {
        isStart = true;
        StartCoroutine(Ghost_COR());
        leftWallStartPos = leftWall.transform.position;
        rightWallStartPos = rightWall.transform.position;

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
        speed = 1;
        Invoke(nameof(MoveWallToStart), 1f);
        heroScript.EndCutScene();
    }

    private void MoveWallToStart()
    {
        leftWall.transform.position = Vector3.MoveTowards(leftWall.transform.position, leftWallStartPos, 3 * speed * Time.deltaTime);
        rightWall.transform.position = Vector3.MoveTowards(rightWall.transform.position, rightWallStartPos, 3 * speed * Time.deltaTime);
    }


    private void Update()
    {
        if (isStart && !isEnd)
        {
            leftWall.transform.position = Vector3.MoveTowards(leftWall.transform.position, leftWallNewPos, speed * Time.deltaTime);
            rightWall.transform.position = Vector3.MoveTowards(rightWall.transform.position, rightWallNewPos, speed * Time.deltaTime);
        }

        if (!isEnd && leftWall.transform.position.x >= leftWallNewPos.x && rightWall.transform.position.x <= rightWallNewPos.x)
            DeathHero();

        if (isEnd)
            MoveWallToStart();
    }

    public override bool ReadyToDeath()
    {
        return !isStart && gameObject && ghost_script.isDialog;
    }

    IEnumerator Ghost_COR()
    {
        ghost_script.speed = 3.5f;
        ghost_script.ChangeAim(Button1.transform, -0.55f, -0.2f);
        yield return new WaitForSeconds(1f);
        ghost_script.ChangePhrase(); //2 - индекс фразы 
        yield return new WaitForSeconds(1f);
        ghost_script.ChangePhrase(); //3
        yield return new WaitForSeconds(1.5f);
        Destroy(Button1);
        speed = 0.5f;
        ghost_script.ChangeAim(Button2.transform, 0.7f, 0.2f);

        ghost_script.ChangePhrase(); //4
        yield return new WaitForSeconds(1.5f);
        ghost_script.ChangePhrase(); //5
        yield return new WaitForSeconds(1.5f);
        Destroy(Button2);
        speed = 1.3f;
        ghost_script.ChangeAim(Button3.transform, 0.55f, 0);

        ghost_script.ChangePhrase(); //6
        yield return new WaitForSeconds(1.5f);
        ghost_script.ChangePhrase(); //7
        yield return new WaitForSeconds(1.5f);
        Destroy(Button3);
        speed = 0.6f;
        ghost_script.ChangeAim(Button4.transform, -0.7f, -0.4f);

        ghost_script.ChangePhrase(); //8
        yield return new WaitForSeconds(1.5f);
        Destroy(Button4);
        speed = 10f;
        ghost_script.ChangeAimToPlayer();

        ghost_script.ChangePhrase();
    }
}
