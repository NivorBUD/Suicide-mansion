using System.Collections;
using UnityEngine;

public class BasementDeath : MonoBehaviour
{
    public GameObject leftWall, rightWall;
    public static float speed = 0.0f;
    public bool isStart,isEnd;
    public AudioClip wallsShiftingSound;
    [SerializeField] private GameObject Button1, Button2, Button3, Button4;
    [SerializeField] private GameObject blackOut;
    [SerializeField] private GameObject shovel;
    [SerializeField] private GameObject navigationButton;
    [SerializeField] private ChangeImage deathopediaImage;

    private Vector3 leftWallNewPos, rightWallNewPos;
    private Vector3 leftWallStartPos, rightWallStartPos;
    private Vector3 deathLeftWallNewPos, deathRightWallNewPos;
    private GameObject ghost, player;
    private Ghost ghostScript;
    private Hero playerScript;
    private bool isDeathopediaChacked;


    private void Start()
    {
        ghost = GameObject.FindWithTag("Ghost");
        ghostScript = ghost.GetComponent<Ghost>();
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
    }

    public void StartDeath()
    {   
        playerScript.canPause = false;
        ghostScript.isDialog = false;
        blackOut.SetActive(false);
        ghostScript.canChangePhraseByButton = false;
        isStart = true;
        StartCoroutine(Ghost_COR());

        if (playerScript.levelComplete != 0)
            return;

        AudioSource.PlayClipAtPoint(wallsShiftingSound, transform.position);
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
        playerScript.NoRespawnDeath();
        ghostScript.canChangePhraseByButton = false;
        isEnd = true;
        speed = 1;
    }

    private void MoveWallToStart()
    {
        leftWall.transform.position = Vector3.MoveTowards(leftWall.transform.position, leftWallStartPos, 3 * speed * Time.deltaTime);
        rightWall.transform.position = Vector3.MoveTowards(rightWall.transform.position, rightWallStartPos, 3 * speed * Time.deltaTime);
    }

    private void Update()
    {
        if (playerScript.levelComplete >= 1 && !isDeathopediaChacked)
        {
            deathopediaImage.ChangeSprite();
            isDeathopediaChacked = true;
            navigationButton.SetActive(true);
        }
            

        if ((ReadyToDeath() && Input.GetKeyDown(KeyCode.F)) || (playerScript.levelComplete == 1 && !isStart))
            StartDeath();

        if (isStart && !isEnd)
        {
            leftWall.transform.position = Vector3.MoveTowards(leftWall.transform.position, leftWallNewPos, speed * Time.deltaTime);
            rightWall.transform.position = Vector3.MoveTowards(rightWall.transform.position, rightWallNewPos, speed * Time.deltaTime);
        }

        if (!isEnd && leftWall.transform.position.x >= leftWallNewPos.x && rightWall.transform.position.x <= rightWallNewPos.x)
        {
            deathopediaImage.ChangeSprite();
            isDeathopediaChacked = true;
            DeathHero();
        }

        if (isEnd)
            MoveWallToStart();
    }

    public bool ReadyToDeath()
    {
        return !isStart && gameObject && ghostScript.isDialog && player.transform.position.y < -5;
    }

    IEnumerator Ghost_COR()
    {
        if (playerScript.levelComplete == 0)
        {
            if (ghostScript.phraseIndex == 0)
                ghostScript.ChangePhrase();
            ghostScript.speed = 3.5f;
            ghostScript.ChangeAim(Button1.transform, -0.55f, -0.2f);
            yield return new WaitForSeconds(1f);
            ghostScript.ChangePhrase(); //2 - ������ ����� 
            yield return new WaitForSeconds(1f);
            ghostScript.ChangePhrase(); //3
            yield return new WaitForSeconds(1.5f);
            Destroy(Button1);
            speed = 0.5f;
            ghostScript.ChangeAim(Button2.transform, 0.7f, 0.2f);

            ghostScript.ChangePhrase(); //4
            yield return new WaitForSeconds(1.5f);
            ghostScript.ChangePhrase(); //5
            yield return new WaitForSeconds(1.5f);
            Destroy(Button2);
            speed = 1.3f;
            ghostScript.ChangeAim(Button3.transform, 0.55f, 0);

            ghostScript.ChangePhrase(); //6
            yield return new WaitForSeconds(1.5f);
            ghostScript.ChangePhrase(); //7
            yield return new WaitForSeconds(1.5f);
            Destroy(Button3);
            speed = 0.6f;
            ghostScript.ChangeAim(Button4.transform, -0.7f, -0.4f);

            ghostScript.ChangePhrase(); //8
            yield return new WaitForSeconds(1.5f);
            Destroy(Button4);
            speed = 10f;
            ghostScript.ChangeAimToPlayer();

            ghostScript.ChangePhrase();
            yield return new WaitForSeconds(1.5f);

            ghostScript.speed = 2;

            while (!isEnd)
                yield return new WaitForSeconds(0.1f);

            for (int i = 0; i < 3; i++)
            {
                ghostScript.ChangePhrase();
                yield return new WaitForSeconds(2f);
            }

            playerScript.RespawnPoof();
            playerScript.canPause = true;
            playerScript.levelComplete = 1;
            ghostScript.canChangePhraseByButton = true;
            playerScript.SaveSave();
        }

        if (ghostScript.GetDialog().Length == 27)
        {
            playerScript.canPause = true;
            ghostScript.canChangePhraseByButton = true;

            while (ghostScript.phraseIndex < 24)
                yield return new WaitForSeconds(0.1f);

            ghostScript.canChangePhraseByButton = false;
            InventoryLogic.canGetItems = true;

            while (shovel != null)
                yield return new WaitForSeconds(0.1f);

            LadderInteraction.canUseLadders = true;
            navigationButton.SetActive(true);
            blackOut.SetActive(true);
            ghostScript.ChangePhrase();
            while (ghostScript.phraseIndex != 26)
                yield return new WaitForSeconds(0.1f);

            yield return new WaitForSeconds(1.5f);

            ghostScript.ChangePhrase();
        }
    }
}
