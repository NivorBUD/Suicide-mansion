using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    public AudioClip soundClip; 
    public float delay = 0f; 
    public int playCount = 0; 

    private int currentPlayCount = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            if (playCount == 0 || currentPlayCount < playCount)
            {
                Invoke(nameof(PlaySound), delay);
                currentPlayCount++;
            }
        }
    }

    private void PlaySound()
    {
        AudioSource.PlayClipAtPoint(soundClip, transform.position);

        if (playCount > 0 && currentPlayCount >= playCount)
        {
            Destroy(gameObject);
        }
    }
}