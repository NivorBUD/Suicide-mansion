using System;
using UnityEngine;

public class Desk : MonoBehaviour
{
    public GameObject getPlace, holdingPlace, dropPlace, helper1, helper2;
    public bool isReady, isInstall;
    public Rigidbody2D rb;

    private ConstantForce2D force;
    private bool needToMove, needToDrop;
    private Hero playerScript;
    

    void Start()
    {
        force = gameObject.GetComponent<ConstantForce2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
    }

    void Update()
    {
        if (playerScript.levelComplete >= 7 && !isInstall)
        {
            transform.localPosition = new Vector3(-0.168f, -3.66f, 0);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(8.85f, 0.15f, 1f);
            GetComponent<BoxCollider2D>().enabled = true;
            rb.simulated = true;
            rb.bodyType = RigidbodyType2D.Static;
            isInstall = true;
        }

        if (!isReady && needToMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, holdingPlace.transform.position, Time.deltaTime);
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(8.85f, 0.15f, 1f), 4 * Time.deltaTime);
        }

        if (needToMove && transform.position == holdingPlace.transform.position && transform.localScale.x == 8.85f)
        {
            isReady = true;
            needToMove = false;
        }

        if (!isReady && needToDrop)
            transform.position = Vector3.MoveTowards(transform.position, dropPlace.transform.position, Time.deltaTime);

        if (Math.Abs(transform.rotation.eulerAngles.z) <= 0.5f && Math.Abs(rb.velocity.x) < 0.1f)
        {
            isInstall = true;
            Destroy(helper1);
            Destroy(helper2);
        }
            

        if (transform.position == dropPlace.transform.position)
        {
            needToDrop = false;
            GetComponent<BoxCollider2D>().enabled = true;
            rb.simulated = true;
            force.force = new Vector2(2, 0);
        }
    }

    public void GetAndMoveToHand()
    {
        helper1.SetActive(true);
        helper2.SetActive(true);
        isInstall = false;
        isReady = false;
        gameObject.SetActive(true);
        transform.position = getPlace.transform.position;
        needToMove = true;
    }

    public void SetAndDrop()
    {
        isReady = false;
        needToDrop = true;
    }
}
