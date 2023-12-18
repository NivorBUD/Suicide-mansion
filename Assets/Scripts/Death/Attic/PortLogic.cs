using UnityEngine;

public class PortLogic : MonoBehaviour
{
    [SerializeField] private ElectricMinigameLogic gameLogic;
    
    private void OnMouseDown()
    {
        if (gameLogic.activeCable.name.Split(" ")[1] == name.Split(" ")[1])
        {
            gameLogic.activeCable.isReady = true;
            gameLogic.activeCable = null;
            gameLogic.readyCables++;
        }
        else
        {
            gameLogic.activeCable = null;
        }
    }
}
