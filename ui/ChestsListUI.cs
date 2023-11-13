using com.super2games.idle.config;
using com.super2games.idle.manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using com.super2games.idle.enums;
using com.super2games.idle.component.possessor;
using com.super2games.idle.component.goods;
using com.super2games.idle.utilities;
using com.super2games.idle.factory;

namespace com.super2games.idle.ui
{
    public class ChestsListUI
    {

        private readonly string CLOSE_BUTTON_HOTSPOT = "closeButtonHotspot";
        private readonly string CLOSE_BUTTON = "closeButton";
        private readonly string BRONZE_PANEL = "Bronze";
        private readonly string SILVER_PANEL = "Silver";
        private readonly string GOLD_PANEL = "Gold";
        private readonly string DESCRIPTION = "description";
        private readonly string FREE_TIMER = "briefcase_01/freeTimerText";
        private readonly string BUY_1_BTN = "briefcase_01/1_PurchaseBtn";
        private readonly string FREE_BTN = "briefcase_01/1_PurchaseBtn/freeBtn";
        private readonly string BUY_10_BTN = "briefcase_02/2_PurchaseBtn";
        private readonly string COST_1_TEXT = "briefcase_01/1_Cost/cost_1_Text";
        private readonly string COST_10_TEXT = "briefcase_02/2_Cost/cost_10_Text";

        private GameObject _closeGO;
        
        private Button _closeButton;
        private Button _bronze_1_btn;
        private Button _bronze_10_btn;
        private Button _silver_1_btn;
        private Button _silver_10_btn;
        private Button _gold_1_btn;
        private Button _gold_10_btn;

        private GameObject _bronzeFreeBtn;
        private GameObject _silverFreeBtn;
        private GameObject _goldFreeBtn;

        private Text _bronze_cost_1_text;
        private Text _bronze_cost_10_text;
        private Text _silver_cost_1_text;
        private Text _silver_cost_10_text;
        private Text _gold_cost_1_text;
        private Text _gold_cost_10_text;

        private Text _bronzeDescription;
        private Text _silverDescription;
        private Text _goldDescription;

        private Text _bronzeFreeTimeText;
        private Text _silverFreeTimeText;
        private Text _goldFreeTimeText;

        private GameObject _bronzePanel;
        private GameObject _silverPanel;
        private GameObject _goldPanel;

        private UIManager _uiManager;
        private ItemsManager _itemsManager;
        private ModelManager _modelManager;
        private PlayerManager _playerManager;

        private Player _player;

