using com.super2games.idle.component.goods;
using com.super2games.idle.enums;
using com.super2games.idle.factory;
using com.super2games.idle.manager;
using com.super2games.idle.utilities;
using com.super2games.idle.utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class AccountManagementPanelUI
    {
        private readonly int PASSWORD_LENGTH = 6;

        private readonly int NUM_SESSIONS_TO_SHOW_ACCOUNT_MANAGEMENT = 10;

        private readonly string ACCOUNT_MANAGEMENT = "AccountManagement";
        private readonly string LOGIN = "Login";
        private readonly string WELCOME_TO_TURBO_TOWN = "WelcomeToTurboTown";
        private readonly string ACCOUNT_MANAGEMENT_OR_LOGIN = "AccountManagementOrLogin";

        private readonly string LEADERBOARD_NAME_INPUT_FIELD = "LeaderboardNameInputField";
        private readonly string SET_LEADERBOARD_NAME_BTN = "SetLeaderboardNameBtn";
        private readonly string USER_NAME_INPUT_FIELD = "UserNameInputField";
        private readonly string PASSWORD_INPUT_FIELD = "PasswordInputField";
        private readonly string EMAIL_INPUT_FIELD = "EmailInputField";
        private readonly string SET_ACCOUNT_INFO_BTN = "SetAccountInfoBtn";

        private readonly string LOGIN_BTN = "LoginBtn";
        private readonly string FORGOT_USERNAME_OR_PASSWORD_BTN = "ForgotUsernameOrPasswordBtn";

        private readonly string LETS_GO_BTN = "LetsGoBtn";

        private readonly string ACCOUNT_MANAGEMENT_BTN = "AccountManagementBtn";

        private readonly string CLOSE_BUTTON = "exitBtnHotspot";

        private readonly double ACCOUNT_INFO_BUCKS_AMOUNT = 30;
        private readonly double LEADERBOARD_NAME_BUCKS_AMOUNT = 10;

        private GameObject _accountManagement;
        private GameObject _login;
        private GameObject _welcomeToTurboTown;
        private GameObject _accountManagementOrLogin;

        private InputField _leaderboardNameInputField_AccountManagement;
        private Button _setLeaderboardNameBtn_AccountManagement;
        private InputField _userNameInputField_AccountManagement;
        private InputField _passwordInputField_AccountManagement;
        private InputField _emailInputField_AccountManagement;
        private Button _setAccountInfoBtn_AccountManagement;
        private Button _closeButton_AccountManagement;

        private InputField _emailInputField_Login;
        private InputField _passwordInputField_Login;
        private Button _loginBtn_Login;
        private Button _forgotUsernameOrPasswordBtn_Login;
        private Button _closeButton_Login;

        private Button _letsGoBtn_WelcomeToTurboTown;
        private Button _loginBtn_WelcomeToTurboTown;

        private Button _accountManagementBtn_AccountManagementOrLogin;
        private Button _loginBtn_AccountManagementOrLogin;
        private Button _closeButton_AccountManagementOrLogin;


        public AccountManagementPanelUI(GameObject panel)
        {
            setupAccountManagement(panel);
            setupLogin(panel);
            setupWelcomeToTurboTown(panel);
            setupAccountManagementOrLogin(panel);
        }

        private void setupAccountManagement(GameObject panel)
        {
            _accountManagement = panel.transform.Find(ACCOUNT_MANAGEMENT).gameObject;

            _leaderboardNameInputField_AccountManagement = _accountManagement.transform.Find(LEADERBOARD_NAME_INPUT_FIELD).gameObject.GetComponent<InputField>();
            _setLeaderboardNameBtn_AccountManagement = _accountManagement.transform.Find(SET_LEADERBOARD_NAME_BTN).gameObject.GetComponent<Button>();
            _userNameInputField_AccountManagement = _accountManagement.transform.Find(USER_NAME_INPUT_FIELD).gameObject.GetComponent<InputField>();
            _passwordInputField_AccountManagement = _accountManagement.transform.Find(PASSWORD_INPUT_FIELD).gameObject.GetComponent<InputField>();
            _emailInputField_AccountManagement = _accountManagement.transform.Find(EMAIL_INPUT_FIELD).gameObject.GetComponent<InputField>();
            _setAccountInfoBtn_AccountManagement = _accountManagement.transform.Find(SET_ACCOUNT_INFO_BTN).gameObject.GetComponent<Button>();

            _closeButton_AccountManagement = _accountManagement.transform.Find(CLOSE_BUTTON).gameObject.GetComponent<Button>();
            _closeButton_AccountManagement.onClick.AddListener(onAccountManagementCloseClick);

            _setLeaderboardNameBtn_AccountManagement.onClick.AddListener(onSetLeaderboardNameClick);
            _setAccountInfoBtn_AccountManagement.onClick.AddListener(onSetAccountInfoClick);
        }

        private void setupLogin(GameObject panel)
        {
            _login = panel.transform.Find(LOGIN).gameObject;

            _emailInputField_Login = _login.transform.Find(EMAIL_INPUT_FIELD).gameObject.GetComponent<InputField>();
            _passwordInputField_Login = _login.transform.Find(PASSWORD_INPUT_FIELD).gameObject.GetComponent<InputField>();

            _loginBtn_Login = _login.transform.Find(LOGIN_BTN).gameObject.GetComponent<Button>();
            _forgotUsernameOrPasswordBtn_Login = _login.transform.Find(FORGOT_USERNAME_OR_PASSWORD_BTN).gameObject.GetComponent<Button>();

            _closeButton_Login = _login.transform.Find(CLOSE_BUTTON).gameObject.GetComponent<Button>();
            _closeButton_Login.onClick.AddListener(onLoginCloseClick);

            _loginBtn_Login.onClick.AddListener(onLoginLoginClick);
            _forgotUsernameOrPasswordBtn_Login.onClick.AddListener(onLoginForgotUsernameOrPasswordClick);
        }

        private void setupWelcomeToTurboTown(GameObject panel)
        {
            _welcomeToTurboTown = panel.transform.Find(WELCOME_TO_TURBO_TOWN).gameObject;

            _letsGoBtn_WelcomeToTurboTown = _welcomeToTurboTown.transform.Find(LETS_GO_BTN).gameObject.GetComponent<Button>();
            _loginBtn_WelcomeToTurboTown = _welcomeToTurboTown.transform.Find(LOGIN_BTN).gameObject.GetComponent<Button>();

            _letsGoBtn_WelcomeToTurboTown.onClick.AddListener(onWelcomeToTurboTownLetsGoClick);
            _loginBtn_WelcomeToTurboTown.onClick.AddListener(onWelcomeToTurboTownLoginClick);
        }

        private void setupAccountManagementOrLogin(GameObject panel)
        {
            _accountManagementOrLogin = panel.transform.Find(ACCOUNT_MANAGEMENT_OR_LOGIN).gameObject;

            _accountManagementBtn_AccountManagementOrLogin = _accountManagementOrLogin.transform.Find(ACCOUNT_MANAGEMENT_BTN).gameObject.GetComponent<Button>();
            _loginBtn_AccountManagementOrLogin = _accountManagementOrLogin.transform.Find(LOGIN_BTN).gameObject.GetComponent<Button>();

            _closeButton_AccountManagementOrLogin = _accountManagementOrLogin.transform.Find(CLOSE_BUTTON).gameObject.GetComponent<Button>();
            _closeButton_AccountManagementOrLogin.onClick.AddListener(onAccountManagementOrLoginCloseClick);

            _accountManagementBtn_AccountManagementOrLogin.onClick.AddListener(onAccountManagementOrLoginAccountManagementClick);
            _loginBtn_AccountManagementOrLogin.onClick.AddListener(onAccountManagementOrLoginLoginClick);
        }

        public void hideAll()
        {
            _accountManagement.SetActive(false);
            _login.SetActive(false);
            _welcomeToTurboTown.SetActive(false);
            _accountManagementOrLogin.SetActive(false);
        }

        public void showAccountManagementOrLogin()
        {
            show();
            _accountManagementOrLogin.SetActive(true);
        }

        public void showWelcomeToTurboTown()
        {
            show();
            JobFactory.fxManager.setStatusText("");
            _welcomeToTurboTown.SetActive(true);
        }

        public void showLogin()
        {
            show();
            _login.SetActive(true);
        }

        public void showAccountManagement()
        {
            show();
            _accountManagement.SetActive(true);
        }

        public void show()
        {
            hideAll();
            JobFactory.uiManager.showAccountManagementPanel();
            InputManager.active = false;
        }

        public void showAccountManagementOnStartUp()
        {
            if (PlatformUtility.isMobile() && JobFactory.tutorialManager.isComplete && JobFactory.player.numSessions > NUM_SESSIONS_TO_SHOW_ACCOUNT_MANAGEMENT && (!JobFactory.itemsManager.hasCollectedAccountInfoEntryReward || !JobFactory.itemsManager.hasCollectedLeaderboardUsernameReward))
            {
                showAccountManagement();
            }
        }

        private void onAccountManagementOrLoginCloseClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            JobFactory.uiManager.hideAccountManagementPanel();
        }

        private void onLoginCloseClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            if (StartUpManager.zoomedInToGameState) JobFactory.uiManager.hideAccountManagementPanel(); //We are in the game, so just close this panel.
            else showWelcomeToTurboTown(); //If we aren't in the game, then we must be a new player and to "go back" to the Welcome popup.
        }

        private void onAccountManagementCloseClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            JobFactory.uiManager.hideAccountManagementPanel();
        }

        private void onSetLeaderboardNameClick()
        {
            string nameFieldValue = _leaderboardNameInputField_AccountManagement.text;
            if (nameFieldValue != "")
            {
                JobFactory.playFabManager.onDisplayNameChangeSuccessEvent += onDisplayNameChangeSuccess;
                JobFactory.playFabManager.saveDisplayName(nameFieldValue);
            }
        }

        private void onSetAccountInfoClick()
        {
            string emailFieldValue = _emailInputField_AccountManagement.text;
            string passwordFieldValue = _passwordInputField_AccountManagement.text;
            string nameFieldValue = _userNameInputField_AccountManagement.text;
            if (emailFieldValue != "" && nameFieldValue != "" && passwordFieldValue != "" && StringUtility.isValidEmailAddress(emailFieldValue))
            {
                JobFactory.playFabManager.onAddUsernameAndPasswordSuccessEvent += onAddUsernameAndPasswordSuccess;
                JobFactory.playFabManager.addUsernamePassword(nameFieldValue, passwordFieldValue, emailFieldValue);
            }
            else
            {
                JobFactory.uiManager.alertPopupUI.showAlert("Either your email or name field(s) are invalid. You must enter a Name too.", "Account Information Error");
            }
        }

        private void onAddUsernameAndPasswordSuccess()
        {
            JobFactory.playFabManager.onAddUsernameAndPasswordSuccessEvent -= onAddUsernameAndPasswordSuccess;
            if (!JobFactory.itemsManager.hasCollectedAccountInfoEntryReward)
            {
                JobFactory.itemsManager.hasCollectedAccountInfoEntryReward = true;
                JobFactory.player.inventory.addItem(new Item(ResourceEnum.BUCKS, ItemTypeEnum.RESOURCE, "", null, ACCOUNT_INFO_BUCKS_AMOUNT));
                JobFactory.uiManager.alertPopupUI.showAlert("Thanks for entering your account information! You can now play your game on multiple devices. Your Reward has been applied! :-)", "Reward!");
            }
        }

        private void onDisplayNameChangeSuccess()
        {
            JobFactory.playFabManager.onDisplayNameChangeSuccessEvent -= onDisplayNameChangeSuccess;
            if (!JobFactory.itemsManager.hasCollectedLeaderboardUsernameReward)
            {
                JobFactory.itemsManager.hasCollectedLeaderboardUsernameReward = true;
                JobFactory.player.inventory.addItem(new Item(ResourceEnum.BUCKS, ItemTypeEnum.RESOURCE, "", null, LEADERBOARD_NAME_BUCKS_AMOUNT));
                JobFactory.uiManager.alertPopupUI.showAlert("Thanks for entering your leaderboard name! You can now see your name on the Leaderboards (wait an hour to update). Your Reward has been applied! :-)", "Reward!");
            }
        }

        private void onWelcomeToTurboTownLetsGoClick()
        {
            JobFactory.startUpManager.checkToLogin();
        }

        private void onWelcomeToTurboTownLoginClick()
        {
            showLogin();
        }

        private void onAccountManagementOrLoginAccountManagementClick()
        {
            showAccountManagement();
        }

        private void onAccountManagementOrLoginLoginClick()
        {
            showLogin();
        }

        private void onLoginLoginClick()
        {
            string emailFieldValue = _emailInputField_Login.text;
            string passwordFieldValue = _passwordInputField_Login.text;
            if (emailFieldValue != "" && passwordFieldValue != "" && StringUtility.isValidEmailAddress(emailFieldValue))
            {
				JobFactory.uiManager.hideAllPanels ();
                JobFactory.startUpManager.loginWithEmail(emailFieldValue, passwordFieldValue);
            }
        }

        private void onLoginForgotUsernameOrPasswordClick()
        {
            string emailFieldValue = _emailInputField_Login.text;
            if (emailFieldValue == "" || StringUtility.isValidEmailAddress(emailFieldValue))
            {
                JobFactory.uiManager.alertPopupUI.showAlert("Enter a valid email address.", "Account Recovery Error");
                return;
            }
            JobFactory.playFabManager.sendAccountRecoveryEmail(emailFieldValue);
        }

    }
}
