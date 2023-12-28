using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    [SerializeField] private Sprite nonShadowSprite;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void ChangeSprite()
    {
        image.sprite = nonShadowSprite;
    }
}
