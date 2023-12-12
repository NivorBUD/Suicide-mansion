using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Tilemaps;
using UnityEngine;


public class Hero : MonoBehaviour
{
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float speed = 3f;
    [SerializeField] GameObject ghost;
    [SerializeField] GameObject getPlace;
    [SerializeField] GameObject holdingPlace;
    public GameObject bullet;
    public Rigidbody2D rb;
    public Dictionary<string, GameObject> inventory = new();
    public Transform bulletPlace;
    public bool isCutScene = false;
    

    private Vector3 liftPos;
    private bool isLift;
    private bool isHorizontalLift;
    private Animator anim;
    private SpriteRenderer sprite;
    private CameraController mainCamera;
    private DeathClass death;
    private Ghost ghostScript;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        bulletPlace = GameObject.FindWithTag("Bullet Start Place").GetComponent<Transform>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        ghostScript = GameObject.FindWithTag("Ghost").GetComponent<Ghost>();
    }

    private void Run()
    {
        State = States.walk;

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
        getPlace.transform.localPosition = new Vector3((sprite.flipX ? 1 : -1) * Math.Abs(getPlace.transform.localPosition.x), getPlace.transform.localPosition.y, getPlace.transform.localPosition.z);
        holdingPlace.transform.localPosition = new Vector3((sprite.flipX ? -1 : 1) * Math.Abs(holdingPlace.transform.localPosition.x), holdingPlace.transform.localPosition.y, holdingPlace.transform.localPosition.z);
    }

    public void StartLift(bool isHorizontalLadder, Vector3 anotherLadderPos)
    {
        rb.velocity = Vector3.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        this.isHorizontalLift = isHorizontalLadder;
        isLift = !isHorizontalLadder;
        liftPos = anotherLadderPos;
        isCutScene = true;
    }

    public void StopLift()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        isLift = false;
        isHorizontalLift = false;
        isCutScene = false;
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
        State = States.idle;
        anim = GetComponent<Animator>();

        if (!isCutScene && Input.GetButtonDown("Jump"))
            Jump();

        if (!isCutScene && Input.GetButton("Horizontal"))
            Run();

        if (isLift || isHorizontalLift)
        {
            State = isHorizontalLift ? States.walk : States.lift;
            transform.position = Vector3.MoveTowards(transform.position, liftPos, speed * Time.deltaTime);
            if (transform.position == liftPos)
                StopLift();
        }
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
        // ��� ������ ����� ������� �� ����������
        sprite.flipX = !sprite.flipX;
        Invoke(nameof(Death), 1);
    }

    public void Death()
    {
        EndCutScene();
        gameObject.SetActive(false);
        Invoke(nameof(Respawn), 3);

        if (transform.localScale.y != 0.4 || transform.localScale.x != 0.4)
        {
            var sc = transform.localScale;
            sc.y = 0.4f;
            sc.x = 0.4f;
            transform.localScale = sc;
        }
    }

    public void Respawn()
    {
        var rot = gameObject.transform.rotation;
        rot.z = 0;
        rb.freezeRotation = true;
        transform.rotation = rot;
        gameObject.SetActive(true);
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
    lift
}
