using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CookingGameManager : MonoBehaviour
{
    public TextMeshProUGUI recipeText; // Текст рецепта на казахском
    public Image dishImage; // Изображение блюда, которое готовит игрок
    public Button[] ingredientButtons; // Массив кнопок с ингредиентами
    public Sprite[] dishStages; // Этапы приготовления блюда (изображения)

    private int currentStep = 0; // Текущий шаг рецепта
    private string[] recipeSteps = new string[]
    {
        "Алдымен ұн қосу керек.", // "Сначала нужно добавить муку."
        "Содан кейін су құйыңыз.", // "Затем добавьте воду."
        "Жұмыртқаны қосыңыз.", // "Добавьте яйцо."
        "Құймақтың ішіне сүт құйыңыз." // "Налейте молоко для теста."
    };

    private string[] correctIngredients = new string[]
    {
        "Ұн", // "Мука"
        "Су", // "Вода"
        "Жұмыртқа", // "Яйцо"
        "Сүт"  // "Молоко"
    };

    void Start()
    {
        // Инициализируем игру с первого шага рецепта
        UpdateRecipeStep();
        foreach (Button ingredientButton in ingredientButtons)
        {
            ingredientButton.onClick.AddListener(() => OnIngredientSelected(ingredientButton));
        }
    }

    // Обновить текст рецепта и изображение блюда
    void UpdateRecipeStep()
    {
        if (currentStep < recipeSteps.Length)
        {
            recipeText.text = recipeSteps[currentStep]; // Показать следующий шаг рецепта
        }
        else
        {
            recipeText.text = "Тамақ дайын болды!"; // "Блюдо готово!"
        }
    }

    // Проверка выбранного ингредиента
    void OnIngredientSelected(Button clickedButton)
    {
        string selectedIngredient = clickedButton.GetComponentInChildren<TextMeshProUGUI>().text;

        // Если игрок выбрал правильный ингредиент
        if (selectedIngredient == correctIngredients[currentStep])
        {
            Debug.Log("Дұрыс! (" + selectedIngredient + ")");
            UpdateDishImage(); // Обновить изображение блюда
            currentStep++; // Переход на следующий шаг
            if (currentStep < recipeSteps.Length)
            {
                UpdateRecipeStep(); // Обновить текст рецепта
            }
            else
            {
                FinishCooking(); // Закончить приготовление
            }
        }
        else
        {
            Debug.Log("Қате! Қайта көріңіз."); // Неправильный выбор, можно дать игроку повторить попытку или дать отрицательный результат
            StartCoroutine(ShowWrongChoiceFeedback(clickedButton));
        }
    }

    // Обновление изображения блюда (этапы приготовления)
    void UpdateDishImage()
    {
        if (currentStep < dishStages.Length)
        {
            dishImage.sprite = dishStages[currentStep]; // Обновить картинку блюда
        }
    }

    // Завершение игры
    void FinishCooking()
    {
        recipeText.text = "Құттықтаймын! Тамақ дайын!"; // Поздравляем, блюдо готово!
    }

    // Визуальная обратная связь для неправильного выбора
    IEnumerator ShowWrongChoiceFeedback(Button clickedButton)
    {
        Color originalColor = clickedButton.GetComponent<Image>().color;
        clickedButton.GetComponent<Image>().color = Color.red; // Поменять цвет на красный
        yield return new WaitForSeconds(1f); // Подождать 1 секунду
        clickedButton.GetComponent<Image>().color = originalColor; // Вернуть исходный цвет
    }
}
