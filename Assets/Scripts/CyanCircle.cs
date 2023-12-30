using UnityEngine;
using UnityEngine.UI;

public class CyanCircle : MonoBehaviour
{
    public GameObject redCircle, navigationButton;
    public float previousUseTime;
    public Image image;

    private Hero playerScript;
    private float deltaTime;
    private bool isOn, needToStopBlink;

    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Hero>();
    }

    void Update()
    {
        if (!navigationButton.activeSelf)
            return;

        deltaTime = Time.time - previousUseTime;
        if (deltaTime > 15 && !isOn)
            TurnOn();

        if (deltaTime < 15 && isOn)
            TurnOff();
    }

    private void TurnOn()
    {
        isOn = true;
        image.color = new Color32(255, 255, 255, 255);
        needToStopBlink = false;
        StartBlink();
    }

    public void TurnOff()
    {
        isOn = false;
        image.color = new Color32(255, 255, 255, 0);
        StopBlink();
    }

    private void StartBlink()
    {
        if (needToStopBlink)
            return;

        if (image.color.a == 1)
            image.color = new Color32(255, 255, 255, 0);
        else
            image.color = new Color32(255, 255, 255, 255);
        
        Invoke(nameof(StartBlink), 0.7f);
    }

    private void StopBlink()
    {
        needToStopBlink = true;
    }
}
