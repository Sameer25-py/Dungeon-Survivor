using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;
using UnityEngine.SceneManagement;

namespace DungeonSurvivor.Scenes.Shaheer.Revamp
{
    public class HomePage : MonoBehaviour
    {
        [SerializeField] private Button login, register;
        [SerializeField] private RegisterPage registerPage;
        [SerializeField] private TMP_InputField nick, pass;
        [SerializeField] private Toggle autoLogin;
        [SerializeField] private TMP_Text message;

        private const string AUTOLOGIN = "AutoLogin";
        private const string NICK = "Nick";
        private const string PASS = "Pass";
        private const string MESSAGE1 = "Welcome to Dungeon Survivor!";
        private const string MESSAGE2 = "Nickname/Password is empty";
        private const string MESSAGE3 = "Wrong Nickname or Password";
        private const string MESSAGE4 = "Unknown Error Encountered";
        private const string MESSAGE5 = "Logged in Sucessfully!";
        
        private void Awake()
        {
            register.onClick.AddListener(Register);
            login.onClick.AddListener(Login);
            Reset();
            if (Convert.ToBoolean(PlayerPrefs.GetInt(AUTOLOGIN)))
            {
                nick.text = PlayerPrefs.GetString(NICK);
                pass.text = PlayerPrefs.GetString(PASS);
                Login();
            }
        }
        public void Reset()
        {
            message.text = MESSAGE1;
            nick.text = "";
            pass.text = "";
        }
        private void Register()
        {
            gameObject.SetActive(false);
            registerPage.Reset();
            registerPage.gameObject.SetActive(true);
        }
        private void Login()
        {
            // UIManager.Instance.SetWaitingState(true);
            if (nick.text == "" || pass.text == "")
            {
                UIManager.Instance.ErrorMessage(MESSAGE2);
                // UIManager.Instance.SetWaitingState(false);
                return;
            }
            LootLockerSDKManager.WhiteLabelLogin(nick.text.Trim(), pass.text.Trim(), OnLoginComplete);
            // UIManager.Instance.SetWaitingState(false);
        }
        private void OnLoginComplete(LootLockerWhiteLabelLoginResponse response)
        {
            
            if (response.success)
            {
                //added by shaheer
                LootLockerSDKManager.StartWhiteLabelSession((response) =>
                {
                    if (!response.success) return;
                });
                // till here added by shaheer
                PlayerPrefs.SetInt(AUTOLOGIN, Convert.ToInt32(autoLogin.isOn));
                PlayerPrefs.SetString(NICK, nick.text);
                if (autoLogin) PlayerPrefs.SetString(PASS, pass.text);
                UIManager.Instance.SuccessMessage(MESSAGE5);
                SceneManager.LoadScene("Lobby");
            }
            else if (response.text.Contains("wrong email/password"))
            {
                UIManager.Instance.ErrorMessage(MESSAGE3);
            }
            else
            {
                UIManager.Instance.ErrorMessage(MESSAGE4);
            }
        }
        
    }
}
