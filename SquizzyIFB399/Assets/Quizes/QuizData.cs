using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quiz", menuName = "Quiz")]
public class QuizData : ScriptableObject
{
    public string quizLocationName, quizLocationAddress;
    public bool hasTimeGoal;
    public float timeGoal;
    
    public Question[] quizQuestions;
}
