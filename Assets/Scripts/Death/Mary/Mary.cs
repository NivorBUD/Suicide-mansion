using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mary : MonoBehaviour
{
    public GameObject bathroomKey;


    private void GiveTheKey()
    {
        bathroomKey.SetActive(true);
    }

    public void StartDialog()
    {
        GiveTheKey();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
