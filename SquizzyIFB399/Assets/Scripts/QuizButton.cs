using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] GameObject correctIcon;
    [SerializeField] GameObject incorrectIcon;

    public void AnswerCorrect()
    {
        correctIcon.SetActive(true);
        incorrectIcon.SetActive(false);
    }

    public void AnswerIncorrect()
    {
        incorrectIcon.SetActive(true);
        correctIcon.SetActive(false);
    }

    public void DisableIcons()
    {
        correctIcon.SetActive(false);
        incorrectIcon.SetActive(false);
    }

    public void DisableButton()
    {
        button.enabled = false;
    }

    public void EnableButton()
    {
        button.enabled = true;
        DisableIcons();
    }


}
