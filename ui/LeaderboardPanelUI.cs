using com.super2games.idle.enums;
using com.super2games.idle.goals;
using com.super2games.idle.manager;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class LeaderboardPanelUI
    {
        private readonly string CONTENT_PREFAB = "Prefabs/ui/LeaderboardContent";
        private readonly string CLOSE_BUTTON = "exitBtnHotspot";
        private readonly string PAGE_BTNS_CONTAINER = "pageBtnsContainer";
        private readonly string AROUND_ME_TOGGLE = "aroundMeToggle";
        private readonly string LOADING = "Loading";
        private readonly string LEADERBOARD_TYPE_TEXT = "leaderboardTypeText";
        private readonly string LEADERBOARD_TYPE_PREV_BUTTON = "leaderboardTypePrevButton";
        private readonly string LEADERBOARD_TYPE_NEXT_BUTTON = "leaderboardTypeNextButton";

        private readonly int NUM_ENTRIES_PER_PAGE = 15;

        private GameObject _panel;
        private PagedUI _pagedUI;
        private GameObject _pageBtnsContainer;
        private Toggle _aroundMeToggle;
        private Text _loadingText;
        private Button _closeButton;
        private Text _leaderboardTypeText;
        private Button _leaderboardTypePrevButton;
        private Button _leaderboardTypeNextButton;

        private UIManager _uiManager;
        private PrefabManager _prefabManager;
        private PlayFabManager _playFabManager;
        private LeaderboardManager _leaderboardManager;

        private Dictionary<string, AchievementListEntry> entries = new Dictionary<string, AchievementListEntry>();

        public LeaderboardPanelUI(GameObject panel, UIManager uiManager, PrefabManager prefabManager, PlayFabManager playFabManager, LeaderboardManager leaderboardManager)
        {
            _panel = panel;
            _uiManager = uiManager;
            _prefabManager = prefabManager;
            _playFabManager = playFabManager;
            _leaderboardManager = leaderboardManager;

            _pageBtnsContainer = _panel.transform.Find(PAGE_BTNS_CONTAINER).gameObject;
            _aroundMeToggle = _panel.transform.Find(AROUND_ME_TOGGLE).gameObject.GetComponent<Toggle>();
            _loadingText = _panel.transform.Find(LOADING).gameObject.GetComponent<Text>();
            _leaderboardTypeText = _panel.transform.Find(LEADERBOARD_TYPE_TEXT).gameObject.GetComponent<Text>();
            _leaderboardTypePrevButton = _panel.transform.Find(LEADERBOARD_TYPE_PREV_BUTTON).gameObject.GetComponent<Button>();
            _leaderboardTypeNextButton = _panel.transform.Find(LEADERBOARD_TYPE_NEXT_BUTTON).gameObject.GetComponent<Button>();
            _closeButton = _panel.transform.Find(CLOSE_BUTTON).gameObject.GetComponent<Button>();

            _closeButton.onClick.AddListener(onCloseClick);
            _leaderboardTypePrevButton.onClick.AddListener(onLeaderboardTypePrevClick);
            _leaderboardTypeNextButton.onClick.AddListener(onLeaderboardTypeNextClick);

            _aroundMeToggle.onValueChanged.AddListener(onAroundMeToggleChange);

            _playFabManager.onGetLeaderboardSuccessEvent += onGetLeaderboardData;

            _pagedUI = _pageBtnsContainer.GetComponent<PagedUI>();
            _pagedUI.init(CONTENT_PREFAB, _panel, _pageBtnsContainer, _prefabManager, NUM_ENTRIES_PER_PAGE);
        }

        private void onAroundMeToggleChange(bool state)
        {
            _leaderboardManager.showLeaderboard();
        }

        private void onLeaderboardTypePrevClick()
        {
            _leaderboardManager.decreaseIndex();
        }

        private void onLeaderboardTypeNextClick()
        {
            _leaderboardManager.increaseIndex();
        }

        private void onGetLeaderboardData(List<PlayerLeaderboardEntry> dataEntries)
        {
            populateUI(dataEntries);
        }

        private void onCloseClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            _uiManager.hideLeaderboardPanel();
        }

        public void populateUI(List<PlayerLeaderboardEntry> dataEntries)
        {
            clear();

            int playerPageIndex = 0;

            for (int i = 0; i < dataEntries.Count; ++i)
            {
                PlayerLeaderboardEntry dataEntry = dataEntries[i];
                LeaderboardEntry entry = new LeaderboardEntry();
                entry.update(dataEntry.DisplayName, dataEntry.StatValue.ToString("N0"), (dataEntry.Position + 1).ToString("N0"));
                _pagedUI.addEntryToPage(entry.view);
                if (_playFabManager.playFabID == dataEntry.PlayFabId)
                {
                    entry.colorEntryMe();
                    playerPageIndex = int.Parse(_pagedUI.numberOfPages().ToString()) - 1; //For whatever reason a direct link to the .Count prop of a list is an object reference even though it's a primitive?
                }
            }

            if (_aroundMeToggle.isOn) _pagedUI.showPage(playerPageIndex);
            else _pagedUI.showPage();
            
            hideLoadingText();
        }

        public void clear()
        {
            _pagedUI.clearAllPages(true);
        }

        public void updateLeaderboardTypeText(string type)
        {
            _leaderboardTypeText.text = LeaderboardTypeEnum.getLeaderboardName(type);
        }

        public bool isAroundMe()
        {
            return _aroundMeToggle.isOn;
        }

        public void showLoadingText()
        {
            _loadingText.gameObject.SetActive(true);
        }

        public void hideLoadingText()
        {
            _loadingText.gameObject.SetActive(false);
        }

    }
}
