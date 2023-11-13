using com.super2games.idle.config;
using com.super2games.idle.enums;
using com.super2games.idle.factory;
using com.super2games.idle.goals;
using com.super2games.idle.manager;
using com.super2games.idle.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class QuestListUI
    {
        private GameObject _questContent;

        private QuestManager _questManager;
        private UIManager _uiManager;
        private ModelManager _modelManager;
        private ItemsManager _itemsManager;

        private Dictionary<Goal, QuestListEntry> _entries = new Dictionary<Goal, QuestListEntry>();

        private GameObject _exitGO;
        private Button _exitBtnHotspot;

        public QuestListUI(GameObject questPanel, GameObject questContent, QuestManager questManager, UIManager uiManager, ModelManager modelManager, ItemsManager itemsManager)
        {
            _questContent = questContent;
            _questManager = questManager;
            _uiManager = uiManager;
            _modelManager = modelManager;
            _itemsManager = itemsManager;
            _exitGO = questPanel.transform.Find("exitBtn").gameObject;
            _exitBtnHotspot = questPanel.transform.Find("exitBtnHotspot").gameObject.GetComponent<Button>();
            _questManager.onGoalAdded += onGoalAdded;
            _exitBtnHotspot.onClick.AddListener(onExitClick);
        }

        private void onGoalAdded(Goal goal)
        {
            if (!_entries.ContainsKey(goal))
            {
                QuestListEntry entry = new QuestListEntry(_questContent, goal);
                entry.onGetRewardClickEvent += onGetRewardClick;
                _entries.Add(goal, entry);
            }
        }

        private void onGetRewardClick(Goal goal)
        {
            ItemConfig itemConfig = _modelManager.itemsModel.getConfig(goal.config.rewardID) as ItemConfig;
            _uiManager.rewardClaimPanelUI.currentItemConfig = itemConfig;
            _uiManager.rewardClaimPanelUI.currentGoal = goal;
            _uiManager.rewardClaimPanelUI.currentBoostConfig = _itemsManager.getBoostConfigFromItemConfig(itemConfig);

            _uiManager.showRewardClaimPanel();
            _uiManager.rewardClaimPanelUI.startRewardAnim();

            _questManager.nextQuest(goal);
        }

        public void goalComplete(Goal goal)
        {
            _entries[goal].showGetRewardButton();
        }

        public void goalProgress(Goal goal)
        {
            _entries[goal].update();
        }

        public void onExitClick()
        {
            if (!JobFactory.tutorialManager.isUIFunctional(UIEnum.QUESTS_CLOSE)) return;
            _uiManager.hideQuestPanel();
            JobFactory.recordsManager.uiClick(UIEnum.QUESTS_CLOSE);
        }

        public void highlightUI(string uiID, string fingerRotationState)
        {
            if (uiID == UIEnum.QUESTS_CLOSE) _uiManager.createHighlight(_exitGO, fingerRotationState);
        }

    }
}
