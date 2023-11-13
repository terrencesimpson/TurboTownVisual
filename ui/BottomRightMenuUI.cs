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
    public class BottomRightMenuUI : MonoBehaviour
    {
        private readonly Color NORMAL_COLOR = new Color(1,1,1);
        private readonly Color DISABLED_COLOR = new Color(.5f, .5f, .5f);
        private readonly Color ENABLED_COLOR = new Color(0, 1, 0);
        private readonly float KEY_PRESS_BUFFER = .2f;

        private readonly string RECORDS_BUTTON = "recordsBtn";
        private readonly string BUILDING_LIST_BUTTON = "buildingBtn";
        private readonly string SETTINGS_BUTTON = "settingsBtn";
        private readonly string COLLAPSE_BUTTON = "collapseBtn";
        private readonly string BULLDOZER_BUTTON = "bulldozerBtn";
        private readonly string LEADERBOARD_BUTTON = "leaderboardBtn";
        private readonly string RESOURCES_BUTTON = "resourcesBtn";

        private UIManager _uiManager;
        private BuildingManager _buildingManager;
        private LeaderboardManager _leaderboardManager;

        private Button _recordsButton;
        private Button _buildingsListButton;
        private Button _settingsButton;
        private Button _collapseButton;
        private Button _bulldozerButton;
        private Button _leaderboardButton;
        private Button _resourcesButton;

        private List<Button> _buttons = new List<Button>();

        private Image _buildingsListImage;
        private Image _bulldozerImage;

        private GameObject _panel;

        private float _keyPressBuffer = 0f;
        private bool _hasStarted = false;

        public bool isCollapsed = true;

        public void initialize(GameObject panel, UIManager uiManager, BuildingManager buildingManager, LeaderboardManager leaderboardManager)
        {
            _panel = panel;
            _uiManager = uiManager;
            _buildingManager = buildingManager;
            _leaderboardManager = leaderboardManager;

            _recordsButton = panel.transform.Find(RECORDS_BUTTON).gameObject.GetComponent<Button>();
            _buildingsListButton = panel.transform.Find(BUILDING_LIST_BUTTON).gameObject.GetComponent<Button>();
            _settingsButton = panel.transform.Find(SETTINGS_BUTTON).gameObject.GetComponent<Button>();
            _collapseButton = panel.transform.Find(COLLAPSE_BUTTON).gameObject.GetComponent<Button>();
            _bulldozerButton = panel.transform.Find(BULLDOZER_BUTTON).gameObject.GetComponent<Button>();
            _leaderboardButton = panel.transform.Find(LEADERBOARD_BUTTON).gameObject.GetComponent<Button>();
            _resourcesButton = panel.transform.Find(RESOURCES_BUTTON).gameObject.GetComponent<Button>();

            _buildingsListImage = panel.transform.Find(BUILDING_LIST_BUTTON).gameObject.GetComponent<Image>();
            _bulldozerImage = panel.transform.Find(BULLDOZER_BUTTON).gameObject.GetComponent<Image>();

            _recordsButton.onClick.AddListener(onRecordsClick);
            _buildingsListButton.onClick.AddListener(onBuildingsListClick);
            _settingsButton.onClick.AddListener(onSettingsButtonClick);
            _collapseButton.onClick.AddListener(onCollapseButtonClick);
            _bulldozerButton.onClick.AddListener(onBulldozerButtonClick);
            _leaderboardButton.onClick.AddListener(onLeaderboardClick);
            _resourcesButton.onClick.AddListener(onResourcesClick);

            _buttons.Add(_recordsButton);
            _buttons.Add(_buildingsListButton);
            _buttons.Add(_settingsButton);
            _buttons.Add(_collapseButton);
            _buttons.Add(_bulldozerButton);
            _buttons.Add(_leaderboardButton);
            _buttons.Add(_resourcesButton);

            setBuildListState();

            _hasStarted = true;
        }

        void Update()
        {
            if (!_hasStarted)
            {
                return;
            }

            _keyPressBuffer += Time.deltaTime;

            if (_keyPressBuffer < KEY_PRESS_BUFFER || !_panel.activeInHierarchy)
            {
                return;
            }

            if (InputManager.key_down_1)
            {
                onBuildingsListClick();
                _keyPressBuffer = 0;
            }
            else if (InputManager.key_down_2)
            {
                onBulldozerButtonClick();
                _keyPressBuffer = 0;
            }
            else if (InputManager.key_down_7)
            {
                onRecordsClick();
                _keyPressBuffer = 0;
            }
            else if (InputManager.key_down_8)
            {
                onLeaderboardClick();
                _keyPressBuffer = 0;
            }
            else if (InputManager.key_down_9)
            {
                onSettingsButtonClick();
                _keyPressBuffer = 0;
            }
        }

        public void highlightUI(string uiID, string fingerRotationState)
        {
            if (uiID == UIEnum.BUILD) _uiManager.createHighlight(_buildingsListButton.gameObject, fingerRotationState);
            else if (uiID == UIEnum.BULLDOZE) _uiManager.createHighlight(_bulldozerButton.gameObject, fingerRotationState);
            else if (uiID == UIEnum.RECORDS) _uiManager.createHighlight(_recordsButton.gameObject, fingerRotationState);
            else if (uiID == UIEnum.SETTINGS) _uiManager.createHighlight(_settingsButton.gameObject, fingerRotationState);
        }

        public void setButtonEnablementByID(string uiID, bool enabled)
        {
            if (uiID == UIEnum.ALL_RIGHT) setAllButtonsEnablement(enabled);
            else if (uiID == UIEnum.BUILD) setButtonEnablement(_buildingsListButton, enabled);
            else if (uiID == UIEnum.RECORDS) setButtonEnablement(_recordsButton, enabled);
            else if (uiID == UIEnum.SETTINGS) setButtonEnablement(_settingsButton, enabled);
            else if (uiID == UIEnum.COLLAPSE_RIGHT) setButtonEnablement(_collapseButton, enabled);
            else if (uiID == UIEnum.BULLDOZE) setButtonEnablement(_bulldozerButton, enabled);
            else if (uiID == UIEnum.LEADERBOARDS) setButtonEnablement(_leaderboardButton, enabled);
            else if (uiID == UIEnum.RESOURCES) setButtonEnablement(_resourcesButton, enabled);
        }

        private void setButtonEnablement(Button button, bool enabled)
        {
            button.enabled = enabled;
            if (enabled) lighten(button.gameObject);
            else darken(button.gameObject);
        }

        public void setAllButtonsEnablement(bool enabled)
        {
            for (int i = 0; i < _buttons.Count; ++i)
            {
                setButtonEnablement(_buttons[i], enabled);
            }
        }

        private void lighten(GameObject go)
        {
            go.GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }

        private void darken(GameObject go)
        {
            go.GetComponent<Image>().color = new Color(.4f, .4f, .4f);
        }

        private void onBulldozerButtonClick()
        {
            if (!JobFactory.tutorialManager.isUIFunctional(UIEnum.BULLDOZE)) return;

            JobFactory.recordsManager.uiClick(UIEnum.BULLDOZE);
            setBulldozerState();
        }

        public void setBuildListState()
        {
            FXManager.instance.hideGridVisualization();
            _buildingManager.toggleBuildOrBulldozerState(true);
        }

        public void setBulldozerState()
        {
            FXManager.instance.hideGridVisualization();
            _uiManager.hideBuildingListPanel();
            _buildingManager.toggleBuildOrBulldozerState(false);
        }

        private void onBuildingsListClick()
        {
            if (!JobFactory.tutorialManager.isUIFunctional(UIEnum.BUILD)) return;

            setBuildListState();

            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            if (_uiManager.buildingListPanel.activeSelf)
            {
                _uiManager.hideBuildingListPanel();
            }
            else
            {
                _uiManager.hideAllPanels();
                _uiManager.showBuildingListPanel();
            }
            JobFactory.recordsManager.uiClick(UIEnum.BUILD);
        }

        private void onCollapseButtonClick()
        {
            if (!JobFactory.tutorialManager.isUIFunctional(UIEnum.COLLAPSE_RIGHT)) return;

            if (iTween.Count(_uiManager.bottomRightMenuPanel) > 0 || iTween.Count(_collapseButton.gameObject) > 0)
            {
                return;
            }

            transitionCollapse();

            JobFactory.recordsManager.uiClick(UIEnum.COLLAPSE_RIGHT);
        }

        public void transitionCollapse()
        {
            int XPosExpanded = -34;
            int XPosClosed = 205;

            RectTransform panelRectTransform = _panel.GetComponent<RectTransform>();

            if (isCollapsed)
            {
                //iTween.MoveTo(_panel, iTween.Hash("x", XPosExpanded, "easeType", "easeInOutCubic", "time", .75f, "isLocal", false));//"isLocal", true));

                iTween.ValueTo(_panel, iTween.Hash(
                "from", panelRectTransform.anchoredPosition,
                "to", new Vector2(XPosExpanded, panelRectTransform.anchoredPosition.y),
                "easetype", "easeInOutCubic",
                "time", .75f,
                "onupdatetarget", this.gameObject,
                "onupdate", "movePanel"));


                iTween.RotateTo(_collapseButton.gameObject, iTween.Hash("z", 0, "easeType", "easeInOutCubic", "time", .75f, "isLocal", true));
                isCollapsed = false;
            }
            else
            {
                //iTween.MoveTo(_panel, iTween.Hash("x", XPosClosed, "easeType", "easeInOutCubic", "time", .75f, "isLocal", false));//"isLocal", true));

                iTween.ValueTo(_panel, iTween.Hash(
                "from", panelRectTransform.anchoredPosition,
                "to", new Vector2(XPosClosed, panelRectTransform.anchoredPosition.y),
                "easetype", "easeInOutCubic",
                "time", .75f,
                "onupdatetarget", this.gameObject,
                "onupdate", "movePanel"));


                iTween.RotateTo(_collapseButton.gameObject, iTween.Hash("z", 180, "easeType", "easeInOutCubic", "time", .75f, "isLocal", true));
                isCollapsed = true;
            }
        }

        private void movePanel(Vector2 position)
        {
            RectTransform panelRectTransform = _panel.GetComponent<RectTransform>();
            panelRectTransform.anchoredPosition = position;
        }

        private void onRecordsClick()
        {
            if (!JobFactory.tutorialManager.isUIFunctional(UIEnum.RECORDS)) return;

            setBuildListState();
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            if (_uiManager.recordsPanel.activeSelf)
            {
                _uiManager.hideRecordsPanel();
            }
            else
            {
                _uiManager.hideAllPanels();
                _uiManager.showRecordsPanel();
            }
            JobFactory.recordsManager.uiClick(UIEnum.RECORDS);
        }

        private void onResourcesClick()
        {
            if (!JobFactory.tutorialManager.isUIFunctional(UIEnum.RESOURCES)) return;

            setBuildListState();
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            if (_uiManager.playerInventoryPanel.activeSelf && !_uiManager.buildingFunctionPanel.activeSelf)
            {
                _uiManager.hidePlayerResourcesPanel();
            }
            else if (!_uiManager.playerInventoryPanel.activeSelf)
            {
                _uiManager.hideAllPanels(); 
                _uiManager.showPlayerResourcesPanel(); 
            }
            JobFactory.recordsManager.uiClick(UIEnum.RESOURCES);
        }

        private void onLeaderboardClick()
        {
            if (!JobFactory.tutorialManager.isUIFunctional(UIEnum.LEADERBOARDS)) return;

            setBuildListState();
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            if (_uiManager.leaderboardPanel.activeSelf)
            {
                _uiManager.hideLeaderboardPanel();
            }
            else
            {
                _uiManager.hideAllPanels();
                _leaderboardManager.showLeaderboard();
            }
            JobFactory.recordsManager.uiClick(UIEnum.LEADERBOARDS);
        }

        private void onSettingsButtonClick()
        {
            if (!JobFactory.tutorialManager.isUIFunctional(UIEnum.SETTINGS)) return;

            setBuildListState();
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            if (_uiManager.settingsPanel.activeSelf)
            {
                _uiManager.hideSettingsPanel();
            }
            else
            {
                _uiManager.hideAllPanels();
                _uiManager.showSettingsPanel();
            }
            JobFactory.recordsManager.uiClick(UIEnum.SETTINGS);
        }

        public void onBuildingListButtonMouseEnter()
        {
            string text = (PlatformUtility.isMobile()) ? "Build" : "Build (1)";
            setTooltip(text, _buildingsListButton);
        }

        public void onBulldozerButtonMouseEnter()
        {
            string text = (PlatformUtility.isMobile()) ? "Bulldozer" : "Bulldozer (2)";
            setTooltip(text, _bulldozerButton);
        }

        public void onRecordsButtonMouseEnter()
        {
            string text = (PlatformUtility.isMobile()) ? "Records" : "Records (7)";
            setTooltip(text, _recordsButton);
        }

        public void onLeaderboardButtonMouseEnter()
        {
            string text = (PlatformUtility.isMobile()) ? "Leaderboard" : "Leaderboard (8)";
            setTooltip(text, _leaderboardButton);
        }

        public void onSettingsButtonMouseEnter()
        {
            string text = (PlatformUtility.isMobile()) ? "Settings" : "Settings (9)"; 
            setTooltip(text, _settingsButton);
        }

        public void onResourcesButtonMouseEnter()
        {
            string text = (PlatformUtility.isMobile()) ? "Resources" : "Resources (0)";
            setTooltip(text, _resourcesButton);
        }

        private void setTooltip(string text, Button button)
        {
            TooltipUI.instance.displaySimpleTooltip(text, button.gameObject.transform.position, TooltipUI.TOOLTIP_TYPE_GENERIC, "", 50, 0);
        }

        public void onButtonMouseOut()
        {
            TooltipUI.instance.hideTooltip();
        }
    }
}
