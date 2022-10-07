using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class QuizManager : MonoBehaviour
{
    // use this script with the main quiz parent object, in the test scene its just called "ActiveQuiz" 
    // drag and drop a QuizData scriptable object in the QuizData field and as long as all the UI elements assigned beforehand,
    // the quiz should configure everything (and play) automatically. All the main parent objects have tags in case they need to be found,
    // but its easier to just set everything in the inspector
    
    [Header("Quiz Data")][Tooltip("Quiz questions, location and other data - Create a new Quiz by selecting 'Quiz' at the top of the asset creation list in the project window")]
    public QuizData quizData;
    private int quizSize;
    private float currentScore, timeMinutes, timeSeconds;

    [Header("Quiz UI Elements")][Tooltip("The location name (not the full adress)")]
    public TextMeshProUGUI locationNameText;
    [Tooltip("The location adress (what you would type into a map)")]
    public TextMeshProUGUI locationAddressText;
    public TextMeshProUGUI scoreText, timerText;    

    // Active Question variables
    private int activeQuestion;
    public TextMeshProUGUI questionText, questionNumberText;
    [Tooltip("The image associated with the active question (set by QuizData)")]
    public RawImage questionImage; 
    private CorrectAnswer activeAnswer;
    [HideInInspector] public string chosenAnswer; // answer buttons interact with this string to choose answers on tap
    [HideInInspector] public GameObject chosenAnswerObject; // answer buttons interact with this string to choose answers on tap

    [Header("Answer Buttons")] // the full button game objects (not just the button component)
    public GameObject answer1Object;
    public GameObject answer2Object;
    public GameObject answer3Object;
    public GameObject answer4Object;

    [Header("Visual Effects")]
    public Color correctAnswerColor;
    public Color incorrectAnswerColor;

    [Header("Quiz Results")]
    public float finalScore;
    public float finalTime;

    bool timerActive;
    
    // Start is called before the first frame update
    void Start()
    {
        // Set Questions
        quizSize = quizData.quizQuestions.Length; // get quiz length
        activeQuestion = 0; // reset active question
        ValidQuizCheck();

        SetQuestion();
                    
        // Set Location Info
        locationNameText.text = quizData.quizLocationName;
        locationAddressText.text = quizData.quizLocationAddress;

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
        questionText.text = quizData.quizQuestions[activeQuestion].questionText;

        // Question Number
        questionNumberText.text = "Question " + (activeQuestion + 1); // +1 since array starts at 0

        // Image
        questionImage.texture = quizData.quizQuestions[activeQuestion].questionImage;

        // Answers
        // A
        answer1Object.GetComponentInChildren<TextMeshProUGUI>().text = quizData.quizQuestions[activeQuestion].answerA;
        // B
        answer2Object.GetComponentInChildren<TextMeshProUGUI>().text = quizData.quizQuestions[activeQuestion].answerB;
        // C
        answer3Object.GetComponentInChildren<TextMeshProUGUI>().text = quizData.quizQuestions[activeQuestion].answerC;
        // D
        answer4Object.GetComponentInChildren<TextMeshProUGUI>().text = quizData.quizQuestions[activeQuestion].answerD;

        activeAnswer = quizData.quizQuestions[activeQuestion].correctAnswer;
        chosenAnswer = "Unanswered";
    }

    public void ChooseAnswer(GameObject answerObject)
    {        
        string answer = ""; // this only works if assigned an empty string don't ask me why

        // check which answer the button corresponds to
        if (answerObject == answer1Object) { answer = "A"; }
        else if (answerObject == answer2Object) { answer = "B"; }
        else if (answerObject == answer3Object) { answer = "C"; }
        else if (answerObject == answer4Object) { answer = "D"; }
        
        // check the chosen answer to the correct answer (compare as strings)
        if (answer == activeAnswer.ToString())
        {
            Debug.Log("Correct!");
            // increase score by 1
            currentScore++;
            scoreText.text = currentScore + "/" + quizSize;
            StartCoroutine(CorrectAnswer(answerObject.transform, 1.5f));
        }
        else if (answer != activeAnswer.ToString())
        {
            Debug.Log("Incorrect!");
            ButtonShake(answerObject.transform); // shake wrong answer           
        }

        StartCoroutine(NextQuestionWait(1.5f)); // wait for animation to finish then go the next question
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
        //quizFinished = true;
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

    // simple shake tween for when the wrong answer is chosen
    void ButtonShake(Transform objectToShake)
    {
        objectToShake.DOPunchRotation(new Vector3(0, 0, 2), 1, 20, 2);
    }

    // scale and highlight for correct answer
    IEnumerator CorrectAnswer(Transform objectToHighlight, float waitTime)
    {
        Vector3 originalScale = objectToHighlight.localScale;
        objectToHighlight.GetComponent<Image>().color = correctAnswerColor;
        objectToHighlight.DOScale(objectToHighlight.localScale * 1.1f, waitTime / 3);
        yield return new WaitForSeconds(waitTime / 3);
        objectToHighlight.GetComponent<Image>().color = Color.white;
        objectToHighlight.DOScale(originalScale, waitTime / 3);
    }

    IEnumerator NextQuestionWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NextQuestion();
    }
}
