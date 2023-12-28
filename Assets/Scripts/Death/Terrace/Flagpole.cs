using UnityEngine;

public class Flagpole : MonoBehaviour
{
    private Trigger trigger;
    private Hero playerScript;
    private LightningDeath lightningDeath;

    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
        lightningDeath = GetComponent<LightningDeath>();
        trigger = GetComponent<Trigger>();
    }

    void Update()
    {
        if (playerScript.isAtTerrace && playerScript.inventory.ContainsKey("Rope") && 
            playerScript.inventory.ContainsKey("Pantaloons"))
            playerScript.ChangePointerAim(transform);

        if (trigger.isTriggered && Input.GetKeyUp(KeyCode.F) && 
            playerScript.inventory.ContainsKey("Rope") && playerScript.inventory.ContainsKey("Pantaloons"))
            lightningDeath.StartDeath();
    }
}
