using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    private string correctAnswer;

    public DialogueManager dialogueManager; // Reference to DialogueManager

    private string[] quizQuestions = new string[]
    {
        "Қай жылдарда Қазақ хандығы құрылды",
        "Қазақтың қай атақты Ханы жоңғар шапқыншылығына қарсылық көрсетті?"
    };

    private string[,] quizAnswers = new string[,]
    {
        { "1980", "1650", "1450", "1540" },
        { "Абылай хан", "Кенесары хан", "Керей хан", "Тәуке хан" }
    };

    private string[] correctAnswers = new string[]
    {
        "1450",
        "Абылай хан"
    };

    private int currentQuestionIndex = 0;
    private Color defaultButtonColor;

    void Start()
    {
        defaultButtonColor = answerButtons[0].GetComponent<Image>().color;
    }

    public void StartQuiz()
    {
        currentQuestionIndex = 0;
        DisplayQuestion(); // Start by displaying the first question
    }

    public void DisplayQuestion()
    {
        ResetButtonColors();

        questionText.text = quizQuestions[currentQuestionIndex];

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = quizAnswers[currentQuestionIndex, i];
            answerButtons[i].gameObject.SetActive(true);
        }

        correctAnswer = correctAnswers[currentQuestionIndex];
    }

    public void OnAnswerSelected(int index)
    {
        string selectedAnswer = answerButtons[index].GetComponentInChildren<TextMeshProUGUI>().text;

        if (selectedAnswer == correctAnswer)
        {
            answerButtons[index].GetComponent<Image>().color = Color.green;
            StartCoroutine(DelayBeforeNextQuestion(1f));
        }
        else
        {
            answerButtons[index].GetComponent<Image>().color = Color.red;
        }
    }

    void ResetButtonColors()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponent<Image>().color = defaultButtonColor;
        }
    }

    IEnumerator DelayBeforeNextQuestion(float delay)
    {
        yield return new WaitForSeconds(delay);

        currentQuestionIndex++;

        if (currentQuestionIndex < quizQuestions.Length)
        {
            DisplayQuestion();
        }
        else
        {
            EndQuiz();
        }
    }

    void EndQuiz()
    {
        Debug.Log("Quiz Finished!");
        dialogueManager.OnQuizComplete(); // Уведомление DialogueManager о завершении квиза
    }
}

