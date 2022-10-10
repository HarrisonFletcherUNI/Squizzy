using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quiz", menuName = "Quiz")]
public class QuizData : ScriptableObject
{
    public string quizLocationName, quizLocationAddress;
    public bool hasTimeLimit;
    public float timeLimit;
    
    public Question[] quizQuestions;
}
