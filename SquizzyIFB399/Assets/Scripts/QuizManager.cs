using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public string quizLocationName,quizLocationAddress;
    public QuestionData[] questions;
    private int quizSize;

    public TextMeshProUGUI scoreText, timerText;
    private float finalScore, finalTime;

    [Header("Answer Buttons")]
    public GameObject answer1Object;
    public GameObject answer2Object;
    public GameObject answer3Object;
    public GameObject answer4Object;

    bool timerActive;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get quizz length
        quizSize = questions.Length;

        timerActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            Timer();
        }
    }

    void Timer()
    {
        
    }
}
