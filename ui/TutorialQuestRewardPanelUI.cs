using com.super2games.idle.config;
using com.super2games.idle.factory;
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
    public class TutorialQuestRewardPanelUI : MonoBehaviour
    {

        private readonly string COLLECT_BTN = "collectBtn";

        private readonly string REWARD_ICON_1 = "RewardsLayout/Reward1/RewardIcon";
        private readonly string REWARD_ICON_2 = "RewardsLayout/Reward2/RewardIcon";
        private readonly string REWARD_ICON_3 = "RewardsLayout/Reward3/RewardIcon";

        private readonly string REWARD_TITLE_1 = "RewardsLayout/Reward1/RewardTitle";
        private readonly string REWARD_TITLE_2 = "RewardsLayout/Reward2/RewardTitle";
        private readonly string REWARD_TITLE_3 = "RewardsLayout/Reward3/RewardTitle";

        private readonly string REWARD_PANEL_1 = "RewardsLayout/Reward1";
        private readonly string REWARD_PANEL_2 = "RewardsLayout/Reward2";
        private readonly string REWARD_PANEL_3 = "RewardsLayout/Reward3";

        private readonly string REWARD_LAYOUT_PANEL = "RewardsLayout";

        private readonly string CONGRATS_TEXT = "CongratsText";

        private GameObject _panel;
        private Button _collectBtn;

        private ResourceIconUI _rewardIcon1;
        private ResourceIconUI _rewardIcon2;
        private ResourceIconUI _rewardIcon3;

        private Text _rewardTitle1;
        private Text _rewardTitle2;
        private Text _rewardTitle3;

        private GameObject _rewardPanel1;
        private GameObject _rewardPanel2;
        private GameObject _rewardPanel3;

        private List<ResourceIconUI> _rewardIcons;
        private List<Text> _rewardTitles;
        private List<GameObject> _rewardPanels;

        private GameObject _rewardLayoutPanel;

        private Text _congratsText;

        public TutorialQuestConfig tutorialQuestConfig;

        public void initialize()
        {
            _panel = gameObject;
            _collectBtn = _panel.transform.Find(COLLECT_BTN).gameObject.GetComponent<Button>();
            _collectBtn.onClick.AddListener(onCollectClick);

            _rewardIcon1 = _panel.transform.Find(REWARD_ICON_1).gameObject.GetComponent<ResourceIconUI>();
            _rewardIcon2 = _panel.transform.Find(REWARD_ICON_2).gameObject.GetComponent<ResourceIconUI>();
            _rewardIcon3 = _panel.transform.Find(REWARD_ICON_3).gameObject.GetComponent<ResourceIconUI>();

            _rewardTitle1 = _panel.transform.Find(REWARD_TITLE_1).gameObject.GetComponent<Text>();
            _rewardTitle2 = _panel.transform.Find(REWARD_TITLE_2).gameObject.GetComponent<Text>();
            _rewardTitle3 = _panel.transform.Find(REWARD_TITLE_3).gameObject.GetComponent<Text>();

            _rewardPanel1 = _panel.transform.Find(REWARD_PANEL_1).gameObject;
            _rewardPanel2 = _panel.transform.Find(REWARD_PANEL_2).gameObject;
            _rewardPanel3 = _panel.transform.Find(REWARD_PANEL_3).gameObject;

            _congratsText = _panel.transform.Find(CONGRATS_TEXT).gameObject.GetComponent<Text>();

            _rewardLayoutPanel = _panel.transform.Find(REWARD_LAYOUT_PANEL).gameObject;

            _rewardIcons = new List<ResourceIconUI>();
            _rewardTitles = new List<Text>();
            _rewardPanels = new List<GameObject>();

            _rewardIcons.Add(_rewardIcon1);
            _rewardIcons.Add(_rewardIcon2);
            _rewardIcons.Add(_rewardIcon3);

            _rewardTitles.Add(_rewardTitle1);
            _rewardTitles.Add(_rewardTitle2);
            _rewardTitles.Add(_rewardTitle3);

            _rewardPanels.Add(_rewardPanel1);
            _rewardPanels.Add(_rewardPanel2);
            _rewardPanels.Add(_rewardPanel3);
        }

        public void setRewards(List<ItemConfig> rewards)
        {
            enableRewardPanels(false);
            enableRewardIcons(false);
            enableRewardTitles(false);

            for (int i = 0; i < rewards.Count; ++i)
            {
                ItemConfig itemConfig = rewards[i];
                _rewardPanels[i].gameObject.SetActive(true);
                _rewardIcons[i].gameObject.SetActive(true);
                _rewardTitles[i].gameObject.SetActive(true);
                _rewardIcons[i].setIcon(itemConfig.itemID);
                _rewardTitles[i].text = StringUtility.toNumString(itemConfig.amount);
            }
        }

        public void onShow()
        {
            _congratsText.text = tutorialQuestConfig.rewardText;
            iTween.ShakeScale(_congratsText.gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "z", 1.1f, "time", .66f));
            iTween.ShakeRotation(_congratsText.gameObject, iTween.Hash("z", 20f, "time", .66f));
            iTween.PunchPosition(_rewardLayoutPanel, iTween.Hash("y", -12, "time", 2));
            SoundManager.instance.playSound(SoundManager.CHA_CHING);
        }

        private void enableRewardIcons(bool enabled)
        {
            for (int i = 0; i < _rewardIcons.Count; ++i) _rewardIcons[i].gameObject.SetActive(enabled);
        }

        private void enableRewardTitles(bool enabled)
        {
            for (int i = 0; i < _rewardTitles.Count; ++i) _rewardTitles[i].gameObject.SetActive(enabled);
        }

        private void enableRewardPanels(bool enabled)
        {
            for (int i = 0; i < _rewardPanels.Count; ++i) _rewardPanels[i].gameObject.SetActive(enabled);
        }

        private void onCollectClick()
        {
            JobFactory.itemsManager.giveTutorialQuestReward(tutorialQuestConfig);
            JobFactory.uiManager.hideTutorialQuestRewardPanel();
        }

    }
}
