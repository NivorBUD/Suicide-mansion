using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LianaHead : MonoBehaviour
{
    [SerializeField] public LianaTrigger trigger;
    [SerializeField] public GameObject lianaHead;
    [SerializeField] private GameObject lianaEnd;
    
    private float xHeadVelocity;
    private float yHeadVelocity;
    private float xEndVelocity;
    private Rigidbody2D rbHead;
    private Rigidbody2D rbEnd;
    private bool needToReturn;
    private bool needToUp;

    void Start()
    {
        xHeadVelocity = 0;
        yHeadVelocity = 0;
        xEndVelocity = 0;
        rbHead = lianaHead.GetComponent<Rigidbody2D>();
        rbEnd = lianaEnd.GetComponent<Rigidbody2D>();
        needToReturn = false;
    }

    void Update()
    {
        if (rbHead.bodyType == RigidbodyType2D.Dynamic)
            rbHead.velocity = new Vector3(xHeadVelocity, yHeadVelocity, 0);
        
        if (rbEnd.bodyType == RigidbodyType2D.Dynamic)
            rbEnd.velocity = new Vector3(xEndVelocity, 0, 0);
        
        if (!needToUp && !needToReturn && trigger.isTriggered) 
        {
            rbEnd.bodyType = RigidbodyType2D.Static;
            xHeadVelocity = 0;
            yHeadVelocity = 0;
        }

        if (needToUp && trigger.isTriggered)
        {
            rbEnd.bodyType = RigidbodyType2D.Static;
            rbHead.bodyType = RigidbodyType2D.Static;
            xHeadVelocity = 0;
            yHeadVelocity = 0;
        }
    }

    public void StartMove()
    {
        xHeadVelocity = 2;
    }

    public void StartReverseMove()
    {
        xEndVelocity = -6;
        needToReturn = true;
        rbEnd.bodyType = RigidbodyType2D.Dynamic;
        rbHead.bodyType = RigidbodyType2D.Dynamic;
    }

    public void MoveUp()
    {
        needToUp = true;
        xHeadVelocity = 3;
        yHeadVelocity = 5;
        rbEnd.bodyType = RigidbodyType2D.Dynamic;
    }
}
