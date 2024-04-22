using System.Collections;
using System.Collections.Generic;
using static QuestionsDeserializer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Image cashedImage;
    [SerializeField] private RectTransform cashedTransform;

    private QuestionsController questionsController;
    private bool isCorrect;
    private bool canClick = true;

    public void SetController(QuestionsController controller) => questionsController = controller;

    public void SetPosition(Vector2 position) => cashedTransform.localPosition = position;

    public void SetSize(Vector2 size) => cashedTransform.sizeDelta = size;

    public void SetButton(Answer answer)
    {
        buttonText.text = answer.text;
        isCorrect = answer.correct;
    }

    public void ButtonPressed()
    { 
        if (canClick && questionsController.CanChooseAnswer)
        {
            canClick = false;
            ChangeColor();
            questionsController.CheckChoice(isCorrect);
        }
    }

    private void ChangeColor() => cashedImage.color = isCorrect ? Color.green : Color.red;
}

