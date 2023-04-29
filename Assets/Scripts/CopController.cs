using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class CopController : MonoBehaviour
{
    [SerializeField] private Interactable interactable;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator? animator = null;
    [SerializeField] private AudioSource? audioSource = null;
    [SerializeField] private Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();
    [SerializeField] private AudioClip[] soundClips;
    [SerializeField] private List<string> messages = new List<string>();
    [SerializeField] private GameObject picture;
    [SerializeField] private TextMeshPro messageText;

    // Add these variables for waypoint functionality
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private float moveSpeed = 2f;

    private int currentWaypointIndex = 0;
    private int currentMessageIndex = 0;
    

    private void Start()
    {
        interactable.OnInteract += OnInteract;
        interactable.OnPlayerLeftTriggerArea += OnPlayerLeftTriggerArea;
        messageText.text = "";

        for (int i = 0; i < soundClips.Length; i++)
        {
            sounds[soundClips[i].name] = soundClips[i];
        }

        // Start the patrol Coroutine if there are waypoints
        if (waypoints.Count > 0)
        {
            StartCoroutine(Patrol());
        }
    }

    private void Update()
    {
        Vector2 targetPosition = waypoints[currentWaypointIndex].position;
        Vector2 currentPosition = transform.position;
        Vector2 direction = targetPosition - currentPosition;
     

        // Flip the SpriteRenderer based on the movement direction
        if (spriteRenderer != null)
        {
            if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (direction.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }


    // Add this Coroutine for moving the cop between waypoints
    private IEnumerator Patrol()
    {
        while (true)
        {
            // Move towards the current waypoint
            Vector2 targetPosition = waypoints[currentWaypointIndex].position;
            Vector2 currentPosition = transform.position;
            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);

            // Update the Animator
            if (animator != null)
            {
                Vector2 direction = targetPosition - currentPosition;
                animator.SetFloat("Speed", Mathf.Abs(direction.x));
            }

            // Check if the cop has reached the current waypoint
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                // Move to the next waypoint
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
                yield return new WaitForSeconds(1f); // Wait for 1 second before moving to the next waypoint
            }

            yield return null;
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
