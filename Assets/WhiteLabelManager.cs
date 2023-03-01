using System.Net;

using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using LootLocker.Requests;

public class WhiteLabelManager : MonoBehaviour
{
    public int Player_iD=0;
    public GameObject StartScreen;
    public GameObject NewUserScreen;
    public GameObject LoginUserScreen;
    public GameObject lobbyScreen;
    // Input fields
    [Header("New User")]
    public TMP_InputField newUserEmailInputField;
    public TMP_InputField newUserPasswordInputField;
    public TMP_InputField nickNameInputField;
    //public TMP_InputField countryInputField;

    [Header("Existing User")]
    public TMP_InputField existingUserEmailInputField;
    public TMP_InputField existingUserPasswordInputField;




    [Header("RememberMe")]
    // Components for enabling auto login
    public Toggle rememberMeToggle;
   
    private int rememberMe;

    //[Header("Button animators")]
    //public Animator autoLoginButtonAnimator;
    //public Animator loginButtonAnimator;
    //public Animator backButtonAnimator;
    //public Animator loginBackButtonAnimator;
    //public Animator newUserButtonAnimator;
    //public Animator resetPasswordButtonAnimator;

    [Header("Player name")]
    public TextMeshProUGUI playerNameText;
   
    public TextMeshProUGUI playerIDText;

    [Header("Error Message")]
    public TextMeshProUGUI errorNameText;
    // Called when pressing "LOGIN" on the login-page
    public void Login()
    {
        LoginUserScreen.SetActive(true);
        StartScreen.SetActive(false);
        string email = existingUserEmailInputField.text;
        string password = existingUserPasswordInputField.text;
        
        LootLockerSDKManager.WhiteLabelLogin(email, password, Convert.ToBoolean(rememberMe), response =>
        {
            if (!response.success)
            {
                // Error
                // Animate the buttons
               
                Debug.Log("error while logging in");
                errorNameText.text = "error while logging in";
                return;
            }
            else
            {
                Debug.Log("Player was logged in succesfully");
                errorNameText.text = "";
                Debug.Log(response);
            }

            // Is the account verified?
            if (response.VerifiedAt == null)
            {
                // Stop here if you want to require your players to verify their email before continuing
            }

            LootLockerSDKManager.StartWhiteLabelSession((response) =>
            {
                if (!response.success)
                {
                    // Error
                    // Animate the buttons
                   
                    Debug.Log("error starting LootLocker session");
                    errorNameText.text = "error starting LootLocker session";
                    return;
                }
                else
                {
                    errorNameText.text = "";
                    // Session was succesfully started;
                    // animate the buttons
                    Player_iD =response.player_id;
                 
                    Debug.Log("session started successfully");
                    Debug.Log("player id"+Player_iD);
                    
                    // Write the current players name to the screen
                    SetPlayerNameToGameScreen();
                }
            });
        });
    }

    // Write the players name to the screen
    void SetPlayerNameToGameScreen()
    {
        LoginUserScreen.SetActive(false);
        NewUserScreen.SetActive(false);
        StartScreen.SetActive(false);
        lobbyScreen.SetActive(true);

        LootLockerSDKManager.GetPlayerName((response) =>
        {
            if (response.success)
            {
               
                PlayerPrefs.SetString("pname", response.name);
                string pname = PlayerPrefs.GetString("pname");
                
                playerNameText.text = "User: "+pname+" UID: "+ PlayerPrefs.GetInt("pid");
                errorNameText.text = "";

            }
        });
    }

