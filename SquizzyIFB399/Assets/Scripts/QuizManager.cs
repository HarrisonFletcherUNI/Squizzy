using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public string quizLocationName, quizLocationAddress;
    public QuestionData[] questions;
    private int quizSize;

    public TextMeshProUGUI locationNameText, locationAddressText;
    public TextMeshProUGUI scoreText, timerText;
    private float currentScore, timeMinutes, timeSeconds;

    [Header("Active Question")]
    public int activeQuestion;
    public TextMeshProUGUI questionText, questionNumberText;
    public RawImage questionImage;
    public CorrectAnswer activeAnswer;
    public string chosenAnswer;

    [Header("Answer Buttons")]
    public GameObject answer1Object;
    public GameObject answer2Object;
    public GameObject answer3Object;
    public GameObject answer4Object;

    [Header("Quiz Results")]
    public float finalScore;
    public float finalTime;

    bool timerActive, quizFinished;
    
    // Start is called before the first frame update
    void Start()
    {
        
        // Set Questions
        quizSize = questions.Length; // get quiz length
        activeQuestion = 0; // reset active question
        ValidQuizCheck();

        SetQuestion();
                    
        // Set Location Info
        locationNameText.text = quizLocationName;
        locationAddressText.text = quizLocationAddress;

        // Score and Time
        scoreText.text = "0/" + quizSize;

        timerActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    void Timer()
    {
        if (timerActive)
        {
            timeSeconds += Time.deltaTime;

            // add minute to clock
            if (timeSeconds >= 60f)
            {
                timeMinutes++;
            }
        }

        // add extra 0 in timer if under 10 seconds
        if (timeSeconds < 9.5f)
        {
            timerText.text = timeMinutes + " : 0" + Mathf.Round(timeSeconds);
        }
        // else, display time normally
        else
        {
            timerText.text = timeMinutes + " : " + Mathf.Round(timeSeconds);
        }       
    }

    public void SetQuestion()
    {
        Debug.Log("Setting Question Data...");

        // Question
        questionText.text = questions[activeQuestion].questionText;

        // Question Number
        questionNumberText.text = "Question " + (activeQuestion + 1); // +1 since array starts at 0

        // Image
        questionImage.texture = questions[activeQuestion].questionImage;

        // Answers
        // A
        answer1Object.GetComponentInChildren<TextMeshProUGUI>().text = questions[activeQuestion].answerA;
        // B
        answer2Object.GetComponentInChildren<TextMeshProUGUI>().text = questions[activeQuestion].answerB;
        // C
        answer3Object.GetComponentInChildren<TextMeshProUGUI>().text = questions[activeQuestion].answerC;
        // D
        answer4Object.GetComponentInChildren<TextMeshProUGUI>().text = questions[activeQuestion].answerD;

        activeAnswer = questions[activeQuestion].correctAnswer;
        chosenAnswer = "Unanswered";
    }

    public void ChooseAnswer(string answer)
    {
        // check the chosen answer to the correct answer (compare as strings)
        if (answer == activeAnswer.ToString())
        {
            Debug.Log("Correct!");
            // increase score by 1
            currentScore++;
            scoreText.text = currentScore + "/" + quizSize;
        }
        else if (answer != activeAnswer.ToString())
        {
            Debug.Log("Incorrect!");
        }


        // go to next question
        NextQuestion();
    }

    public void NextQuestion()
    {
        Debug.Log("Next Question");
        // increase question number
        activeQuestion++;

        // if the question number is invalid (quiz is finished)
        if (activeQuestion >= quizSize)
        {
            EndQuiz();
        }
        // else, continue to next question
        else
        {
            SetQuestion();
        }        
    }

    void EndQuiz()
    {       
        // get quiz results
        QuizResults();

        timerActive = false;
        quizFinished = true;
    }

    void QuizResults()
    {
        // collect final score
        finalScore = currentScore;
        // collect final time
        finalTime = (timeMinutes * 60) + timeSeconds; // total seconds

        finalTime = Mathf.Round(finalTime);


        Debug.Log("Quiz Complete! Final Score: " + finalScore);
    }

    void ValidQuizCheck()
    {       
        // check if there are actually questions
        if (quizSize <= 0) { Debug.Log("Error: No questions set"); } 
    }
}
