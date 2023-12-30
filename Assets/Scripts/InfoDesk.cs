using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoDesk : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;
    private bool needToHide;

    public void Show(string text)
    {
        needToHide = false;
        this.text.text = "Вы подобрали: <color=#00FFF0>" + text;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        this.text.color = new Color(this.text.color.r, this.text.color.g, this.text.color.b, 1);
        Invoke(nameof(Hide), 0.75f);
    }

    public void Hide()
    {
        needToHide = true;
    }

    void Update()
    {
        if (needToHide)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 
                Mathf.MoveTowards(image.color.a, 0, 0.5f * Time.deltaTime));
            text.color = new Color(text.color.r, text.color.g, text.color.b,
                Mathf.MoveTowards(text.color.a, 0, 0.5f * Time.deltaTime));
            if (image.color.a == 0)
                needToHide = false;
        }
    }
}
