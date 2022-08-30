using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public QuestionData[] questions;
    private int quizSize;

    public GameObject answer1, answer2, answer3, answer4;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get quizz length
        quizSize = questions.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
