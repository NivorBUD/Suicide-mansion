using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public Transform hitPosition;
    public bool isReady;

    [SerializeField] private ParticleSystem rain;

    private bool needToMove = false;

    void Update()
    {
        if (needToMove)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, hitPosition.position, 3 * Time.deltaTime);
            if (gameObject.transform.position == hitPosition.position)
            {
                needToMove = false;
                isReady = true;
            }
        }
    }

    public void Move()
    {
        needToMove = true;
    }

    public void StartRain()
    {
        rain.Play();
    }
    
    public void StopRain()
    {
        rain.Stop();
    }

    public void Lightning()
    {

    }
}
