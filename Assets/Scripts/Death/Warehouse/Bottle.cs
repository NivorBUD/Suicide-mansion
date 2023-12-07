using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    [SerializeField] private GameObject[] balls = new GameObject[3];
    [SerializeField] private GameLogic gameLogic;
    private bool isReady;

    private void OnMouseDown()
    {
        if (isReady)
            return;
        if (gameLogic.activeBall.activeSelf)
            SetBall();
        else
            gameLogic.SetActiveBall(GetBall());
    }

    private Color GetBall()
    {
        for (int i = 0; i < balls.Length; i++)
            if (balls[i].activeSelf)
            {
                balls[i].SetActive(false);
                return balls[i].GetComponent<SpriteRenderer>().color;
            }
                
        return Color.black;
    }

    private void SetBall()
    {
        for (int i = balls.Length - 1; i > -1; i--)
            if (!balls[i].activeSelf)
            {
                balls[i].SetActive(true);
                balls[i].GetComponent<SpriteRenderer>().color = gameLogic.activeBallRenderer.color;
                gameLogic.activeBall.SetActive(false);
                checkReady();
                return;
            }
    }

    private void checkReady()
    {
        Color color = Color.cyan;
        for (int i = 0; i < balls.Length; i++)
            if (balls[i].activeSelf)
            {
                color = balls[i].GetComponent<SpriteRenderer>().color;
                break;
            }

        for (int i = 0; i < balls.Length; i++)
            if (!balls[i].activeSelf || balls[i].GetComponent<SpriteRenderer>().color != color)
                return;
        isReady = true;
        gameLogic.ReadyBottle();
    }
}
