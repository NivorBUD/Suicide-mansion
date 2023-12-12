using System;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isStart = false;

    private Vector3[] targets;
    private Chandelier_Interaction chandelierInteraction;
    private int targetindex = 0;
    

    void Start()
    {
        chandelierInteraction = GameObject.FindWithTag("Chandelier").GetComponent<Chandelier_Interaction>();
        targets = new Vector3[11];
        for (int i = 0; i < chandelierInteraction.targets.Length; i++)
            targets[i] = chandelierInteraction.targets[i].transform.position;
    }

    private void Update()
    {
        if (isStart) {
            var deltaY = gameObject.transform.position.y - targets[targetindex].y;
            var deltaX = gameObject.transform.position.x - targets[targetindex].x;
            float angle = (float)(Math.Atan2(deltaY, deltaX) * 180 / Math.PI) + 90;
            gameObject.transform.SetPositionAndRotation(Vector3.MoveTowards(gameObject.transform.position, targets[targetindex], 30f * Time.deltaTime), Quaternion.Euler(0, 0, angle));

            if (gameObject.transform.position == targets[targetindex])
            {
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
}
