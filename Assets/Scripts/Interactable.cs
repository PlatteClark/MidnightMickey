#nullable enable

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider2D))]
public class Interactable : MonoBehaviour
{
    public GameObject interactionPrompt;
    public bool isClue;
    private bool isFound = false;

    public delegate void InteractEvent(string soundName);
    public event InteractEvent OnInteract;

    public delegate void PlayerLeftTriggerAreaEvent();
    public event PlayerLeftTriggerAreaEvent OnPlayerLeftTriggerArea;

    public bool Interact(string soundName = "")
    {
        Debug.Log("Interacted with the object!");
        OnInteract?.Invoke(soundName);

        if (!isFound)
        {
            isFound = true;
            return isClue; 
        }
        else
        {
            return false;
        }
    }


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
            OnPlayerLeftTriggerArea?.Invoke();
        }
    }
}
