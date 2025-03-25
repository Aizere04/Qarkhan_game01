using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BeshbarmakCookingManager : MonoBehaviour
{
    public TextMeshProUGUI recipeText; // Текст рецепта
    public Image dishImage; // Изображение блюда на этапе приготовления
    public Button[] ingredientButtons; // Кнопки с ингредиентами
    public Sprite[] dishStages; // Этапы приготовления блюда

    private int currentStep = 0; // Текущий шаг рецепта
    private string[] recipeSteps = new string[]
    {
        "Ұн қосыңыз.", // Добавьте муку
        "Су құйыңыз.", // Налейте воду
        "Жұмыртқа қосыңыз.", // Добавьте яйцо
        "Тұз қосыңыз.", // Добавьте соль
        "Етті қайнатыңыз." // Отварите мясо
    };

    private string[] correctIngredients = new string[]
    {
        "Ұн", // Мука
        "Су", // Вода
        "Жұмыртқа", // Яйцо
        "Тұз", // Соль
        "Ет"  // Мясо
    };

    void Start()
    {
        // Инициализация игры с первого шага
        UpdateRecipeStep();

        foreach (Button ingredientButton in ingredientButtons)
        {
            ingredientButton.onClick.AddListener(() => OnIngredientSelected(ingredientButton));
        }
    }

    // Обновление текста рецепта и изображения блюда
    void UpdateRecipeStep()
    {
        if (currentStep < recipeSteps.Length)
        {
            recipeText.text = recipeSteps[currentStep]; // Показать следующий шаг рецепта
        }
        else
        {
            recipeText.text = "Бешбармак дайын болды!"; // "Бешбармак готов!"
        }
    }

    // Проверка выбранного ингредиента
    void OnIngredientSelected(Button clickedButton)
    {
        string selectedIngredient = clickedButton.GetComponentInChildren<TextMeshProUGUI>().text;

        // Если ингредиент правильный
        if (selectedIngredient == correctIngredients[currentStep])
        {
            Debug.Log("Дұрыс! (" + selectedIngredient + ")");
            UpdateDishImage(); // Обновить изображение блюда
            currentStep++; // Переход к следующему шагу
            if (currentStep < recipeSteps.Length)
            {
                UpdateRecipeStep(); // Обновить шаг рецепта
            }
            else
            {
                FinishCooking(); // Завершение приготовления
            }
        }
        else
        {
            Debug.Log("Қате! Қайта көріңіз."); // Неправильный выбор, игрок должен попробовать снова
            StartCoroutine(ShowWrongChoiceFeedback(clickedButton));
        }
    }

    // Обновление изображения блюда на каждом этапе
    void UpdateDishImage()
    {
        if (currentStep < dishStages.Length)
        {
            dishImage.sprite = dishStages[currentStep]; // Обновить изображение блюда
        }
    }

    // Завершение приготовления
    void FinishCooking()
    {
        recipeText.text = "Құттықтаймыз! Бешбармак дайын."; // "Поздравляем! Бешбармак готов."
    }

    // Обратная связь при ошибочном выборе ингредиента
    IEnumerator ShowWrongChoiceFeedback(Button clickedButton)
    {
        Color originalColor = clickedButton.GetComponent<Image>().color;
        clickedButton.GetComponent<Image>().color = Color.red; // Меняем цвет кнопки на красный
        yield return new WaitForSeconds(1f); // Ждем 1 секунду
        clickedButton.GetComponent<Image>().color = originalColor; // Возвращаем исходный цвет
    }
}
