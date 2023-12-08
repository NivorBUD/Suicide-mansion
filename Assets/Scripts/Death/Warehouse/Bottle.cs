using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] items = new SpriteRenderer[3];
    [SerializeField] private GameLogic gameLogic;
    private bool isReady;
    public bool isClick;

    private void OnMouseDown()
    {
        isClick = !isClick;
        if (isReady)
            return;
        if (gameLogic.activeBallRenderer.sprite != null)
            SetBall();
        else
            gameLogic.SetActiveBall(GetBall());
    }

    private Sprite GetBall()
    {
        for (int i = 0; i < items.Length; i++)
            if (items[i].sprite != null)
            {
                var sprite = items[i].sprite;
                items[i].sprite = null;
                return sprite;
            }
                
        return null;
    }

    private void SetBall()
    {
        for (int i = items.Length - 1; i > -1; i--)
            if (items[i].sprite == null)
            {
                items[i].sprite = gameLogic.activeBallRenderer.sprite;
                gameLogic.activeBallRenderer.sprite = null;
                checkReady();
                return;
            }
    }

    private void checkReady()
    {
        Sprite sprite = null;
        for (int i = 0; i < items.Length; i++)
            if (items[i].sprite != null)
            {
                sprite = items[i].sprite;
                break;
            }

        if (sprite == null)
            return;

        for (int i = 0; i < items.Length; i++)
            if (items[i].sprite == null || items[i].sprite != sprite)
                return;
        isReady = true;
        gameLogic.ReadyBottle();
    }
}
