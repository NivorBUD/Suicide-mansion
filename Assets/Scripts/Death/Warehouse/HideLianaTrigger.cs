using UnityEngine;

public class HideLianaTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var render = collision.gameObject.GetComponent<SpriteRenderer>();
        if (collision.CompareTag("Liana") || collision.CompareTag("Liana part"))
            render.enabled = false;
    }
}
