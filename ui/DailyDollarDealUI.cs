using com.super2games.idle.component.possessor;
using com.super2games.idle.config;
using com.super2games.idle.enums;
using com.super2games.idle.factory;
using com.super2games.idle.manager;
using com.super2games.idle.utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class DailyDollarDealUI
    {
        private readonly int SHOW_AFTER = 10800; //3 Hours: 10800

        private readonly string ITEM_TITLE = "DDDContainer/itemTitle";
        private readonly string BOOSTS_CONTAINER = "DDDContainer/BoostsContainer";
        private readonly string BRIEFCASE_SILVER = "DDDContainer/BriefcaseSilver";
        private readonly string BRIEFCASE_GOLD = "DDDContainer/BriefcaseGold";
        private readonly string PERIODIC_CONTAINER = "DDDContainer/PeriodicContainer";
        private readonly string BUCKS_CONTAINER = "DDDContainer/BucksContainer";
        private readonly string TIME_WARP_CONTAINER = "DDDContainer/TimeWarpContainer";
        private readonly string CONFIRM_BUTTON = "DDDContainer/ConfirmBtn";
        private readonly string CANCEL_BUTTON = "DDDContainer/CancelBtn";
        private readonly string BOOST_ITEM_ICON = "DDDContainer/BoostsContainer/BoostItemIcon";

        private readonly int NUM_BOOST_ITEM_ICONS = 5;

        private GameObject _dailyDollarDealPanel;
        private Text _itemTitle;
        private GameObject _boostContainer;
        private GameObject _briefcaseSilver;
        private GameObject _briefcaseGold;
        private GameObject _periodicContainer;
        private GameObject _bucksContainer;
        private GameObject _timeWarpContainer;
        private Button _confirmButton;
        private Button _cancelButton;

        private List<BoostItemIconUI> _boostIcons = new List<BoostItemIconUI>();

        private Player _player;

        private UIManager _uiManager;
        private ItemsManager _itemsManager;

        private ShopConfig _shopConfig;

        private Button _dailyDollarDealButton;

        public DailyDollarDealUI(GameObject panel, Button dailyDollarDealButton, Player player, UIManager uiManager, ItemsManager itemsManager)
        {
            _dailyDollarDealPanel = panel;
            _dailyDollarDealButton = dailyDollarDealButton;
            _player = player;
            _uiManager = uiManager;
            _itemsManager = itemsManager;

            _itemTitle = panel.transform.Find(ITEM_TITLE).gameObject.GetComponent<Text>();
            _boostContainer = panel.transform.Find(BOOSTS_CONTAINER).gameObject;
            _briefcaseSilver = panel.transform.Find(BRIEFCASE_SILVER).gameObject;
            _briefcaseGold = panel.transform.Find(BRIEFCASE_GOLD).gameObject;
            _periodicContainer = panel.transform.Find(PERIODIC_CONTAINER).gameObject;
            _bucksContainer = panel.transform.Find(BUCKS_CONTAINER).gameObject;
            _timeWarpContainer = panel.transform.Find(TIME_WARP_CONTAINER).gameObject;
            _confirmButton = panel.transform.Find(CONFIRM_BUTTON).gameObject.GetComponent<Button>();
            _cancelButton = panel.transform.Find(CANCEL_BUTTON).gameObject.GetComponent<Button>();

            for (int i = 1; i <= NUM_BOOST_ITEM_ICONS; ++i)
            {
                _boostIcons.Add(panel.transform.Find(BOOST_ITEM_ICON + i).gameObject.GetComponent<BoostItemIconUI>());
            }

            _dailyDollarDealButton.onClick.AddListener(onDailyDollarDealButtonClick);

            _confirmButton.onClick.AddListener(onConfirmClick);
            _cancelButton.onClick.AddListener(onCancelClick);

            hideAllIcons();
        }

        private void onDailyDollarDealButtonClick()
        {
            _itemsManager.showDailyDollarDeal();
            JobFactory.playFabManager.analytics(AnalyticsEnum.DDD_BUTTON_CLICKED);
        }

        public void hideDailyDollarDealButton()
        {
            _dailyDollarDealButton.gameObject.SetActive(false);
        }

        public void showDailyDollarDealButton()
        {
            _dailyDollarDealButton.gameObject.SetActive(true);
        }

        public void showOffer(ShopConfig shopConfig)
        {
            if (_player.secondsInGame < SHOW_AFTER)
            {
                return;
            }
            _shopConfig = shopConfig;
            hideAllIcons();
            _uiManager.showDailyDollarDealPanel();
            showItem(shopConfig.thirdPartyID);
            checkBoostsUI();
            showDailyDollarDealButton();
            _itemTitle.text = CurrencyUtility.getDescription(shopConfig); 
        }

        private void checkBoostsUI()
        {
            if (_shopConfig.thirdPartyID == KongregatePurchasableEnum.TT_DDD_5_BUILDING_BOOSTS || _shopConfig.thirdPartyID == KongregatePurchasableEnum.TT_DDD_3_PLAYER_BOOSTS)
            {
                ItemConfig itemConfig = JobFactory.modelManager.itemsModel.getConfig(_shopConfig.itemID) as ItemConfig;
                int length = itemConfig.itemPool.Length;
                for (int i = 1; i <= NUM_BOOST_ITEM_ICONS; ++i)
                {
                    _dailyDollarDealPanel.transform.Find(BOOST_ITEM_ICON + i).gameObject.SetActive(false);
                }
                for (int i = 0; i < length; ++i)
                {
                    int index = (length < NUM_BOOST_ITEM_ICONS) ? (index = i + 1) : (index = i);
                    int iconIndex = (length < NUM_BOOST_ITEM_ICONS) ? (iconIndex = i + 2) : (iconIndex = i + 1);
                    BoostConfig boostConfig = JobFactory.itemsManager.getBoostConfigFromItemConfig(JobFactory.modelManager.itemsModel.getConfig(itemConfig.itemPool[i]) as ItemConfig);
                    _dailyDollarDealPanel.transform.Find(BOOST_ITEM_ICON + iconIndex).gameObject.SetActive(true);
                    _boostIcons[index].setIcon(boostConfig.id, boostConfig.imgPath, boostConfig.rank);
                }
            }
        }

        private void onConfirmClick()
        {
            _uiManager.hideDailyDollarDealPanel();
            _itemsManager.purchaseShopItem(_shopConfig);
            JobFactory.playFabManager.analytics(AnalyticsEnum.DDD_BUY_NOW_CLICKED);
        }

        private void onCancelClick()
        {
            _uiManager.hideDailyDollarDealPanel();
            JobFactory.playFabManager.analytics(AnalyticsEnum.DDD_NO_THANKS_CLICKED);
        }

        private void hideAllIcons()
        {
            _boostContainer.SetActive(false);
            _briefcaseSilver.SetActive(false);
            _briefcaseGold.SetActive(false);
            _periodicContainer.SetActive(false);
            _bucksContainer.SetActive(false);
            _timeWarpContainer.SetActive(false);
        }

        private void showItem(string kongID)
        {
            if (kongID == KongregatePurchasableEnum.TT_DDD_500_BUCKS) _bucksContainer.SetActive(true);
            else if (kongID == KongregatePurchasableEnum.TT_DDD_5_GOLD_CASES) _briefcaseGold.SetActive(true);
            else if (kongID == KongregatePurchasableEnum.TT_DDD_10_SILVER_CASES) _briefcaseSilver.SetActive(true);
            else if (kongID == KongregatePurchasableEnum.TT_DDD_MONTHLY_PERIODIC) _periodicContainer.SetActive(true);
            else if (kongID == KongregatePurchasableEnum.TT_DDD_5_BUILDING_BOOSTS || kongID == KongregatePurchasableEnum.TT_DDD_3_PLAYER_BOOSTS) _boostContainer.SetActive(true);
            else if (kongID == KongregatePurchasableEnum.TT_DDD_12_HOUR_TIME_WARP) _timeWarpContainer.SetActive(true);
        }


    }
}
