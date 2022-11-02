using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using System;

public class LeaderboardUpload : MonoBehaviour
{
    private int[] quizLeaderboards = { 8396, 8397, 8398, 8399, 8400}; //In order - Bot Garden/Roma St/New Farm/Kangaroo Point/Mt Cootha
    private string[] quizzes = { "BotanicGarden", "RomaSt", "NewFarm", "KangarooPoint", "MtCootha" };
    // Update is called once per frame
    void Update()
    {

    }
    public bool ScoreUpload(string quizName, int scoreUpload)
    {
        int quizIndex = Array.IndexOf(quizzes, quizName);
        if (quizIndex == -1)
        {
            Debug.Log("Quiz not found");
            return false;
        }
        SubmitScoreRoutine(scoreUpload, quizLeaderboards[quizIndex]);
        return true;
    }
    public IEnumerator SubmitScoreRoutine(int scoreToUpload, int leaderboardID)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Success uploading score");
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
