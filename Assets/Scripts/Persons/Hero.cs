using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private ParticleSystem respawnPoof;
    [SerializeField] private TextMeshProUGUI missionText;
    [SerializeField] GameObject ghost, getPlace, holdingPlace;
    public GameObject bullet, achivmentRedCircle;
    public Rigidbody2D rb;
    public Dictionary<string, GameObject> inventory = new();
    public Transform bulletPlace;
    public bool isCutScene, isHandsUp, isHorizontalLift, isAtTerrace, isPause, canPause;
    public Ghost ghostScript;
    public GameObject pointer;

    private Vector3 liftPos, pointerAim;
    private bool isLift, isScared, isUppingHands, isAcid, isSit;
    private Animator anim;
    private SpriteRenderer sprite;
    private CameraController mainCamera;
    private Sprite standardSprite;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        bulletPlace = GameObject.FindWithTag("Bullet Start Place").GetComponent<Transform>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        ghost = GameObject.FindWithTag("Ghost");
        ghostScript = ghost.GetComponent<Ghost>();
        standardSprite = sprite.sprite;
        canPause = true;
        ChangeMission("Войти в дом");
    }

    private void Run()
    {
        State = States.walk;

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    public void StartLift(bool isHorizontalLadder, Vector3 anotherLadderPos)
    {
        rb.velocity = Vector3.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        this.isHorizontalLift = isHorizontalLadder;
        isLift = !isHorizontalLadder;
        liftPos = anotherLadderPos;
        isCutScene = true;
        sprite.flipX = transform.position.x > anotherLadderPos.x;
    }

    public void StopLift()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        isLift = false;
        isHorizontalLift = false;
        isCutScene = false;
    }

    public void StartSit()
    {
        isSit = true;
        State = States.sit;
    }

    public void EndSit()
    {
        isSit = false;
    }

    public void AddToInventiry([SerializeField] GameObject InventoryObject)
    {
        inventory[InventoryObject.name] = InventoryObject;
    }

    public void DelFromInventiry([SerializeField] GameObject InventoryObject)
    {
        inventory.Remove(InventoryObject.name);
    }

    void Update()
    {
        if (isPause) 
            return;

        if (transform.position.z != 0 && !isHorizontalLift)
        {
            var pos = transform.position;
            pos.z = 0;
            transform.position = pos;   
        }
        else if (isHorizontalLift)
        {
            var pos = transform.position;
            pos.z = 1;
            transform.position = pos;
        }

        ChangePointerAngle();
        ChangeSprite();

        if (!ghostScript.isDialog && !isCutScene && Input.GetButtonDown("Jump"))
            Jump();

        if (!ghostScript.isDialog && !isCutScene && Input.GetButton("Horizontal"))
            Run();

        if (isLift || isHorizontalLift)
        {
            State = isHorizontalLift ? States.walk : States.lift;
            transform.position = Vector3.MoveTowards(transform.position, liftPos, speed * Time.deltaTime);
            if (Math.Abs(transform.position.x - liftPos.x) <= 0.1f &&
                Math.Abs(transform.position.y - liftPos.y) <= 0.1f)
                StopLift();
        }

        getPlace.transform.localPosition = new Vector3((sprite.flipX ? 1 : -1) * Math.Abs(getPlace.transform.localPosition.x), getPlace.transform.localPosition.y, getPlace.transform.localPosition.z);
        holdingPlace.transform.localPosition = new Vector3((sprite.flipX ? -1 : 1) * Math.Abs(holdingPlace.transform.localPosition.x), holdingPlace.transform.localPosition.y, holdingPlace.transform.localPosition.z);
    }

    public void ChangeMission(string mission)
    {
        missionText.text = mission;
    }

    private void ChangeSprite()
    {
        if (!isSit && !isAcid && !isUppingHands && !isScared && State != States.electric)
            State = States.idle;

        else if (isScared)
        {
            State = States.scare;
            if (sprite.sprite.name == "Player Scaried 9")
                Death();
        }
        else if (isAcid)
        {
            State = States.acid;
            if (sprite.sprite.name == "Player Acid 9")
                Death();
        }
        else if (isSit)
            State = States.sit;
        else if (isUppingHands)
        {
            if (sprite.sprite.name == "PlayerPrimer 7" || State == States.handsUpStand)
            {
                isHandsUp = true;
                State = States.handsUpStand;
            }
            else
                State = States.handsUp;
        }
    }

    private void ChangePointerAngle()
    {
        if (!pointer.activeSelf)
            return;

        var zAngle = Math.Atan2(pointerAim.y - pointer.transform.position.y, 
            pointerAim.x - pointer.transform.position.x) * 180 / Math.PI;

        pointer.transform.rotation = Quaternion.Euler(0, 0, (float)zAngle);
    }

    public void ChangePointerAim(Transform pos)
    {
        pointerAim = pos.position;
        pointer.SetActive(true);
    }

    public void StopPointerAiming()
    {
        pointer.SetActive(false);
    }

    public void EletricSchock()
    {
        State = States.electric;
    }

    public void EndCutScene()
    {
        isCutScene = false;
        mainCamera.ZoomIn(5);
        mainCamera.ChangeAimToPlayer();
        ghostScript.canChangePhraseByButton = true;
    }

    public void DeadlyScare()
    {
        isScared = true;
        PlayScareSound(); // звук испуга
        sprite.flipX = !sprite.flipX;
    }

    public void UpHands()
    {
        isUppingHands = true;
        sprite.flipX = false;
    }

    public void StopHandsUp()
    {
        isUppingHands = false;
        isHandsUp = false;
    }

    public void Acid()
    {
        isAcid = true;
        PlayAcidSound();
    }

    private void PlayAcidSound()
    {

    }

    private void PlayScareSound()
    {

    }

    public void Death()
    {
        EndCutScene();
        isScared = false;
        isUppingHands = false;
        isAcid = false;
        gameObject.SetActive(false);
        State = States.idle;
        Invoke(nameof(RespawnPoof), 1);

        if (transform.localScale.y != 0.4 || transform.localScale.x != 0.4)
        {
            var sc = transform.localScale;
            sc.y = 0.4f;
            sc.x = 0.4f;
            transform.localScale = sc;
        }
    }

    public void NoRespawnDeath()
    {
        EndCutScene();
        isScared = false;
        isUppingHands = false;
        isAcid = false;
        gameObject.SetActive(false);
        State = States.idle;
    }

    public void RespawnPoof()
    {
        var pos = transform.position;
        pos.z = respawnPoof.gameObject.transform.position.z;
        respawnPoof.gameObject.transform.position = pos;
        respawnPoof.Play();
        Invoke(nameof(Respawn), 1.5f);
    }

    private void Respawn()
    {
        var rot = gameObject.transform.rotation;
        rot.z = 0;
        rb.freezeRotation = true;
        transform.rotation = rot;
        gameObject.SetActive(true);
    }

    public void Up()
    {
        EndSit();
        sprite.flipX = true;
        rb.AddForce(transform.right * jumpForce, ForceMode2D.Impulse);
        Invoke(nameof(TurnUp), 0.05f);
    }

    private void TurnUp()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        isCutScene = false;
    }

    public States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Jump()
    {
        if (rb.velocity.y == 0)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);        
    }
}

public enum States 
{
    idle,
    walk,
    lift,
    electric,
    scare,
    handsUp, 
    handsUpStand,
    acid,
    sit
}
