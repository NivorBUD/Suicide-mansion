using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Tilemaps;
using UnityEngine;


public class Hero : MonoBehaviour
{
    //[SerializeField] private float jumpForce = 10f;
    [SerializeField] private float speed = 3f;
    [SerializeField] GameObject ghost;
    public GameObject bullet;
    public Rigidbody2D rb;
    public Dictionary<string, GameObject> inventory = new();
    public int cutSceneIndex = 0;
    public Transform bulletPlace;
    public bool isCutScene = false;
    public double z;

    private Animator anim;
    private SpriteRenderer sprite;
    private bool needToFall = false;
    private CameraController mainCamera;
    private DeathClass death;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        bulletPlace = GameObject.FindWithTag("Bullet Start Place").GetComponent<Transform>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
    }

    private void Run()
    {
        State = States.walk;

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    public void addToInventiry([SerializeField] GameObject InventoryObject)
    {
        inventory[InventoryObject.name] = InventoryObject;
    }

    public void delFromInventiry([SerializeField] GameObject InventoryObject)
    {
        inventory.Remove(InventoryObject.name);
    }

    void Update()
    {
        State = States.idle;
        anim = gameObject.GetComponent<Animator>();

        if (!isCutScene && Input.GetButton("Horizontal"))
            Run();

        if (needToFall)
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(0, 0, 90), 10f * Time.deltaTime);


        if (Math.Round(gameObject.transform.rotation.eulerAngles.z, 1) == 90)
        {
            needToFall = false;
            Death();
        }
        
        CheckToCutScene();
    }

    private void CheckToCutScene()
    {
        switch (cutSceneIndex)
        {
            case 0:
                death = death != null ? death : GameObject.FindWithTag("Chandelier").GetComponent<ChandelierDeath>();
                break;
            case 1:
                death = death != null ? death : GameObject.FindWithTag("Mirror").GetComponent<MirrorDeath>();
                break;
        }

        if (death != null && Input.GetKeyUp(KeyCode.F) && death.ReadyToDeath())
            death.StartDeath();
    }

    public void EndCutScene()
    {
        isCutScene = false;
        cutSceneIndex++;
        death = null;
    }

    public void FallOnBack()
    {
        needToFall = true;
    }

    public void DeadlyScare()
    {

    }

    public void Death()
    {
        gameObject.SetActive(false);
        Invoke(nameof(Respawn), 3);
        mainCamera.ChangeAimToPlayer();
    }

    public void Respawn()
    {
        var rot = gameObject.transform.rotation;
        rot.z = 0;
        gameObject.transform.rotation = rot;
        gameObject.SetActive(true);
    }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    //private void CheckGround()
    //{
    //    Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
    //    isGrounded = collider.Length > 1;
    //}

    //private void Jump()
    //{
    //    rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    //}
}

public enum States {
    idle,
    walk
}
