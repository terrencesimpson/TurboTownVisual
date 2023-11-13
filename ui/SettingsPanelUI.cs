using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using com.super2games.idle.manager;
using com.super2games.idle.enums;
using com.super2games.idle.factory;
using com.super2games.idle.utilties;

namespace com.super2games.idle.ui
{
    public class SettingsPanelUI
    {
        private readonly string CLOSE_BUTTON = "exitBtnHotspot";
        private readonly string HELP_BUTTON = "helpBtn";
        private readonly string TIPS_BUTTON = "tipsBtn";
        private readonly string ACCOUNT_MANAGEMENT_BUTTON = "accountManagementBtn";

        private readonly string PLAY_FAB_ID = "playfabID";

        private readonly string ZOOM_SPEED_SLIDER = "zoomSpeedEntry/zoomSpeedSlider";
        private readonly string PAN_SPEED_SLIDER = "panSpeedEntry/panSpeedSlider";

        private readonly string MUSIC_SLIDER = "musicEntry/musicSlider";
        private readonly string SOUND_SLIDER = "soundEntry/soundSlider";

        private readonly string MUSIC_TOGGLE = "musicEntry/musicToggle";
        private readonly string SOUND_TOGGLE = "soundEntry/soundToggle";
        private readonly string TIPS_TOGGLE = "tipsEntry/tipsToggle";

        private readonly string QUALITY_SETTINGS_DROPDOWN = "QualitySettingsDropdown";

        private readonly string SHOW_ANIMATIONS_TOGGLE = "showAnimationsEntry/toggle";
        private readonly string SHOW_JOB_REWARD_POPUPS_TOGGLE = "showJobRewardPopupsEntry/toggle";
        private readonly string SHOW_PARTICLE_EFFECTS_TOGGLE = "showParticleEffectsEntry/toggle";
        private readonly string SHOW_SHADOWS_TOGGLE = "showShadowsEntry/toggle";

        private readonly string FACEBOOK_BTN = "facebookBtn";
        private readonly string TWITTER_BTN = "twitterBtn";
        private readonly string S2G_WEBSITE_BTN = "s2gWebsiteBtn";

        private Text _playfabID;

        private Button _closeButton;
        private Button _helpButton;
        private Button _tipsButton;
        private Button _accountManagementButton;

        private Slider _zoomSpeedSlider;
        private Slider _panSpeedSlider;

        private Slider _musicSlider;
        private Slider _soundSlider;

        private Toggle _musicToggle;
        private Toggle _soundToggle;
        private Toggle _tipsToggle;
        private Toggle _showAnimationsToggle;
        private Toggle _showJobRewardPopupsToggle;
        private Toggle _showParticleEffectsToggle;
        private Toggle _showShadowsToggle;

        private Button _facebookBtn;
        private Button _twitterBtn;
        private Button _s2gWebsiteBtn;

        private UIManager _uiManager;
        private SettingsManager _settingsManager;

        private Dropdown _qualitySettingsDropdown;

        public SettingsPanelUI(GameObject panel, SettingsManager settingsManager, UIManager uiManager, string playfabIDString)
        {
            _uiManager = uiManager;
            _settingsManager = settingsManager;

            //TODO: Get game object references and build out UI. Check other UI classes for reference.
            _closeButton = panel.transform.Find(CLOSE_BUTTON).gameObject.GetComponent<Button>();
            _closeButton.onClick.AddListener(onCloseClick);

            _helpButton = panel.transform.Find(HELP_BUTTON).gameObject.GetComponent<Button>();
            _helpButton.onClick.AddListener(onHelpClick);

            _tipsButton = panel.transform.Find(TIPS_BUTTON).gameObject.GetComponent<Button>();
            _tipsButton.onClick.AddListener(onTipsClick);

            _accountManagementButton = panel.transform.Find(ACCOUNT_MANAGEMENT_BUTTON).gameObject.GetComponent<Button>();
            _accountManagementButton.onClick.AddListener(onAccountManagementClick);

            //if (PlatformUtility.isWebGL()) _accountManagementButton.gameObject.SetActive(false); //We don't want web users to be able to transfer accounts at iOS launch

            _soundToggle = panel.transform.Find(SOUND_TOGGLE).gameObject.GetComponent<Toggle>();
            _soundToggle.onValueChanged.AddListener(onSoundToggle);

            _musicToggle = panel.transform.Find(MUSIC_TOGGLE).gameObject.GetComponent<Toggle>();
            _musicToggle.onValueChanged.AddListener(onMusicToggle);

            _tipsToggle = panel.transform.Find(TIPS_TOGGLE).gameObject.GetComponent<Toggle>();
            _tipsToggle.onValueChanged.AddListener(onTipsToggle);

            _zoomSpeedSlider = panel.transform.Find(ZOOM_SPEED_SLIDER).gameObject.GetComponent<Slider>();
            _zoomSpeedSlider.onValueChanged.AddListener(onZoomSpeedSlider);

            _panSpeedSlider = panel.transform.Find(PAN_SPEED_SLIDER).gameObject.GetComponent<Slider>();
            _panSpeedSlider.onValueChanged.AddListener(onPanSpeedSlider);

            _soundSlider = panel.transform.Find(SOUND_SLIDER).gameObject.GetComponent<Slider>();
            _soundSlider.onValueChanged.AddListener(onSoundSlider);

            _musicSlider = panel.transform.Find(MUSIC_SLIDER).gameObject.GetComponent<Slider>();
            _musicSlider.onValueChanged.AddListener(onMusicSlider);

            _playfabID = panel.transform.Find(PLAY_FAB_ID).gameObject.GetComponent<Text>();

            _qualitySettingsDropdown = panel.transform.Find(QUALITY_SETTINGS_DROPDOWN).gameObject.GetComponent<Dropdown>();

            _showAnimationsToggle = panel.transform.Find(SHOW_ANIMATIONS_TOGGLE).gameObject.GetComponent<Toggle>();
            _showJobRewardPopupsToggle = panel.transform.Find(SHOW_JOB_REWARD_POPUPS_TOGGLE).gameObject.GetComponent<Toggle>();
            _showParticleEffectsToggle = panel.transform.Find(SHOW_PARTICLE_EFFECTS_TOGGLE).gameObject.GetComponent<Toggle>();
            _showShadowsToggle = panel.transform.Find(SHOW_SHADOWS_TOGGLE).gameObject.GetComponent<Toggle>();

            _facebookBtn = panel.transform.Find(FACEBOOK_BTN).gameObject.GetComponent<Button>();
            _twitterBtn = panel.transform.Find(TWITTER_BTN).gameObject.GetComponent<Button>();
            _s2gWebsiteBtn = panel.transform.Find(S2G_WEBSITE_BTN).gameObject.GetComponent<Button>();

            _facebookBtn.onClick.AddListener(facebookButtonClick);
            _twitterBtn.onClick.AddListener(twitterButtonClick);
            _s2gWebsiteBtn.onClick.AddListener(s2gWebsiteButtonClick);

            _showAnimationsToggle.onValueChanged.AddListener(onShowAnimationsToggle);
            _showJobRewardPopupsToggle.onValueChanged.AddListener(onShowJobRewardPopupsToggle);
            _showParticleEffectsToggle.onValueChanged.AddListener(onShowParticleEffectsToggle);
            _showShadowsToggle.onValueChanged.AddListener(onShowShadowsToggle);

            string[] names = QualitySettings.names;
            List<string> namesList = new List<string>();
            for (int i = 0; i < names.Length; ++i)
            {
                namesList.Add(names[i]);
            }

            _qualitySettingsDropdown.ClearOptions();
            _qualitySettingsDropdown.AddOptions(namesList);
            _qualitySettingsDropdown.onValueChanged.AddListener(delegate { onQualitySettingsChange(); });
            _qualitySettingsDropdown.value = namesList.Count - 1;

            if (playfabIDString != null && playfabIDString != "")
            {
                _playfabID.text = "ID: " + playfabIDString;
            }
            else
            {
                _playfabID.text = "ID: Not logged in.";
            }
        }

