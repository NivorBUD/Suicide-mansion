using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public Transform position1;
    public Transform position2;
    public Bullet bullet;

    private bool needToGoToPosition1 = true;
    private Hero playerScript;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
    }

    void Update()
    {
        if (bullet.targetindex >= 1 || playerScript.levelComplete >= 3)
            Destroy(gameObject);

        if (needToGoToPosition1)
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, position1.position, Time.deltaTime);
        else
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, position2.position, Time.deltaTime);

        if (gameObject.transform.position == position1.position)
            needToGoToPosition1 = false;

        if (gameObject.transform.position == position2.position)
            needToGoToPosition1 = true;
    }
}
