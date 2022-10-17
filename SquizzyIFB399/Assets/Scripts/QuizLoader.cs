
using System.IO;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace SquizzyReader
{

    

    
    class QuizLoader
    {
        // Default folder    
           
  
        public static void LoadJson()
        {
            using (StreamReader r = new StreamReader(@"C:\Users\campb\OneDrive\Documents\Capstone Project Pt.1\test.json"))
            {
                string json = r.ReadToEnd();
                List<QuestionData> questions = JsonConvert.DeserializeObject<List<QuestionData>>(json);
                foreach (QuestionData question in questions)
                {
                    Console.WriteLine("{0} {1}", question.questionText, question.correctAnswer);
                }
                
            }
        }

        public class QuestionData
        {

            public string questionText;
            public string answerA, answerB, answerC, answerD;
            public string correctAnswer;
        }
        static void Main(string[] args)
        {

            LoadJson();
           

            Console.ReadKey();
        }
    }
}
