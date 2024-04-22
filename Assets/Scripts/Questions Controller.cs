using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestionsDeserializer;

public class QuestionsController : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private MainUIController mainUIController;
    [SerializeField] private RectTransform answerContainer;
    [SerializeField] private AnswerButton answerPrefab;

    [Header("Settings")]

    [SerializeField] private float offset;

    [Header("Debug")]

    [SerializeField] private List<Question> availableQuestions;
    public bool CanChooseAnswer { get; private set; }

    private Question currentQuestion;
    private float currentOffset;
    private int answersGiven;
    private bool isMultiChoice = false;
    private int answersAmount;
    private int rightAnswersAmount;
    private List<GameObject> answersGO = new List<GameObject>();

    public void NextQuestion()
    {
        if (HasQuestions())
        {
            CreateQuestion();
        }

        else
            mainUIController.ShowWinWindow();
    }

    public void SetQuestions(List<Question> list)
    {
        ScoreController.ResetScore();
        availableQuestions = new List<Question>(list);

        CreateQuestion();
    }

    private void CreateQuestion()
    {
        answersGiven = 0;

        currentQuestion = availableQuestions[Random.Range(0, availableQuestions.Count)];
        availableQuestions.Remove(currentQuestion);
        answersAmount = currentQuestion.answersAmount;
        SetQuestionType();
        mainUIController.SetQuestionText(currentQuestion.question);
        CreateAnswers();
        mainUIController.SetQuestionBackground(GetQuestionBackground());
        CanChooseAnswer = true;
    }

    public void CheckChoice(bool isRightAnswer)
    {
        if (isRightAnswer)
        {
            if (isMultiChoice)
            {
                answersGiven++;
                mainUIController.SetAnswersCount(answersGiven, rightAnswersAmount);

                if (answersGiven == rightAnswersAmount)
                {
                    mainUIController.ShowQuestionEndWindow(true);
                    ScoreController.AddScore();
                }
            }

            else
            {
                CanChooseAnswer = false;
                mainUIController.ShowQuestionEndWindow(true);
                ScoreController.AddScore();
            }
        }

        else
        {
            CanChooseAnswer = false;
            mainUIController.ShowQuestionEndWindow(false);
        }
    }

    private void SetQuestionType()
    {
        if (currentQuestion.hasMultiChoice)
        {
            isMultiChoice = true;
            rightAnswersAmount = currentQuestion.rightAnswersAmount;
            mainUIController.SetMultiQuestionTextState(true);
            mainUIController.SetAnswersCount(answersGiven, rightAnswersAmount);
        }

        else
        {
            isMultiChoice = false;
            mainUIController.SetMultiQuestionTextState(false);
            rightAnswersAmount = 1;
        }

    }

    private Sprite GetQuestionBackground()
    {
        return Resources.Load<Sprite>(currentQuestion.background.Replace(".jpg", ""));
    }

    private Vector2 CalculateAnswerSize()
    {
        int defaultAnswersAmount = 4;
        currentOffset = answersAmount <= 4 ? offset : (offset - ((offset / answersAmount) * (answersAmount - defaultAnswersAmount)));

        float containerHeight = answerContainer.rect.height;
        float containerWidth = answerContainer.rect.width;

        float answerSizeY = (containerHeight - (currentOffset * (answersAmount + 1))) / answersAmount;
        float answerSizeX = containerWidth;

        return new Vector2(answerSizeX, answerSizeY);
    }

    private void CreateAnswers()
    {
        if (answersGO.Count != 0)
            ClearAnswers();

        Vector2 answerSize = CalculateAnswerSize();
        float spawnPosY = (answerContainer.rect.height / 2) - currentOffset - (answerSize.y / 2);

        for (int i = 0; i < answersAmount; i++)
        {
            AnswerButton answerButton = Instantiate(answerPrefab, answerContainer);
            answerButton.SetController(this);
            answerButton.SetSize(answerSize);
            answerButton.SetPosition(new Vector2(0, spawnPosY));
            answerButton.SetButton(currentQuestion.answers[i]);
            answersGO.Add(answerButton.gameObject);

            spawnPosY -= (currentOffset + answerSize.y);
        }
    }

    private void ClearAnswers()
    {
        foreach(GameObject a in answersGO)
        {
            Destroy(a);
        }

        answersGO.Clear();
    }

    private bool HasQuestions()
    {
        return availableQuestions.Count > 0;
    }

}
