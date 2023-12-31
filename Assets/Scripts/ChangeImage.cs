using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    [SerializeField] private Sprite nonShadowSprite;
    [SerializeField] private GameObject mainRedCircle, individualRedCircle;
    private Image image;
    public bool isNew;

    public void ChangeSprite()
    {
        image = gameObject.GetComponent<Image>();
        image.sprite = nonShadowSprite;
        mainRedCircle.SetActive(true);
        isNew = true;
    }

    void Update()
    {
        individualRedCircle.SetActive(isNew);
    }
}
