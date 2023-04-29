using UnityEngine;

public class Door : MonoBehaviour
{

    public AudioSource audioSource;
    public bool canPlay = true;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (canPlay)
        {
            audioSource.Play();
            canPlay = false;
            Invoke("CanPlayAgain", 1f);
        }
    }

    void CanPlayAgain()
    {
        canPlay = true;
    }
}
