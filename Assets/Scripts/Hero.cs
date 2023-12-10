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
    public GameObject bullet;
    public Rigidbody2D rb;
    public Dictionary<string, GameObject> inventory = new();
    public Transform bulletPlace;
    public bool isCutScene = false;

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
        anim = gameObject.GetComponent<Animator>();

        if (!isCutScene && Input.GetButtonDown("Jump"))
            Jump();

        if (!isCutScene && Input.GetButton("Horizontal"))
            Run();
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

        if (transform.localScale.y != 1.23 || transform.localScale.x != 1.23)
        {
            var sc = gameObject.transform.localScale;
            sc.y = 1.23f;
            sc.x = 1.23f;
            gameObject.transform.localScale = sc;
        }
    }

    public void Respawn()
    {
        var rot = gameObject.transform.rotation;
        rot.z = 0;
        rb.freezeRotation = true;
        gameObject.transform.rotation = rot;
        gameObject.SetActive(true);
    }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Jump()
    {
        if (rb.velocity.y == 0)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);        
        }
    }
}

public enum States 
{
    idle,
    walk
}
