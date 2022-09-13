using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Quiz/Question")]
public class QuestionData : ScriptableObject
{
    [TextArea(5, 10)]
    public string questionText;
    public Texture2D questionImage;
    public string answerA, answerB, answerC, answerD;
    public CorrectAnswer correctAnswer;
   
}
public enum CorrectAnswer { A, B, C, D, Null };

