using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Add this to use UI elements like Text

public class PlayerController : MonoBehaviour
{

    [SerializeField] Animator animator;
    public float speed = 4.0f;
    private Rigidbody2D rb;
    private Interactable interactable;
    private SpriteRenderer spriteRenderer;

    // Add these variables to track the number of clues found and the victory text
    private int cluesFound = 0;
    public TextMeshPro victoryText;
    public GameObject victoryPanel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        victoryPanel.SetActive(false); // Hide the victory panel at the start
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * speed;

        if (moveHorizontal < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveHorizontal > 0)
        {
            spriteRenderer.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && interactable != null)
        {
            if (interactable.Interact()) // Check if the interaction was successful
            {
                cluesFound++; // Increase the clue count
                CheckWinCondition(); // Check if the player has won
            }
        }

        animator.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("SpeedY", rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            interactable = collision.GetComponent<Interactable>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            interactable = null;
        }
    }

    private void CheckWinCondition()
    {
        if (cluesFound >= 3) // If the player has found 3 clues
        {
            StartCoroutine(DisplayVictoryAndExit()); // Display victory message and exit the game
        }
    }

    private IEnumerator DisplayVictoryAndExit()
    {
        victoryPanel.SetActive(true); // Show the victory panel
        victoryText.text = "You found all the clues! You win!";
        yield return new WaitForSeconds(5); // Wait for 5 seconds before closing the game

        // Close the game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
