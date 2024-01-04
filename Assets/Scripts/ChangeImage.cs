using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    [SerializeField] public Sprite nonShadowSprite;
    [SerializeField] private GameObject mainRedCircle, individualRedCircle;
    public Image image;
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
