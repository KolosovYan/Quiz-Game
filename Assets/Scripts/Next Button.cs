using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private MainUIController mainUIController;
    [SerializeField] private QuestionsController controller;

    private bool canGoNext;

    public void SetState(bool state) => canGoNext = state;

    public void ButtonPressed()
    {
        if (canGoNext)
        {
            controller.NextQuestion();
            mainUIController.endWindowChangeActive();
        }
        else
            SceneLoader.LoadSceneByName("Menu");
    }
}
