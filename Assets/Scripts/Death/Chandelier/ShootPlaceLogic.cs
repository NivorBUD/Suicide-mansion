using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlaceLogic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChandelierDeath.EnterShootPlace();
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        ChandelierDeath.ExitShootPlace();
    }
}
