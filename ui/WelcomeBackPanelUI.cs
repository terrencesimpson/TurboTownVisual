using com.super2games.idle.component.goods;
using com.super2games.idle.config;
using com.super2games.idle.enums;
using com.super2games.idle.manager;
using com.super2games.idle.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Advertisements;

namespace com.super2games.idle.ui
{
    public class WelcomeBackPanelUI
    {
        private readonly string CLOSE_BUTTON = "Panel/closeButtonHotspot";

        private readonly string SPENT_COLUMN = "Panel/spentColumn";
        private readonly string EARNED_COLUMN = "Panel/earnedColumn";
        private readonly string NET_COLUMN = "Panel/netColumn";
        private readonly string TIME_AWAY = "Panel/timeAwayText";
        private readonly string UPGRADE_TIME_AWAY_BUTTON = "Panel/upgradeTimeAwayButton";
        private readonly string UPGRADE_TIME_AWAY_PRICE_TEXT = "Panel/upgradeTimeAwayButton/priceText";
        private readonly string DETAILS_TEXT = "Panel/detailsText";

		public static readonly int SECONDS_NEEDED_TO_SHOW = 60;

        private GameObject _spendColumn;
        private GameObject _earnedColumn;
        private GameObject _netColumn;

        private RecordsManager _recordsManager;
        private PrefabManager _prefabManager;
        private UIManager _uiManager;
        private StartUpManager _startUpManager;
        private ModelManager _modelManager;
        private ItemsManager _itemsManager;

        private Button _closeButton;
        private Text _timeAwayText;
        private Button _upgradeTimeAwayButton;
        private Text _upgradeTimeAwayPriceText;
        private Text _detailsText;

        private List<ResourceEntryUI> _entries = new List<ResourceEntryUI>(); 

        public WelcomeBackPanelUI(GameObject panel, GameObject spentColumn, GameObject earnedColumn, GameObject netColumn, RecordsManager recordsManager, PrefabManager prefabManager, UIManager uiManager, StartUpManager startUpManager, ModelManager modelManager, ItemsManager itemsManager)
        {
            _closeButton = panel.transform.Find(CLOSE_BUTTON).gameObject.GetComponent<Button>();
            _closeButton.onClick.AddListener(onCloseClick);

            _timeAwayText = panel.transform.Find(TIME_AWAY).gameObject.GetComponent<Text>();

            _upgradeTimeAwayButton = panel.transform.Find(UPGRADE_TIME_AWAY_BUTTON).gameObject.GetComponent<Button>();
            _upgradeTimeAwayButton.onClick.AddListener(onUpgradeTimeAwayClick);

            _upgradeTimeAwayPriceText = panel.transform.Find(UPGRADE_TIME_AWAY_PRICE_TEXT).gameObject.GetComponent<Text>();

            _detailsText = panel.transform.Find(DETAILS_TEXT).gameObject.GetComponent<Text>();

            _spendColumn = spentColumn;
            _earnedColumn = earnedColumn;
            _netColumn = netColumn;
            _recordsManager = recordsManager;
            _prefabManager = prefabManager;
            _uiManager = uiManager;
            _startUpManager = startUpManager;
            _modelManager = modelManager;
            _itemsManager = itemsManager;
        }

        private void onUpgradeTimeAwayClick()
        {
            _itemsManager.purchaseCatchUpTimeUpgrade();
            updateCatchUpPriceText();
        }

        private void onCloseClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            _uiManager.hideWelcomeBackPanel();
        }

        public void populateUI()
        {
            clear();

            _spendColumn.transform.DetachChildren();
            _earnedColumn.transform.DetachChildren();
            _netColumn.transform.DetachChildren();

            Dictionary<string, double> resourcesNet = InventoryUtility.resourcesNet(_recordsManager.resourcesEarned, _recordsManager.resourcesSpent);

            buildEntries(_recordsManager.resourcesEarned, _earnedColumn);
            buildEntries(_recordsManager.resourcesSpent, _spendColumn);
            buildEntries(resourcesNet, _netColumn, 1, true);

            updateCatchUpPriceText();
        }

        public void postInitialize()
        {
            updateCatchUpPriceText();
        }

        public void updateCatchUpPriceText()
        {
            _timeAwayText.text = StringUtility.formatSeconds(_startUpManager.catchUpTimeAway) + " / " + StringUtility.formatSeconds(_itemsManager.getCatchUpTimeLimit());
            if (_itemsManager.canPurchaseTimeAwayUpgrade())
            {
                _detailsText.text = "Add " + StringUtility.formatSecondsToMinutesLongName(_itemsManager.getCatchUpTimeIncreaseTimeIncrement()) + " Permanently";
                _upgradeTimeAwayPriceText.text = _itemsManager.getCatchUpTimeUpgradePriceText();
            }
            else
            {
                _detailsText.gameObject.SetActive(false);
                _upgradeTimeAwayPriceText.gameObject.SetActive(false);
                _upgradeTimeAwayButton.gameObject.SetActive(false);
            }
        }

        private void buildEntries(Dictionary<string, double> resources, GameObject column, int multiplier = 1, bool showColor = false)
        {
            foreach (KeyValuePair<string, double> pair in resources)
            {
                addEntryToParent(pair.Key, pair.Value, column, multiplier, false, showColor);
            }
            addEntryToParent("", 0, column, 0, true);
        }

        private void addEntryToParent(string key, double value, GameObject column, int multiplier, bool isSpacer = false, bool showColor = false)
        {
            ResourceEntryUI entry;
            if (isSpacer)
            {
                entry = new ResourceEntryUI(_prefabManager, key, value, isSpacer);
            }
            else
            {
                entry = new ResourceEntryUI(_prefabManager, key, (value * multiplier), isSpacer, showColor);
            }
            entry.view.transform.SetParent(column.transform, false);
            _entries.Add(entry);
        }

        private void clear()
        {
            for (int i = 0; i < _entries.Count; ++i)
            {
                _entries[i].returnToPrefabManager();
            }
            _entries.Clear();
        }

    }
}
