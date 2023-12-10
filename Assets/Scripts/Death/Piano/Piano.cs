using System;
using UnityEngine;

public class Piano : MonoBehaviour
{
    public BrokenDoorInteraction door;
    public GameObject key;
    public PianoDeath deathScript;
    public bool isEnd;
    public BoxCollider2D floorCollider;

    private Hero playerScript;
    private GameObject player;
    private Rigidbody2D rb;
    private PolygonCollider2D bc;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc = gameObject.GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        if (transform.localPosition.x <= -10.5f && transform.localPosition.x > -11f)
        {
            var sc = player.transform.localScale;
            sc.x = Math.Abs(-10.5f - transform.localPosition.x);
            player.transform.localScale = sc;
        }

        if (!door.isBroke && transform.localPosition.x <= -11)
        {
            BreakDoor();
        }

        if (playerScript.isCutScene && Math.Round(player.transform.rotation.eulerAngles.z, 1) == 90 && playerScript.rb.velocity.x == 0)
        {
            playerScript.Death();
            Invoke(nameof(TurnOnBlackOut), 3.5f);
        }
            

        if (!isEnd && transform.localPosition.x < -11 && rb.velocity.x == 0)
        {
            EndDeath();
        }
    }

    private void BreakDoor()
    {
        var speed = rb.velocity;
        speed.x /= 2;
        rb.velocity = speed;

        door.Break();

        playerScript.rb.AddForce(new Vector2(-10, 5), ForceMode2D.Impulse);
        playerScript.rb.freezeRotation = false;
        playerScript.isCutScene = true;
        deathScript.blackOut.SetActive(false);
    }

    private void EndDeath()
    {
        var keypos = transform.position;
        keypos.z = 1;
        transform.position = keypos;

        keypos.z = -1;
        keypos.y = -4f;
        floorCollider.enabled = true;
        key.transform.position = keypos;
        key.SetActive(true);
        
        isEnd = true;
        rb.simulated = false;
        bc.enabled = false;
        deathScript.col.enabled = false;
    }

    private void TurnOnBlackOut()
    {
        deathScript.blackOut.SetActive(true);
    }
}
