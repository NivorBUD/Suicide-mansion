using System;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isStart = false;

    private Vector3[] targets;
    private Chandelier_Interaction chandelier_interaction;
    private int targetindex = 0;
    

    void Start()
    {
        chandelier_interaction = GameObject.FindWithTag("Chandelier").GetComponent<Chandelier_Interaction>();
        targets = new Vector3[12];
        for (int i = 0; i < chandelier_interaction.targets.Length; i++)
            targets[i] = chandelier_interaction.targets[i].transform.position;
    }

    private void Update()
    {
        if (isStart) {
            var deltaY = gameObject.transform.position.y - targets[targetindex].y;
            var deltaX = gameObject.transform.position.x - targets[targetindex].x;
            float angle = (float)(Math.Atan2(deltaY, deltaX) * 180 / Math.PI) + 90;
            gameObject.transform.SetPositionAndRotation(Vector3.MoveTowards(gameObject.transform.position, targets[targetindex], 20f * Time.deltaTime), Quaternion.Euler(0, 0, angle));

            if (gameObject.transform.position == targets[targetindex])
            {
                if (targetindex == targets.Length - 1)
                {
                    chandelier_interaction.Fall();
                    Destroy(gameObject);
                    GameObject.FindWithTag("MainCamera").GetComponent<CameraController>().ChangeAim(GameObject.FindWithTag("Chandelier").transform);
                }
                targetindex++;
            }
        }
    }
}
