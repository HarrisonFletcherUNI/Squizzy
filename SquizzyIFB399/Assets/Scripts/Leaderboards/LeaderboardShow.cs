using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using System;
using TMPro;

public class LeaderboardShow : MonoBehaviour
{
    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScores;
    public TextMeshProUGUI leaderboardTitle;
    private int[] quizLeaderboards = { 8396, 8397, 8398, 8399, 8400 }; //In order - Bot Garden/Roma St/New Farm/Kangaroo Point/Mt Cootha
    private string[] quizzes = { "BotanicGarden", "RomaSt", "NewFarm", "KangarooPoint", "MtCootha" };
    public void FetchHighScores(int index)
    {
        LootLockerSDKManager.GetScoreList(quizLeaderboards[index], 10, 0, (response) =>
        {
            if (response.success)
            {
                string tempPlayerNames = "Names\n";
                string tempPlayerScores = "Scores\n";
                LootLockerLeaderboardMember[] members = response.items;
                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    if (members[i].player.name != "")
                    {
                        tempPlayerNames += members[i].player.name;
                    }
                    else
                    {
                        tempPlayerNames += members[i].player.id;
                    }
                    tempPlayerScores += members[i].score + "\n";
                    tempPlayerNames += "\n";
                }
                playerNames.text = tempPlayerNames;
                playerScores.text = tempPlayerScores;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
            }
        });
    }
    public void LeaderboardNameDisplay(int index)
    {
        leaderboardTitle.text = quizzes[index];
    }
}
