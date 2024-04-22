using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI answersCountText;
    [SerializeField] private Image questionsBackground;

    [Header("Question End")]

    [SerializeField] private GameObject window;
    [SerializeField] private TextMeshProUGUI endText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private GameObject resultTextGO;
    [SerializeField] private NextButton nextButton;
    [SerializeField] private TextMeshProUGUI nextButtonText;

    public void SetMultiQuestionTextState(bool state) => answersCountText.enabled = state;

    public void SetQuestionText(string text) => questionText.text = text;

    public void SetQuestionBackground(Sprite background) => questionsBackground.sprite = background;

    public void SetAnswersCount(int current, int target) => answersCountText.text = $"{current}/{target}";

    public void ShowQuestionEndWindow(bool isCorrect)
    {
        endText.text = isCorrect ? "Верно" : "Неверно";
        nextButtonText.text = "Далее";
        nextButton.SetState(true);
        endWindowChangeActive();
    }

    public void ShowWinWindow()
    {
        endText.text = "Результат:";
        resultText.text = $"Правильных ответов: {ScoreController.Score}";
        nextButtonText.text = "Новая игра";
        resultTextGO.SetActive(true);
        endWindowChangeActive();
        nextButton.SetState(false);
    }

    public void endWindowChangeActive() => window.SetActive(!window.activeSelf);
}
