using UnityEngine;

public class ShootPlaceLogic : MonoBehaviour
{
    private ButtonHint hint;
    private Hero playerScript;
    private SpriteRenderer sprite;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        sprite = GetComponent<SpriteRenderer>();
        hint = GetComponent<ButtonHint>();
    }

    private void Update()
    {
        if (playerScript.inventory.ContainsKey("Key") && playerScript.inventory.ContainsKey("Slingshot"))
        {
            hint.isOn = true;
            sprite.enabled = true;
        }
        else
        {
            hint.isOn = false;
            sprite.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChandelierDeath.EnterShootPlace();
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        ChandelierDeath.ExitShootPlace();
    }
}
