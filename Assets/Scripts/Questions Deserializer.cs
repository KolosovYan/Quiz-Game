using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionsDeserializer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private QuestionsController questionsController;
    [SerializeField] private TextAsset jsonFile;

    [System.Serializable]
    public class Answer
    {
        public string text;
        public bool correct;
    }

    [System.Serializable]
    public class Question
    {
        public string question;
        public Answer[] answers;
        public string background;
        public bool hasMultiChoice = false;
        public int rightAnswersAmount;
        public int answersAmount => answers.Length;

        public bool CheckMultiChoice()
        {
            int correctCount = 0;

            foreach (Answer a in answers)
            {
                if (a.correct == true)
                    correctCount++;
            }

            rightAnswersAmount = correctCount;

            return correctCount >= 2;
        }


    }

    [System.Serializable]
    public class QuestionList
    {
        public Question[] questions;
    }

    void Start()
    {
        string jsonContent = jsonFile.text;
        QuestionList questionList = JsonUtility.FromJson<QuestionList>(jsonContent);

        List<Question> list = new List<Question>();

        foreach (Question q in questionList.questions)
        {
            q.hasMultiChoice = q.CheckMultiChoice();
            list.Add(q);
        }

        questionsController.SetQuestions(list);
    }
}
