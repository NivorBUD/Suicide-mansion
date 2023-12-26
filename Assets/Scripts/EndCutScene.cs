using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EndCutScene : MonoBehaviour
{
    public Sprite[] danceImages;
    public Image blackOutRenderer, danceRenderer;
    public GameObject titlesObject;

    private int imageIndex = 0;
    private bool needToShow;

    void Update()
    {
        if (needToShow)
        {
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
    }

    private void ChangeImage()
    {
        imageIndex = (imageIndex + 1) % danceImages.Length;
        danceRenderer.sprite = danceImages[imageIndex];
        Invoke(nameof(ChangeImage), 0.1f);
    }
}
