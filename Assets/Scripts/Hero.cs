using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] GameObject ghost;
    private bool isGrounded = false;

    public Rigidbody2D rb;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
    }

    void Update()
    {
        if (Input.GetButton("Horizontal"))
            Run();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    public void Death()
    {
        gameObject.SetActive(false);
        Invoke("Respawn", 3);
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
    }
}
