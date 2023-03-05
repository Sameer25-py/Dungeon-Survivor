using LootLocker.Requests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonSurvivor.Scenes.Shaheer.Revamp
{
    public class RegisterPage : MonoBehaviour
    {
        [SerializeField] private Button back, register;
        [SerializeField] private HomePage homePage;
        [SerializeField] private TMP_InputField nick, pass;
        [SerializeField] private TMP_Text message;

        private const string MESSAGE1 = "Register New Credentials";
        private const string MESSAGE2 = "Nickname/Password is empty";
        private const string MESSAGE3 = "Password Has to Contain Atleast 8 Characters";
        private const string MESSAGE4 = "Credentials Registered Successfully";
        private const string MESSAGE5 = "Unknown Error Encountered";
        private const string MESSAGE6 = "Nickname already taken";

        private void Start()
        {
            back.onClick.AddListener(Back);
            register.onClick.AddListener(Register);
            Reset();
        }
        public void Reset()
        {
            message.text = MESSAGE1;
            nick.text = "";
            pass.text = "";
        }
        private void Back()
        {
            gameObject.SetActive(false);
            homePage.Reset();
            homePage.gameObject.SetActive(true);
        }
        private void Register()
        {
            // UIManager.Instance.SetWaitingState(true);
            if (nick.text == "" || pass.text == "")
            {
                UIManager.Instance.ErrorMessage(MESSAGE2);
                // UIManager.Instance.SetWaitingState(false);
                return;
            }
            if (pass.text.Length < 8)
            {
                UIManager.Instance.ErrorMessage(MESSAGE3);
                // UIManager.Instance.SetWaitingState(false);
                return;
            }
            LootLockerSDKManager.WhiteLabelSignUp(nick.text.Trim(), pass.text.Trim(), OnSignUpComplete);
            // UIManager.Instance.SetWaitingState(false);
        }

        private void OnSignUpComplete(LootLockerWhiteLabelSignupResponse response)
        {
            if (response.success)
            {
                UIManager.Instance.SuccessMessage(MESSAGE4);
            }
            else if (response.text.Contains("already exists"))
            {
                UIManager.Instance.ErrorMessage(MESSAGE6);
            }
            else
            {
                UIManager.Instance.ErrorMessage(MESSAGE5);
            }
        }
    }
}