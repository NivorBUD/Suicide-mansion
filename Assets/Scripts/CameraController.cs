using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform aim;
    private Vector3 pos;
    private float speed = 1.0f;
    private bool isAimPlayer = true;
    private Camera camera;
    private float newCamSize;
    private bool needToZoom;

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
        ZoomIn(5);
    }

    public void ZoomIn(float size)
    {
        needToZoom = true;
        newCamSize = size;
    }

    private void Awake()
    {
        if (!aim)
            aim = FindObjectOfType<Hero>().transform;
        camera = gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        if (needToZoom)
            camera.orthographicSize = Mathf.MoveTowards(camera.orthographicSize, newCamSize, 5.0f * Time.deltaTime);

        if (!aim)
            ChangeAimToPlayer();
        pos = aim.position;
        
        if (isAimPlayer) 
            pos.y += 2.4f;
        pos.z = -10f;

        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
    }
}