        private void facebookButtonClick()
        {
            Application.OpenURL("https://www.facebook.com/super2games/");
        }

        private void twitterButtonClick()
        {
            Application.OpenURL("https://twitter.com/super2games");
        }

        private void s2gWebsiteButtonClick()
        {
            Application.OpenURL("http://www.super2games.com");
        }

        private void onQualitySettingsChange()
        {
            JobFactory.settingsManager.setQualitySetting(_qualitySettingsDropdown.value);
        }

        public void setStates(float soundSliderValue, float musicSliderValue, bool tipToggleState, bool musicToggleState, bool soundToggleState, bool animationState, bool resourcePopups, bool particleSystems, int qualitySetting, float zoomSliderValue, float panSliderValue)
        {
            _soundSlider.value = soundSliderValue;
            _musicSlider.value = musicSliderValue;
            _tipsToggle.isOn = tipToggleState;
            _musicToggle.isOn = musicToggleState;
            _soundToggle.isOn = soundToggleState;
            _showAnimationsToggle.isOn = animationState;
            _showJobRewardPopupsToggle.isOn = resourcePopups;
            _showParticleEffectsToggle.isOn = particleSystems;
            _qualitySettingsDropdown.value = qualitySetting;

            _zoomSpeedSlider.value = zoomSliderValue;
            _panSpeedSlider.value = panSliderValue;
        }

        private void onZoomSpeedSlider(float value)
        {
            MouseCameraControl.setZoomSpeed(value);
        }

        private void onPanSpeedSlider(float value)
        {
            MouseCameraControl.setPanSpeed(value);
        }

        private void onSoundSlider(float value)
        {
            _settingsManager.setSoundVolume(value);
        }

        private void onMusicSlider(float value)
        {
            _settingsManager.setMusicVolume(value);
        }

        private void onShowAnimationsToggle(bool toggled)
        {
            _settingsManager.setAnimationsEnabled(toggled);
        }

        private void onShowJobRewardPopupsToggle(bool toggled)
        {
            _settingsManager.setResourcePopupsEnabled(toggled);
        }

        private void onShowParticleEffectsToggle(bool toggled)
        {
            _settingsManager.setParticleSystemsEnabled(toggled);
        }

        private void onShowShadowsToggle(bool toggled)
        {
            _settingsManager.setShadowsEnabled(toggled);
        }

        private void onSoundToggle(bool toggled)
        {
            _settingsManager.setSoundEnabled(toggled);
        }

        private void onMusicToggle(bool toggled)
        {
            _settingsManager.setMusicEnabled(toggled);
        }

        private void onTipsToggle(bool toggled)
        {
            _settingsManager.setTipBoxEnabled(toggled);

            if (!toggled)
            {
                _uiManager.hideTipBoxPanel();
            }
            else
            {
                _uiManager.showTipBoxLastTip();
            }
        }

        private void onCloseClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            _uiManager.hideSettingsPanel();
        }

        private void onHelpClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            _uiManager.hideSettingsPanel();
            _uiManager.hideAllPanels();
            _uiManager.showHelpPanel();
        }

        private void onTipsClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            _uiManager.hideSettingsPanel();
            _uiManager.showTipBoxLastTip(true);
        }

        private void onAccountManagementClick()
        {
            _uiManager.accountManagementPanelUI.showAccountManagementOrLogin();
        }

    }
}