        public ChestsListUI(GameObject chestsPanel, UIManager uiManager, ModelManager modelManager, ItemsManager itemsManager, PlayerManager playerManager)
        {
            _uiManager = uiManager;
            _modelManager = modelManager;
            _itemsManager = itemsManager;
            _playerManager = playerManager;
            _player = playerManager.player;

            _closeGO = chestsPanel.transform.Find(CLOSE_BUTTON).gameObject;
            _closeButton = chestsPanel.transform.Find(CLOSE_BUTTON_HOTSPOT).gameObject.GetComponent<Button>();
            _closeButton.onClick.AddListener(onCloseClick);

            _bronzePanel = chestsPanel.transform.Find(BRONZE_PANEL).gameObject;
            _bronzeDescription = _bronzePanel.transform.Find(DESCRIPTION).gameObject.GetComponent<Text>();
            _bronzeFreeTimeText = _bronzePanel.transform.Find(FREE_TIMER).gameObject.GetComponent<Text>();
            _bronze_1_btn = _bronzePanel.transform.Find(BUY_1_BTN).gameObject.GetComponent<Button>();
            _bronze_10_btn = _bronzePanel.transform.Find(BUY_10_BTN).gameObject.GetComponent<Button>();
            _bronzeFreeBtn = _bronzePanel.transform.Find(FREE_BTN).gameObject;
            _bronze_cost_1_text = _bronzePanel.transform.Find(COST_1_TEXT).gameObject.GetComponent<Text>();
            _bronze_cost_10_text = _bronzePanel.transform.Find(COST_10_TEXT).gameObject.GetComponent<Text>();

            _silverPanel = chestsPanel.transform.Find(SILVER_PANEL).gameObject;
            _silverDescription = _silverPanel.transform.Find(DESCRIPTION).gameObject.GetComponent<Text>();
            _silverFreeTimeText = _silverPanel.transform.Find(FREE_TIMER).gameObject.GetComponent<Text>();
            _silver_1_btn = _silverPanel.transform.Find(BUY_1_BTN).gameObject.GetComponent<Button>();
            _silver_10_btn = _silverPanel.transform.Find(BUY_10_BTN).gameObject.GetComponent<Button>();
            _silverFreeBtn = _silverPanel.transform.Find(FREE_BTN).gameObject;
            _silver_cost_1_text = _silverPanel.transform.Find(COST_1_TEXT).gameObject.GetComponent<Text>();
            _silver_cost_10_text = _silverPanel.transform.Find(COST_10_TEXT).gameObject.GetComponent<Text>();

            _goldPanel = chestsPanel.transform.Find(GOLD_PANEL).gameObject;
            _goldDescription = _goldPanel.transform.Find(DESCRIPTION).gameObject.GetComponent<Text>();
            _goldFreeTimeText = _goldPanel.transform.Find(FREE_TIMER).gameObject.GetComponent<Text>();
            _gold_1_btn = _goldPanel.transform.Find(BUY_1_BTN).gameObject.GetComponent<Button>();
            _gold_10_btn = _goldPanel.transform.Find(BUY_10_BTN).gameObject.GetComponent<Button>();
            _goldFreeBtn = _goldPanel.transform.Find(FREE_BTN).gameObject;
            _gold_cost_1_text = _goldPanel.transform.Find(COST_1_TEXT).gameObject.GetComponent<Text>();
            _gold_cost_10_text = _goldPanel.transform.Find(COST_10_TEXT).gameObject.GetComponent<Text>();

            ChestConfig bronze = _modelManager.chestsModel.getConfig(ChestTypeEnum.BRONZE) as ChestConfig;
            ChestConfig silver = _modelManager.chestsModel.getConfig(ChestTypeEnum.SILVER) as ChestConfig;
            ChestConfig gold = _modelManager.chestsModel.getConfig(ChestTypeEnum.GOLD) as ChestConfig;

            _bronzeDescription.text = bronze.description;

            _silverDescription.text = silver.description;
            _silver_cost_1_text.text = StringUtility.toNumString(silver.price);
            _silver_cost_10_text.text = StringUtility.toNumString((silver.price * 10) * (1 - silver.discount));

            _goldDescription.text = gold.description;
            _gold_cost_1_text.text = StringUtility.toNumString(gold.price);
            _gold_cost_10_text.text = StringUtility.toNumString((gold.price * 10) * (1 - gold.discount));

            _bronze_1_btn.onClick.AddListener(onBronze1Click);
            _bronze_10_btn.onClick.AddListener(onBronze10Click);
            _silver_1_btn.onClick.AddListener(onSilver1Click);
            _silver_10_btn.onClick.AddListener(onSilver10Click);
            _gold_1_btn.onClick.AddListener(onGold1Click);
            _gold_10_btn.onClick.AddListener(onGold10Click);
        }

        public void updateBronzeCasePrice()
        {
            ChestConfig bronze = _modelManager.chestsModel.getConfig(ChestTypeEnum.BRONZE) as ChestConfig;
            _bronze_cost_1_text.text = StringUtility.toNumString(_itemsManager.casePurchasePrice(ChestTypeEnum.BRONZE, 1));
            _bronze_cost_10_text.text = StringUtility.toNumString(_itemsManager.casePurchasePrice(ChestTypeEnum.BRONZE, 10));
        }

        public void postInitialize()
        {
            updateBronzeCasePrice(); //Player props doesn't exist in constructor
            _player.onPropertiesChange += onPlayerPropertiesChange;
        }

