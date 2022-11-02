using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using LootLocker.Requests;

public class PlayerLogin : MonoBehaviour
{
    public TMPro.TMP_InputField userEmail, userPass, newUserEmail, newUserPass, newUserNickname, resetEmail;
    // Start is called before the first frame update
    public void Login()
    {
        string email = userEmail.text;
        string password = userPass.text;
        LootLockerSDKManager.WhiteLabelLogin(email, password, response =>
        {
            if (!response.success)
            {
                //cant login
            }
            else
            {
                //player logged in
            }
            LootLockerSDKManager.StartWhiteLabelSession((response) =>
            {
                if (!response.success)
                {
                    // Error
                    // Animate the buttons
                    Debug.Log("error starting LootLocker session");
                    return;
                }
                else
                {
                    // Session was succesfully started;
                    // animate the buttons
                    Debug.Log("session started successfully");
                    // Write the current players name to the screen

                }
            });
        });
    }
    public void NewUser()
    {
        string email = newUserEmail.text;
        string password = newUserPass.text;
        string newNickName = newUserNickname.text;

        // Local function for errors
        static void Error(string error)
        {
            Debug.Log(error);
        }

        LootLockerSDKManager.WhiteLabelSignUp(email, password, (response) =>
        {
            if (!response.success)
            {
                Error(response.Error);
                return;
            }
            else
            {
                // Succesful response
                // Log in player to set name
                // Login the player
                LootLockerSDKManager.WhiteLabelLogin(email, password, false, response =>
                {
                    if (!response.success)
                    {
                        Error(response.Error);
                        return;
                    }
                    // Start session
                    LootLockerSDKManager.StartWhiteLabelSession((response) =>
                    {
                        if (!response.success)
                        {
                            Error(response.Error);
                            return;
                        }
                        // Set nickname to be public UID if nothing was provided
                        if (newNickName == "")
                        {
                            newNickName = response.public_uid;
                        }
                        // Set new nickname for player
                        LootLockerSDKManager.SetPlayerName(newNickName, (response) =>
                        {
                            if (!response.success)
                            {
                                Error(response.Error);
                                return;
                            }

                            // End this session
                            LootLockerSessionRequest sessionRequest = new LootLockerSessionRequest();
                            LootLocker.LootLockerAPIManager.EndSession(sessionRequest, (response) =>
                            {
                                if (!response.success)
                                {
                                    Error(response.Error);
                                    return;
                                }
                                Debug.Log("Account Created");
                            });
                        });
                    });
                });
            }
        });
    }
    public void ResetPassword()
    {
        string email = resetEmail.text;
        LootLockerSDKManager.WhiteLabelRequestPassword(email, (response) =>
        {
            if (!response.success)
            {
                Debug.Log("error requesting password reset");

                return;
            }

            Debug.Log("Requested password reset successfully");
        });


    }
    public void GuestLogin()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");

                return;
            }

            Debug.Log("successfully started LootLocker session");
        });
    }

}

