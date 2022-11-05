using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardSelect : MonoBehaviour
{
    public GameObject menuScreen, leaderboardScreen;
    public void ShowLeaderboard()
    {
        menuScreen.SetActive(false);
        leaderboardScreen.SetActive(true);
    }
    public void ShowSelectScreen()
    {
        menuScreen.SetActive(false);
        leaderboardScreen.SetActive(true);

    }
}
