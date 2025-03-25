using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WordConnectManager : MonoBehaviour
{
    public TextMeshProUGUI formedWordText; // To display the word the player is forming
    public Button submitButton; // Submit button to check the word
    public Button[] letterTiles; // Array of letter tiles (buttons)
    public string correctWord; // The correct word for the current round

    private List<string> selectedLetters = new List<string>(); // List to store selected letters
    private Color defaultTextColor; // Store the default color of the formedWordText

    void Start()
    {
        // Initialize buttons and set up event listeners
        foreach (Button letterTile in letterTiles)
        {
            letterTile.onClick.AddListener(() => OnLetterTileClicked(letterTile));
        }

        submitButton.onClick.AddListener(OnSubmitWord);

        // Store the default color of the text
        defaultTextColor = formedWordText.color;

        ResetWord();
    }

    // Called when a letter tile is clicked
    void OnLetterTileClicked(Button clickedTile)
    {
        string letter = clickedTile.GetComponentInChildren<TextMeshProUGUI>().text;

        // Add letter to the list and update the formed word
        selectedLetters.Add(letter);
        UpdateFormedWordText();
    }

    // Update the text that shows the formed word
    void UpdateFormedWordText()
    {
        formedWordText.text = string.Join("", selectedLetters); // Concatenate selected letters
    }

    // Called when the player submits the word
    void OnSubmitWord()
    {
        string formedWord = string.Join("", selectedLetters);

        if (formedWord == correctWord)
        {
            Debug.Log("Correct! You formed the word: " + correctWord);
            formedWordText.color = Color.green; // Change text color to green for correct word
            // Provide success feedback
        }
        else
        {
            Debug.Log("Incorrect word.");
            formedWordText.color = Color.red; // Change text color to red for incorrect word
            // Provide failure feedback
        }

        // Reset the word after 2 seconds (allow the player to see the feedback)
        StartCoroutine(ResetWordAfterDelay(2f));
    }

    // Reset the word formation process and revert the text color
    IEnumerator ResetWordAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reset the word and text color
        ResetWord();
        formedWordText.color = defaultTextColor; // Revert to default color
    }

    void ResetWord()
    {
        selectedLetters.Clear();
        UpdateFormedWordText();
    }
}


