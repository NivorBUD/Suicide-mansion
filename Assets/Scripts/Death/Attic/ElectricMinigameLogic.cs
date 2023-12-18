using UnityEngine;

public class ElectricMinigameLogic : MonoBehaviour
{
    public Shield shield;
    public CableLogic activeCable;
    public int readyCables;

    private void Update()
    {
        if (readyCables == 5)
            EndGame();
    }

    public void StartGame()
    {
        gameObject.SetActive(true);
    }

    private void EndGame()
    {
        shield.StartDeath();
        gameObject.SetActive(false);
    }
}
