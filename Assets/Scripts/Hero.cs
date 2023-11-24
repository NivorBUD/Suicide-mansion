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


    private SpriteRenderer sprite;
    private bool isInShootPlace = false;
    private Transform bulletPlace;
    private bool isShoot = false;
    private bool needToFall = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        bulletPlace = GameObject.FindWithTag("Bullet Start Place").GetComponent<Transform>();
    }

    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    private void Shoot()
    {
        isShoot = true;
        Instantiate(bullet, bulletPlace.position, Quaternion.identity);
    }

    public void EnterShootPlace()
    {
        isInShootPlace = true;
    }

    public void ExitShootPlace()
    {
        isInShootPlace = false;
    }

    

    void Update()
    {
        if (Input.GetButton("Horizontal"))
            Run();
        if (!isShoot && isInShootPlace && Input.GetKeyUp(KeyCode.F))
            Shoot();

        if (needToFall)
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(0, 0, 90), 10f * Time.deltaTime);
           
        if(isActiveAndEnabled && gameObject.transform.rotation.eulerAngles.z >= 89)
        {
            needToFall = false;
            Death();
            var rot = gameObject.transform.rotation;
            rot.z = 0;
            gameObject.transform.rotation = rot;
        }
    }

    public void FallOnBack()
    {
        needToFall = true;
    }

    public void Death()
    {
        gameObject.SetActive(false);
        Invoke(nameof(Respawn), 3);
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
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
