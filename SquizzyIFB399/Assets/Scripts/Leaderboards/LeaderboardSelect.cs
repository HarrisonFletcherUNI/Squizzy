using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardSelect : MonoBehaviour
{
    public GameObject[] leaderboardScreens;
    int currentSelection;
    public void ShowLeaderboard(int selection)
    {
        leaderboardScreens[0].SetActive(false);
        leaderboardScreens[selection].SetActive(true);
        currentSelection = selection;
    }
    public void ShowSelectScreen()
    {
        leaderboardScreens[currentSelection].SetActive(false);
        leaderboardScreens[0].SetActive(true);

    }
}
