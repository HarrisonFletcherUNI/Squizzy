using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;

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

    [Header("Quiz Screens")]
    public GameObject startScreen;
    public GameObject endScreen;

    [Header("Quiz Results")]
    public float finalScore;
    public float finalTime;

    bool timerActive;

    [SerializeField] UnityEvent OnQuizStart;
    //[SerializeField] UnityEvent OnQuizComplete;
    [SerializeField] UnityEvent OnQuizEnd;
    [SerializeField] UnityEvent OnQuestionSelect;
    [SerializeField] UnityEvent OnQuestionCorrect;
    [SerializeField] UnityEvent OnQuestionIncorrect;
    [SerializeField] UnityEvent OnNewQuestion;

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
        
        
        // activate start screen
        if (startScreen != null)
        {
            startScreen.SetActive(true);
        }
        // deactivate end screen
        if (endScreen != null)
        {
            endScreen.SetActive(false);
        }
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

        // enable buttons
        EnableButtons();
    }

    public void ChooseAnswer(GameObject answerObject)
    {
        // Event - this will trigger at the same time as the correct/incorrect events
        OnQuestionSelect.Invoke();
        
        string answer = ""; // this only works if assigned an empty string don't ask me why

        // check which answer the button corresponds to
        if (answerObject == answer1Object) { answer = "A"; }
        else if (answerObject == answer2Object) { answer = "B"; }
        else if (answerObject == answer3Object) { answer = "C"; }
        else if (answerObject == answer4Object) { answer = "D"; }

        // disable button interaction
        DisableButtons();

        // check the chosen answer to the correct answer (compare as strings)
        if (answer == activeAnswer.ToString())
        {
            Debug.Log("Correct!");

            // Event
            OnQuestionCorrect.Invoke();

            // increase score by 1
            currentScore++;
            scoreText.text = currentScore + "/" + quizSize;
            StartCoroutine(CorrectAnswer(answerObject.transform, 1.5f));
        }
        else if (answer != activeAnswer.ToString())
        {
            Debug.Log("Incorrect!");

            // Event
            OnQuestionIncorrect.Invoke();

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
            // Event
            OnNewQuestion.Invoke();

            SetQuestion();
        }        
    }

    void ValidQuizCheck()
    {       
        // check if there are actually questions
        if (quizSize <= 0) { Debug.Log("Error: No questions set"); } 
    }

    // simple shake tween for when the wrong answer is chosen
    void ButtonShake(Transform button)
    {
        button.GetComponent<QuizButton>().AnswerIncorrect();
        button.DOPunchRotation(new Vector3(0, 0, 2), 1, 20, 2);
    }

    // scale and highlight for correct answer
    IEnumerator CorrectAnswer(Transform chosenButton, float waitTime)
    {
        Vector3 originalScale = chosenButton.localScale;
        chosenButton.GetComponent<QuizButton>().AnswerCorrect();
        chosenButton.DOScale(chosenButton.localScale * 1.1f, waitTime / 3);
        yield return new WaitForSeconds(waitTime / 3);
        chosenButton.DOScale(originalScale, waitTime / 3);
    }

    IEnumerator NextQuestionWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NextQuestion();
    }

    void DisableButtons()
    {
        // A
        answer1Object.GetComponent<QuizButton>().DisableButton();
        // B
        answer2Object.GetComponent<QuizButton>().DisableButton();
        // C
        answer3Object.GetComponent<QuizButton>().DisableButton();
        // D
        answer4Object.GetComponent<QuizButton>().DisableButton();
    }

    void EnableButtons()
    {        
        // A
        answer1Object.GetComponent<QuizButton>().EnableButton();
        // B
        answer2Object.GetComponent<QuizButton>().EnableButton();
        // C
        answer3Object.GetComponent<QuizButton>().EnableButton();
        // D
        answer4Object.GetComponent<QuizButton>().EnableButton();
    }

    public void StartQuiz()
    {
        // enable timer
        timerActive = true;
        
        // if the start screen is defined but isn't deactivated on begin button for whatever reason
        if (startScreen.activeSelf && startScreen != null)
        {
            startScreen.SetActive(false);
        }

        OnQuizStart.Invoke();
    }

    void EndQuiz()
    {
        // get quiz results
        QuizResults();

        timerActive = false;
        //quizFinished = true;

        OnQuizEnd.Invoke();
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
}
