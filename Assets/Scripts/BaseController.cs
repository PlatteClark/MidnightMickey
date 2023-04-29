#nullable enable

using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class BaseController : MonoBehaviour
{
    [SerializeField] private Interactable interactable;
    [SerializeField] private Animator? animator = null;
    [SerializeField] private AudioSource? audioSource = null;
    [SerializeField] private Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();

    [SerializeField] private List<string> messages = new List<string>();
    [SerializeField] private GameObject picture;
    [SerializeField] private TextMeshPro messageText;

    private int currentMessageIndex = 0;
    [SerializeField] private AudioClip[] soundClips;

    private void Start()
    {
        interactable.OnInteract += OnInteract;
        interactable.OnPlayerLeftTriggerArea += OnPlayerLeftTriggerArea;
        messageText.text = "";

        for (int i = 0; i < soundClips.Length; i++)
        {
            sounds[soundClips[i].name] = soundClips[i];
        }
    }

    private void OnDestroy()
    {
        interactable.OnInteract -= OnInteract;
        interactable.OnPlayerLeftTriggerArea -= OnPlayerLeftTriggerArea;
    }

    private void OnInteract(string soundName)
    {
        //soundName = "CopTalk1";
        animator?.SetTrigger("PlayAnimation");
        if (sounds.Count > 0)
        {
            if (string.IsNullOrEmpty(soundName))
            {
                // Play a random sound
                int randomIndex = Random.Range(0, sounds.Count);
                audioSource.PlayOneShot(sounds.ElementAt(randomIndex).Value);
            }
            else if (sounds.ContainsKey(soundName))
            {
                // Play the named sound
                audioSource.PlayOneShot(sounds[soundName]);
            }
        }
        if (messages.Count > 0)
        {
            messageText.text = messages[currentMessageIndex];
            currentMessageIndex = (currentMessageIndex + 1) % messages.Count;
        }
        picture.SetActive(true);
    }


    private void OnPlayerLeftTriggerArea()
    {
        messageText.text = "";
        picture.SetActive(false);
        currentMessageIndex = 0;
    }
}
