using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targets[targetindex], 50f * Time.deltaTime);
        
        if (gameObject.transform.position == targets[targetindex])
        {
            if (targetindex == targets.Length - 1)
            {
                chandelier_interaction.Fall();
                Destroy(gameObject);
            }
            targetindex++;
        }
    }
}
