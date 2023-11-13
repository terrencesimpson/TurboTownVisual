using com.super2games.idle.config;
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
    public class DailyRewardPanelUI
    {
        private readonly string COLLECT_BUTTON = "collectBtn";
        private readonly string DAYS_GROUP = "daysGroup/";
        private readonly string DAILY_REWARD_DAY_ENTRY = "DailyRewardDayEntry";
        private readonly string YOU_EARNED_TEXT = "youEarnedText/";
        private readonly string RESOURCE_ICON = "resourceAmount/ResourceIcon";
        private readonly string RESOURCES_TEXT = "resourceAmount/resourcesText";
        private readonly string DAY_SELECTED_GRAPHIC = "daySelectedGraphic";

        private GameObject _view;    
        private Button _collectButton;

        private DailyRewardManager _dailyRewardManager;

        public DailyRewardPanelUI(GameObject panel, DailyRewardManager dailyRewardManager)
        {
            _dailyRewardManager = dailyRewardManager;
            _view = panel;
            _collectButton = panel.transform.Find(COLLECT_BUTTON).gameObject.GetComponent<Button>();

            _collectButton.onClick.AddListener(onCollectClick);
        }

        public void populateUI(List<ItemConfig> dailyRewardConfigs, int todayIndex)
        {
            ItemConfig todaysDailyReward = dailyRewardConfigs[todayIndex];
            GameObject daySelectedGraphic = _view.transform.Find(DAYS_GROUP + DAILY_REWARD_DAY_ENTRY + (todayIndex + 1) + "/" + DAY_SELECTED_GRAPHIC).gameObject;
            ResourceIconUI youEarnedIcon = _view.transform.Find(YOU_EARNED_TEXT + RESOURCE_ICON).gameObject.GetComponent<ResourceIconUI>();
            Text youEarnedText = _view.transform.Find(YOU_EARNED_TEXT + RESOURCES_TEXT).gameObject.GetComponent<Text>();

            for (int i = 0; i < dailyRewardConfigs.Count; ++i)
            {
                string path = DAYS_GROUP + DAILY_REWARD_DAY_ENTRY + (i + 1) + "/";
                ResourceIconUI resourceIcon = _view.transform.Find(path + RESOURCE_ICON).gameObject.GetComponent<ResourceIconUI>();
                Text resourceText = _view.transform.Find(path + RESOURCES_TEXT).gameObject.GetComponent<Text>();
                ItemConfig config = dailyRewardConfigs[i];
                resourceIcon.setIcon(config.itemID);
                resourceText.text = StringUtility.toNumString(config.amount);
            }

            daySelectedGraphic.SetActive(true);
            youEarnedIcon.setIcon(todaysDailyReward.itemID);
            youEarnedText.text = StringUtility.toNumString(todaysDailyReward.amount);
        }

        private void onCollectClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            _dailyRewardManager.awardDailyPrize();
        }


    }
}
