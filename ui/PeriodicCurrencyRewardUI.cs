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
    public class PeriodicCurrencyRewardUI
    {

        private readonly string COLLECT_BUTTON = "collectBtn";
        private readonly string RESOURCE_ICON = "panel/RewardEntry/resourceAmount/ResourceIcon";
        private readonly string RESOURCE_TEXT = "panel/RewardEntry/resourceAmount/resourcesText";

        private Button _collectButton;
        private ResourceIconUI _resourceIcon;
        private Text _resourceText;

        private UIManager _uiManager;
        private PlayerManager _playerManager;
        private FileManager _fileManager;

        private int _rewardAmount;

        public PeriodicCurrencyRewardUI(GameObject panel, UIManager uiManager, PlayerManager playerManager, FileManager fileManager)
        {
            _uiManager = uiManager;
            _playerManager = playerManager;
            _fileManager = fileManager;

            _collectButton = panel.transform.Find(COLLECT_BUTTON).gameObject.GetComponent<Button>();
            _resourceIcon = panel.transform.Find(RESOURCE_ICON).gameObject.GetComponent<ResourceIconUI>();
            _resourceText = panel.transform.Find(RESOURCE_TEXT).gameObject.GetComponent<Text>();

            _resourceIcon.setIcon("Bucks");

            _collectButton.onClick.AddListener(onCollectClick);
        }

        public void setRewardAmount(int amount)
        {
            _rewardAmount = amount;
            _resourceText.text = StringUtility.toNumString(amount);
        }

        private void onCollectClick()
        {
            _uiManager.hidePeriodicCurrencyRewardPanel();
            _playerManager.player.bucksAmount += _rewardAmount; //This is a special case because it's all the currencies added up.
            _fileManager.saveAll(); //Saving after collecting Periodic currency.
        }

    }
}
