using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingLogic : MonoBehaviour
{
    public static bool canDraw;
    public static int paintedPartsCount;
    // Start is called before the first frame update
    void Start()
    {
        canDraw = false;
    }

    public void StartDraw()
    {
        canDraw = true;
    }
}
