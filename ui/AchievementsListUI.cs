using com.super2games.idle.goals;
using com.super2games.idle.manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class AchievementsListUI
    {
        private readonly string ACHIEVEMENTS_CONTENT_PREFAB = "Prefabs/ui/AchievementsContent";
        private readonly string CLOSE_BUTTON = "exitBtnHotspot";
        private readonly string PAGE_BTNS_CONTAINER = "pageBtnsContainer";

        private readonly int NUM_ENTRIES_PER_PAGE = 10;

        private GameObject _achievementsPanel;
        private PagedUI _pagedUI;
        private GameObject _pageBtnsContainer;

        private Button _closeButton;

        private AchievementsManager _achievementsManager;
        private UIManager _uiManager;
        private PrefabManager _prefabManager;

        private List<AchievementListEntry> _initialListOrder = new List<AchievementListEntry>();

        private Dictionary<string, AchievementListEntry> entries = new Dictionary<string, AchievementListEntry>();

        public AchievementsListUI(GameObject achievementsPanel, AchievementsManager achievementsManager, UIManager uiManager, PrefabManager prefabManager)
        {
            _achievementsPanel = achievementsPanel;
            _achievementsManager = achievementsManager;
            _uiManager = uiManager;
            _prefabManager = prefabManager;
            _pageBtnsContainer = achievementsPanel.transform.Find(PAGE_BTNS_CONTAINER).gameObject;
            _closeButton = achievementsPanel.transform.Find(CLOSE_BUTTON).gameObject.GetComponent<Button>();
            _closeButton.onClick.AddListener(onCloseClick);

            _achievementsManager.onGoalCompleteEvent += onGoalComplete;
            _achievementsManager.onGoalProgressEvent += onGoalProgress;
            _achievementsManager.onGoalFinishedEvent += onGoalFinished;

            _pagedUI = _pageBtnsContainer.GetComponent<PagedUI>();
            _pagedUI.init(ACHIEVEMENTS_CONTENT_PREFAB, _achievementsPanel, _pageBtnsContainer, _prefabManager, NUM_ENTRIES_PER_PAGE);
        }

        private void onCloseClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            _uiManager.hideAchievementsPanel();
        }

        public void populateUI()
        {
            _initialListOrder.Clear();

            foreach (KeyValuePair<string, List<Goal>> pair in _achievementsManager.achievementGoals)
            {
                string goalID = pair.Key;
                Goal goal = _achievementsManager.findCurrentGoal(goalID); //First entry is fine for reference

                if (goal != null && !entries.ContainsKey(goalID))
                {
                    AchievementListEntry entry = new AchievementListEntry(goalID);
                    entry.update(goal);
                    entry.onGetRewardClickEvent += onGetRewardClick;
                    entries.Add(goalID, entry);
                    _initialListOrder.Add(entry);
                }
                else if (goal == null && !entries.ContainsKey(goalID))
                {
                    goal = _achievementsManager.findLastGoal(goalID);
                    AchievementListEntry entry = new AchievementListEntry(goalID);
                    entry.updateToEnd(goal);
                    entry.onGetRewardClickEvent += onGetRewardClick;
                    entries.Add(goalID, entry);
                    _initialListOrder.Add(entry);
                }
            }

            sortEntries();
            _pagedUI.showPage(0);
        }

        public void sortEntries()
        {
            List<AchievementListEntry> sortedList = new List<AchievementListEntry>();

            for (int i=0;i<_initialListOrder.Count;i++)
            {
                AchievementListEntry entry = _initialListOrder[i];

                if (entry.getRewardGO.activeSelf)
                {
                    sortedList.Insert(0, entry);
                }
                else
                {
                    sortedList.Add(entry);
                }
            }

            _pagedUI.clearAllPages(true);

            for (int i = 0; i < sortedList.Count; i++)
            {
                _pagedUI.addEntryToPage(sortedList[i].view);
            }

            _pagedUI.showPage(0);
        }

        private void onGetRewardClick(string goalID)
        {
            entries[goalID].hideGetRewardButton();
            _achievementsManager.retrieveReward(goalID);
        }

        private void onGoalComplete(Goal goal)
        {
            entries[goal.config.id].showGetRewardButton();
            if (!_achievementsPanel.activeSelf)
            {   //Shows only if the panel is not active.
                _uiManager.bottomLeftMenuUI.showAchievementsDot();
            }
        }

        private void onGoalProgress(Goal goal)
        {
            if (entries.ContainsKey(goal.config.id))
            {   //Will not contain the key at start up, but will on every other one.
                entries[goal.config.id].update(goal);
            }
        }

        private void onGoalFinished(string goalID)
        {
            if (!entries.ContainsKey(goalID)) //This happens on start up when the entries haven't been built yet but the data is trying to hit it with the event complete from Achievement Manager.
            {
                return;
            }
            entries[goalID].fillAllStars();
        }



    }
}
