using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] GameObject correctIcon;
    [SerializeField] GameObject incorrectIcon;

    public bool textScaling;
    [SerializeField] int scalerStartLength;
    [SerializeField] int sizeDecreaseAmount;
    private int textSize;
    private int textSizeB;
    private int textSizeC;

    public void Start()
    {
        // Set text scaler variables
        if (textScaling)
        {
            textSize = (int)buttonText.fontSize;
            textSizeB = textSize - sizeDecreaseAmount;
            textSizeC = textSizeB - sizeDecreaseAmount;
        }
    }

    public void Update()
    {
        if (textScaling)
        {
            ScaleText();
        }
    }

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

    // for scaling text size depedning on the text length (there's probably some method for this already but I couldn't find it)
    public void ScaleText()
    {
        if (buttonText.text.Length > scalerStartLength)
        {
            buttonText.fontSize = textSizeB;
        }
        else if (buttonText.text.Length > (scalerStartLength + scalerStartLength))
        {
            buttonText.fontSize = textSizeC;
        }
        else
        {
            buttonText.fontSize = textSize;
        }
    }


}
