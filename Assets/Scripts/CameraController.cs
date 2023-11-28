using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform aim;
    private Vector3 pos;
    private float speed = 1.0f;
    private bool isAimPlayer = true;
    public GameObject player;

    public void ChangeAim(Transform aim)
    {
        this.aim = aim;
        isAimPlayer = false;
        speed = 20.0f;
    }

    public void ChangeSpeed(float speed)
    {
        this.speed = speed;
    }

    public void ChangeAimToPlayer()
    {
        aim = player.transform;
        isAimPlayer = true;
        speed = 1.0f;
    }

    private void Awake()
    {
        if (!aim)
        {
            aim = FindObjectOfType<Hero>().transform;
        }
    }

    void Update()
    {
        if (!aim)
            ChangeAimToPlayer();
        pos = aim.position;
        if (isAimPlayer) 
            pos.y += 3f;
        pos.z = -10f;

        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
    }
}
