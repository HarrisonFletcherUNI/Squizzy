using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;

public class QuizEnd : MonoBehaviour
{
    public QuizManager quizManager;

    public TextMeshProUGUI locationNameText;           
    public TextMeshProUGUI locationAddressText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalTimeText;

    // Start is called before the first frame update
    void Start()
    {
        locationNameText.text = quizManager.quizData.quizLocationName;
        locationAddressText.text = quizManager.quizData.quizLocationAddress;        
    }

    // Update is called once per frame
    void Update()
    {
        if (quizManager.quizEnded)
        {
            finalScoreText.text = "" + quizManager.finalScore;
            finalTimeText.text = "" + quizManager.finalTime;

            //if (quizManager.underTime)
            //{
            //    finalTimeText.text = "" + quizManager.finalTime + " (TIME GOAL REACHED)";
            //}
        }
    }
}
