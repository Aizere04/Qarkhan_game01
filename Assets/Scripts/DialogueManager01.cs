using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // To work with TextMeshPro
using UnityEngine.UI;

public class DialogueManager01 : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Dialogue text box
    public Button nextButton; // Button to advance the dialogue

    private string[] sentences; // Array to hold the sentences
    private int currentSentenceIndex = 0; // To keep track of which sentence we're on

    void Start()
    {
        // Initialize the sentences
        sentences = new string[] {
            "Protagonist: \"This book looks old... I wonder what it's about?\"",
            "Narrator: \"You open the book and suddenly, the world around you begins to spin. Everything goes dark...\""
        };

        // Set the first sentence
        dialogueText.text = sentences[currentSentenceIndex];

        // Add a listener to the button to advance the dialogue
        nextButton.onClick.AddListener(DisplayNextSentence);
    }

    // Method to display the next sentence
    void DisplayNextSentence()
    {
        currentSentenceIndex++;

        if (currentSentenceIndex < sentences.Length)
        {
            dialogueText.text = sentences[currentSentenceIndex];
        }
        else
        {
            // If we reach the end, deactivate the button or load the next scene
            nextButton.gameObject.SetActive(false);
            Debug.Log("End of the scene. Transition to the next scene.");
        }
    }
}

