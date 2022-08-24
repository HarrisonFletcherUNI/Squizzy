using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UsernameDisplay : MonoBehaviour
{
    // Place this script on a text object to display the player's username automatically
    private TextMeshProUGUI usernameText;
    private PlayerProfileManager profileManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get references
        usernameText = GetComponent<TextMeshProUGUI>();
        profileManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerProfileManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // update dispalyed username
        usernameText.text = profileManager.username;
    }
}
