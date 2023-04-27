using UnityEngine;
using UnityEngine.UI;


public class Interactable : MonoBehaviour
{
    public GameObject interactionPrompt;
    [SerializeField] GameObject[] messages;
    [SerializeField] GameObject picture;

    private int interactionQueue = 0;

    private void Start()
    {
        interactionPrompt.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactionPrompt.SetActive(false);
            messages[interactionQueue -1].SetActive(false);
            picture.SetActive(false);
            interactionQueue = 0;
        }
    }

    public void Interact()
    {
        Debug.Log("Interacted with the object!");
        if(interactionQueue > 0) { messages[interactionQueue - 1].SetActive(false); }
        
        messages[interactionQueue].SetActive(true);
        picture.SetActive(true);
        interactionQueue++;
    }
}
