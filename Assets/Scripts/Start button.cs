using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Пространство имён для управления сценами
using UnityEngine.UI; // Пространство имён для UI


public class Startbutton : MonoBehaviour
{
    public Button startButton; // Ссылка на кнопку Старт

    void Start()
    {
        // Добавляем событие на нажатие кнопки
        startButton.onClick.AddListener(OnStartButtonClick);
    }

    // Метод, который будет вызван при нажатии кнопки
    void OnStartButtonClick()
    {
        // Загружаем следующую сцену (замените "SceneName" на название вашей сцены)
        SceneManager.LoadScene("Main Scene");
    }
}
