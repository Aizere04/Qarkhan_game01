using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Текст диалога
    public Button nextButton; // Кнопка "Next"
    public Button startQuizButton; // Кнопка "Start Quiz"
    public Image backgroundImage; // Картинка фона
    public Sprite[] backgrounds; // Массив фонов
    private Queue<string> dialogueQueue; // Очередь для диалогов
    private int sceneIndex = 0;

    public Canvas quizCanvas; // Canvas для квиза
    public Canvas dialogueCanvas; // Canvas для диалога
    public QuizManager quizManager; // Ссылка на менеджер квиза

    void Start()
    {
        dialogueQueue = new Queue<string>();
        nextButton.gameObject.SetActive(false);
        startQuizButton.gameObject.SetActive(false); // Скрыть кнопку "Start Quiz" в начале
        quizCanvas.gameObject.SetActive(false); // Скрыть квизовый Canvas в начале
        dialogueCanvas.gameObject.SetActive(true); // Показать Canvas диалога
        StartScene1();
    }

    public void StartScene1()
    {
        backgroundImage.sprite = backgrounds[0]; // Установить фон библиотеки
        dialogueQueue.Clear();

        // Добавить строки для сцены 1
        dialogueQueue.Enqueue("Бұл кітап ескі сияқты... Қызық, бұл не туралы?");
        dialogueQueue.Enqueue("Сіз кітапты ашсаңыз, кенеттен айналаңыздағы әлем айнала бастайды. Қараңғы түседі...");

        DisplayNextSentence();
    }

    public void StartScene2()
    {
        backgroundImage.sprite = backgrounds[1]; // Установить фон древнего Казахстана
        dialogueQueue.Clear();

        // Добавить строки для сцены 2
        dialogueQueue.Enqueue("Сен! Мен сенің бұл елден емес екеніңді білемін. Сені мұнда не әкелді?");
        dialogueQueue.Enqueue("Мен... білмеймін! Мен кітап оқып едім, кенеттен енді осында пайда болдым...");
        dialogueQueue.Enqueue("Сен маған шайқасқа дайындалуыма көмектесу керексің. Бірақ алдымен біліміңді дәлелдеу керек. Осы сұрақтарға жауап бер, мен сенің даналығыңа сене аламын ба?");
        dialogueQueue.Enqueue("END_DIALOGUE"); // Специальный маркер для квиза

        DisplayNextSentence();
    }

    // Отображение следующего предложения
    public void DisplayNextSentence()
    {
        if (dialogueQueue.Count == 0)
        {
            if (sceneIndex == 0)
            {
                sceneIndex++;
                StartScene2(); // Переход к сцене 2
            }
            else
            {
                EndDialogue(); // Завершить диалог
            }
            return;
        }

        string sentence = dialogueQueue.Dequeue();

        if (sentence == "END_DIALOGUE")
        {
            ShowStartQuizButton(); // Показать кнопку "Start Quiz"
            return;
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        nextButton.gameObject.SetActive(true); // Показать кнопку "Next"
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void OnNextButtonClick()
    {
        nextButton.gameObject.SetActive(false); // Скрыть кнопку "Next"
        DisplayNextSentence();
    }

    public void EndDialogue()
    {
        Debug.Log("End of dialogue");
    }

    // Показать кнопку "Start Quiz"
    public void ShowStartQuizButton()
    {
        nextButton.gameObject.SetActive(false); // Скрыть кнопку "Next"
        startQuizButton.gameObject.SetActive(true); // Показать кнопку "Start Quiz"
    }

    // Переход на Canvas с квизом
    public void OnStartQuizButtonClick()
    {
        dialogueCanvas.gameObject.SetActive(false); // Скрыть Canvas диалога
        quizCanvas.gameObject.SetActive(true); // Показать Canvas квиза
        quizManager.StartQuiz(); // Запустить квиз
    }

    // Этот метод вызывается после завершения квиза
    public void OnQuizComplete()
    {
        // Логика после завершения квиза
        Debug.Log("Quiz complete. Returning to dialogue with new scene.");

        // Добавляем новый диалог после успешного прохождения квиза
        dialogueQueue.Clear();
        dialogueQueue.Enqueue("Абылай Хан:" +
            "Сен өзіңді дәлелдедің! Мен саған сенемін. Алда болатын нәрсеге дайындалайық.");

        // Изменяем фон после успешного прохождения квиза
        backgroundImage.sprite = backgrounds[2]; // Здесь вы можете заменить на нужный фон

        // Возвращаемся к диалоговому Canvas
        dialogueCanvas.gameObject.SetActive(true); // Показать Canvas диалога
        quizCanvas.gameObject.SetActive(false); // Скрыть Canvas квиза

        // Показать следующий диалог
        DisplayNextSentence();
    }
}
