using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileScreen : MonoBehaviour
{
    private PlayerProfileManager profileManager;
    public TextMeshProUGUI inputText;
    public Image editCheckImage;
    public Sprite editIcon, validIcon, invalidIcon;

    public bool editingUsername;
    private bool validUsername;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // method for checking if the username is valid and displaying the 
    // edit and valid/invalid sprites next to the input field
    void UsernameCheck()
    {

    }

    void UpdateUsername()
    {

    }

    void EditingUsername(bool isEditing)
    {
        if (isEditing) { editingUsername = true; }
        else if (!isEditing) { editingUsername = false; }
    }
}
