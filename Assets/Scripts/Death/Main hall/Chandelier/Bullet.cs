using System;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isStart = false;
    public int targetindex = 0;

    private float speed;
    private Vector3[] targets;
    private ChandelierInteraction chandelierInteraction;
    

    void Start()
    {
        speed = 15;
        chandelierInteraction = GameObject.FindWithTag("Chandelier").GetComponent<ChandelierInteraction>();
        targets = new Vector3[chandelierInteraction.targets.Length];
        for (int i = 0; i < chandelierInteraction.targets.Length; i++)
            targets[i] = chandelierInteraction.targets[i].transform.position;
    }

    private void Update()
    {
        if (isStart) {
            var deltaY = gameObject.transform.position.y - targets[targetindex].y;
            var deltaX = gameObject.transform.position.x - targets[targetindex].x;
            float angle = (float)(Math.Atan2(deltaY, deltaX) * 180 / Math.PI) + 90;
            gameObject.transform.SetPositionAndRotation(Vector3.MoveTowards(gameObject.transform.position, targets[targetindex], speed * Time.deltaTime), Quaternion.Euler(0, 0, angle));

            if (gameObject.transform.position == targets[targetindex])
            {
                PlayRicochetSound(); // звук рикошета
                if (targetindex == targets.Length - 1)
                {
                    chandelierInteraction.Fall();
                    Destroy(gameObject);
                    GameObject.FindWithTag("MainCamera").GetComponent<CameraController>().ChangeAim(GameObject.FindWithTag("Chandelier").transform);
                }
                targetindex++;
            }
        }
    }

    private void PlayRicochetSound()
    {

    }
}