    // Called when pressing "CREATE" on new user screen
    public void NewUser()
    {
        Debug.Log("new user function called");
       
        NewUserScreen.SetActive(true);
        StartScreen.SetActive(false);
        string email = newUserEmailInputField.text;
        string password = newUserPasswordInputField.text;
        string newNickName = nickNameInputField.text;

        if (email == "" || password == "")
        {
            Debug.Log("empty");
            errorNameText.text = "Please Fill All Fields";
            return;
        }
        else
        {
            Debug.Log("session enter");
            Debug.Log(email);
            Debug.Log(password);


            // string country=countryInputField.text;
            // Local function for errors
            void Error(string error)
            {
                Debug.Log(error);
               
            }

            LootLockerSDKManager.WhiteLabelSignUp(email, password, (response) =>
            {
                Debug.Log("SessionRequest");
                if (!response.success)
                {
                   // Error(response.Error);
                    errorNameText.text = response.Error;
                    return;
                }
                else
                {
                    errorNameText.text = "";
                    // Succesful response
                    // Log in player to set name
                    // Login the player
                  

                    LootLockerSDKManager.WhiteLabelLogin(email, password, false, response =>
                        {
                            if (!response.success)
                            {
                                Error(response.Error);
                                errorNameText.text = response.Error;
                                return;
                            }
                            errorNameText.text = "";
                        // Start session
                        LootLockerSDKManager.StartWhiteLabelSession((response) =>
                            {
                                if (!response.success)
                                {
                                    Error(response.Error);
                                    errorNameText.text = response.Error;
                                    return;
                                }
                                errorNameText.text = "";
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
                                        errorNameText.text = response.Error;
                                        return;
                                    }
                                    errorNameText.text = "";
                                    PlayerPrefs.SetString("pname", response.name);
                                // End this session
                                LootLockerSessionRequest sessionRequest = new LootLockerSessionRequest();
                                    LootLocker.LootLockerAPIManager.EndSession(sessionRequest, (response) =>
                                    {
                                        if (!response.success)
                                        {
                                            Error(response.Error);
                                            errorNameText.text = response.Error;
                                            return;
                                        }
                                        Debug.Log("Account Created");
                                        errorNameText.text = "";

                                      
                                        PlayerPrefs.SetInt("pid", response.player_id);
                                        NewUserScreen.SetActive(false);
                                        AutoLogin();
                                    // New user, turn off remember me
                                    rememberMeToggle.isOn = false;
                                    });
                                });
                            });
                        });
                }
            });
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        NewUserScreen.SetActive(false);
        LoginUserScreen.SetActive(false);
        StartScreen.SetActive(true);
        // See if we should log in the player automatically
        rememberMe = PlayerPrefs.GetInt("rememberMe", 0);
        if (rememberMe == 0)
        {
            rememberMeToggle.isOn = false;
        }
        else
        {
            rememberMeToggle.isOn = true;
        }
    }

    // Called when changing the value on the toggle
    public void ToggleRememberMe()
    {
        bool rememberMeBool = rememberMeToggle.isOn;
        rememberMe = Convert.ToInt32(rememberMeBool);

        // Animate button
      
        PlayerPrefs.SetInt("rememberMe", rememberMe);
    }

    public void AutoLogin()
    {
        // Does the user want to automatically log in?
        if (Convert.ToBoolean(rememberMe) == true)
        {
            // Hide the buttons on the login screen
           

          
            LootLockerSDKManager.CheckWhiteLabelSession(response =>
            {
                if (response == false)
                {
                    // Session was not valid, show error animation
                    // and show back button
                 

                    // set the remember me bool to false here, so that the next time the player press login
                    // they will get to the login screen
                    rememberMeToggle.isOn = false;
                }
                else
                {
                    // Session is valid, start game session
                    LootLockerSDKManager.StartWhiteLabelSession((response) =>
                    {
                        if (response.success)
                        {
                            // It was succeful, log in

                            PlayerPrefs.SetInt("pid", response.player_id);

                            SetPlayerNameToGameScreen();


                           
                        }
                        else
                        {
                            
                          

                            Debug.Log("error starting LootLocker session");
                         
                            rememberMeToggle.isOn = false;

                            return;
                        }

                    });

                }

            });
        }
        else if(Convert.ToBoolean(rememberMe) == false)
        {
            LoginUserScreen.SetActive(true);
            StartScreen.SetActive(false);
           
        }
    }

    // public void PasswordReset()
    // {
    //     string email = resetPasswordInputField.text;
    //     LootLockerSDKManager.WhiteLabelRequestPassword(email, (response) =>
    //     {
    //         if (!response.success)
    //         {
    //             Debug.Log("error requesting password reset");
    //             resetPasswordButtonAnimator.SetTrigger("Error");
    //             backButtonAnimator.SetTrigger("Show");
    //             return;
    //         }

    //         Debug.Log("requested password reset successfully");
    //         resetPasswordButtonAnimator.SetTrigger("Done");
    //         backButtonAnimator.SetTrigger("Show");
    //     });
    // }

    // public void ResendVerificationEmail()
    // {
    //     int playerID = 0;
    //     LootLockerSDKManager.WhiteLabelRequestVerification(playerID, (response) =>
    //     {
    //         if (response.success)
    //         {
    //             // Email was sent!
    //         }
    //     });
    // }
}