        private void onPlayerPropertiesChange(Inventory inventory)
        {
            _bronzeFreeTimeText.text = "Free In: " + StringUtility.formatSeconds(_player.bronzeCaseFreeInTime);
            _silverFreeTimeText.text = "Free In: " + StringUtility.formatSeconds(_player.silverCaseFreeInTime);
            _goldFreeTimeText.text = "Free In: " + StringUtility.formatSeconds(_player.goldCaseFreeInTime);

            _bronzeFreeBtn.SetActive(false);
            _silverFreeBtn.SetActive(false);
            _goldFreeBtn.SetActive(false);

            if (_player.bronzeCaseFreeInTime <= 0) //Shouldn't ever be less than, safety check.
            {
                _bronzeFreeTimeText.text = "Free!";
                _bronzeFreeBtn.SetActive(true);
                _uiManager.bottomLeftMenuUI.showChestsDot();
            }

            if (_player.silverCaseFreeInTime <= 0)
            {
                _silverFreeTimeText.text = "Free!";
                _silverFreeBtn.SetActive(true);
                _uiManager.bottomLeftMenuUI.showChestsDot();
            }

            if (_player.goldCaseFreeInTime <= 0)
            {
                _goldFreeTimeText.text = "Free!";
                _goldFreeBtn.SetActive(true);
                _uiManager.bottomLeftMenuUI.showChestsDot();
            }
        }

        public void highlightUI(string uiID, string fingerRotationState)
        {
            if (uiID == UIEnum.CASES_CLOSE) _uiManager.createHighlight(_closeGO, fingerRotationState);
            else if (uiID == UIEnum.CASE_OPEN) _uiManager.createHighlight(_bronze_1_btn.gameObject, fingerRotationState); //For tutorial we just want to highlight the bronze case button, but they could click on any button if they have the resources.
        }

        private void onBronze1Click()
        {
            if (!JobFactory.tutorialManager.isComplete) JobFactory.uiManager.removeHighlights();
            onCase1Click(_player.freeBronzeCaseByTime, ChestTypeEnum.BRONZE);
        }

        private void onBronze10Click()
        {
            chestButtonClick(10, ChestTypeEnum.BRONZE);
        }

        private void onSilver1Click()
        {
            onCase1Click(_player.freeSilverCaseByTime, ChestTypeEnum.SILVER);
        }

        private void onSilver10Click()
        {
            chestButtonClick(10, ChestTypeEnum.SILVER);
        }

        private void onGold1Click()
        {
            onCase1Click(_player.freeGoldCaseByTime, ChestTypeEnum.GOLD);
        }

        private void onGold10Click()
        {
            chestButtonClick(10, ChestTypeEnum.GOLD);
        }

        private void onCase1Click(double freeCaseCount, string caseType)
        {
            bool isFree = (freeCaseCount > 0); 
            chestButtonClick(1, caseType, isFree);
            if (isFree)
            {
                _playerManager.resetCaseTimer(caseType);
            }
        }

        private void chestButtonClick(int totalNumRewards, string chestType, bool isFree = false)
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            if (_itemsManager.canPurchaseChest(chestType, totalNumRewards) && !isFree)
            {
                if (chestType == ChestTypeEnum.SILVER && totalNumRewards == 1) JobFactory.playFabManager.analytics(AnalyticsEnum.SINGLE_SILVER_CASE_PURCHASED);
                if (chestType == ChestTypeEnum.SILVER && totalNumRewards == 10) JobFactory.playFabManager.analytics(AnalyticsEnum.TEN_SILVER_CASES_PURCHASED);
                if (chestType == ChestTypeEnum.GOLD && totalNumRewards == 1) JobFactory.playFabManager.analytics(AnalyticsEnum.SINGLE_GOLD_CASE_PURCHASED);
                if (chestType == ChestTypeEnum.GOLD && totalNumRewards == 10) JobFactory.playFabManager.analytics(AnalyticsEnum.TEN_GOLD_CASES_PURCHASED);
            }
            _itemsManager.reveal(totalNumRewards, chestType, isFree);
        }

        private void onCloseClick()
        {
            if (!JobFactory.tutorialManager.isUIFunctional(UIEnum.CASES_CLOSE)) return;
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            _uiManager.hideChestsPanel();
            JobFactory.recordsManager.uiClick(UIEnum.CASES_CLOSE);
        }

    }
}
