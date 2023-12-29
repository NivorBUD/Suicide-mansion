using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EndCutScene : MonoBehaviour
{
    public Sprite[] danceImages;
    public Image blackOutRenderer, danceRenderer, achivmentButtonRenderer, exitButtonRenderer;
    public GameObject titlesObject, pauseButton, deathopedia;

    private int imageIndex = 0;
    private bool needToShow, isDeathopediaOpen;

    void Update()
    {
        if (needToShow)
        {
            pauseButton.SetActive(false);
            exitButtonRenderer.color = new Color(255, 255, 255, Mathf.MoveTowards(exitButtonRenderer.color.a, 1, 0.5f * Time.deltaTime));
            blackOutRenderer.color = new Color(0, 0, 0, Mathf.MoveTowards(blackOutRenderer.color.a, 1, 0.5f * Time.deltaTime));
            danceRenderer.color = new Color(255, 255, 255, Mathf.MoveTowards(danceRenderer.color.a, 1, 0.5f * Time.deltaTime));
            if (blackOutRenderer.color.a == 1)
                needToShow = false;
            titlesObject.SetActive(true);
        }
    }

    public void StartEndCutScene()
    {
        ChangeImage();
        needToShow = true;
        GameObject.FindWithTag("Player").GetComponent<Hero>().isCutScene = true;
        achivmentButtonRenderer.gameObject.SetActive(true);
        exitButtonRenderer.gameObject.SetActive(true);
    }

    private void ChangeImage()
    {
        imageIndex = (imageIndex + 1) % danceImages.Length;
        danceRenderer.sprite = danceImages[imageIndex];
        Invoke(nameof(ChangeImage), 0.1f);
    }

    public void OpenCloseDeathopedia()
    {
        if (!isDeathopediaOpen)
        {
            deathopedia.SetActive(true);
            isDeathopediaOpen = true;
        }
        else
        {
            deathopedia.SetActive(false);
            isDeathopediaOpen = false;
        }
    }
}
