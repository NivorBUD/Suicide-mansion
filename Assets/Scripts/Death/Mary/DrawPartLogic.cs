using UnityEngine;

public class DrawPartLogic : MonoBehaviour
{
    private SpriteRenderer sr;

    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnMouseEnter()
    {
        if (DrawingLogic.canDraw && sr.color != Color.red)
        {
            sr.color = Color.red;
            DrawingLogic.paintedPartsCount++;
        }
    }

    //private void OnMouseDown()
    //{
    //    if (DrawingLogic.canDraw && sr.color != Color.red)
    //    {
    //        sr.color = Color.red;
    //        DrawingLogic.paintedPartsCount++;
    //    }
    //}
}
