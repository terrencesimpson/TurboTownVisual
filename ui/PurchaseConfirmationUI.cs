using com.super2games.idle.config;
using com.super2games.idle.manager;
using com.super2games.idle.utilities;
using com.super2games.idle.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using com.super2games.idle.factory;
using com.super2games.idle.utilties;

namespace com.super2games.idle.ui
{
    public class PurchaseConfirmationUI
    {
        private readonly string BOOST_ITEM_ICON_DISPLAY = "panel/purchasedItem/BoostItemIconDisplay";
        private readonly string PURCHASED_ITEM_RESOURCE_AMOUNT = "panel/purchasedItem/resourceAmount";
        private readonly string PURCHASED_ITEM_RESOURCE_ICON = "panel/purchasedItem/resourceAmount/ResourceIcon";
        private readonly string PURCHASED_ITEM_RESOURCES_TEXT = "panel/purchasedItem/resourceAmount/resourcesText";
        private readonly string PURCHASED_ITEM = "panel/purchasedItem";
        private readonly string CURRENCY_ITEM_RESOURCE_ICON = "panel/currency/resourceAmount/ResourceIcon";
        private readonly string CURRENCY_ITEM_RESOURCES_TEXT = "panel/currency/resourceAmount/resourcesText";
        private readonly string CONFIRM_BUTTON = "panel/ConfirmBtns/ConfirmBtn";
        private readonly string CANCEL_BUTTON = "panel/ConfirmBtns/CancelBtn";
        private readonly string DESCRIPTION_TEXT = "panel/descriptionText";

        private readonly string DESCRIPTION_STRING = "Would you like to purchase ";

        private ShopListUI _shopListUI;

        private GameObject _boostItemIconDisplayGO;
        private GameObject _purchasedItemResourceAmountGO;
        private GameObject _purchasedItemResourceIconGO;

        private BoostItemIconUI _purchasedBoostItemIcon;

        private GameObject _purchasedItemGO;
        private Text _purchasedItemText;
        private ResourceIconUI _purchasedItemIcon;

        private Text _currencyItemText;
        private ResourceIconUI _currencyItemIcon;

        private Button _confirmButton;
        private Button _cancelButton;

        private Text _descriptionText;

        private GameObject _parent;

        private UIManager _uiManager;
        private ItemsManager _itemsManager;

        private bool _isInitialized = false;

        private ShopConfig _shopConfig;
        private ItemConfig _itemConfig;
        private BoostConfig _boostConfig;

        public PurchaseConfirmationUI(GameObject parent, UIManager uiManager, ItemsManager itemsManager, ShopListUI shopListUI)
        {
            _parent = parent;
            _uiManager = uiManager;
            _itemsManager = itemsManager;
            _shopListUI = shopListUI;
        }

        private void initialize()
        {
            _purchasedItemGO = _parent.transform.Find(PURCHASED_ITEM).gameObject;
            _boostItemIconDisplayGO = _parent.transform.Find(BOOST_ITEM_ICON_DISPLAY).gameObject;
            _purchasedItemResourceAmountGO = _parent.transform.Find(PURCHASED_ITEM_RESOURCE_AMOUNT).gameObject;
            _purchasedItemResourceIconGO = _parent.transform.Find(PURCHASED_ITEM_RESOURCE_ICON).gameObject;

            _descriptionText = _parent.transform.Find(DESCRIPTION_TEXT).gameObject.GetComponent<Text>();

            _purchasedBoostItemIcon = _parent.transform.Find(BOOST_ITEM_ICON_DISPLAY).gameObject.GetComponent<BoostItemIconUI>();

            _purchasedItemText = _parent.transform.Find(PURCHASED_ITEM_RESOURCES_TEXT).gameObject.GetComponent<Text>();
            _purchasedItemIcon = _parent.transform.Find(PURCHASED_ITEM_RESOURCE_ICON).gameObject.GetComponent<ResourceIconUI>();

            _currencyItemText = _parent.transform.Find(CURRENCY_ITEM_RESOURCES_TEXT).gameObject.GetComponent<Text>();
            _currencyItemIcon = _parent.transform.Find(CURRENCY_ITEM_RESOURCE_ICON).gameObject.GetComponent<ResourceIconUI>();

            _confirmButton = _parent.transform.Find(CONFIRM_BUTTON).gameObject.GetComponent<Button>();
            _cancelButton = _parent.transform.Find(CANCEL_BUTTON).gameObject.GetComponent<Button>();

            _confirmButton.onClick.AddListener(onConfirmClick);
            _cancelButton.onClick.AddListener(onCancelClick);

            _isInitialized = true;
        }
        
        public void populateUI(ShopConfig shopConfig, ItemConfig itemConfig, BoostConfig boostConfig)
        {
            if (!_isInitialized)
            {
                initialize();
            }

            _shopConfig = shopConfig;
            _itemConfig = itemConfig;
            _boostConfig = boostConfig;

            _currencyItemText.text = CurrencyUtility.getPrice(shopConfig).ToString();
            _currencyItemIcon.setIcon(CurrencyUtility.getCurrency(shopConfig));

            _descriptionText.text = DESCRIPTION_STRING;
            _purchasedItemGO.SetActive(true);

            if (_boostConfig == null && itemConfig != null && itemConfig.type != ItemTypeEnum.PERIODIC)
            {
                activateResourcePurchase();
            }
            else if (_boostConfig != null)
            {
                activateBoostPurchase(boostConfig);
            }
            else if (itemConfig != null && itemConfig.type == ItemTypeEnum.PERIODIC)
            {
                activatePeriodicPurchase(itemConfig);
            }
        }

        private void activatePeriodicPurchase(ItemConfig itemConfig)
        {
            _purchasedItemResourceAmountGO.SetActive(true);
            _boostItemIconDisplayGO.SetActive(false);
            _purchasedItemResourceIconGO.SetActive(false);
            _purchasedItemGO.SetActive(false);

            _descriptionText.text = DESCRIPTION_STRING + itemConfig.description;

            _purchasedItemText.text = StringUtility.toNumString(_itemConfig.amount);

            _purchasedItemIcon.setIcon(_itemConfig.itemID);
        }

        private void activateResourcePurchase()
        {
            _purchasedItemResourceAmountGO.SetActive(true);
            _boostItemIconDisplayGO.SetActive(false);
            _purchasedItemResourceIconGO.SetActive(true);

			if (_itemConfig.itemID == ResourceEnum.TIME_WARP) 
				_purchasedItemText.text = NumberUtility.secondsToHours(_itemConfig.amount) + "H";
			else
				_purchasedItemText.text = StringUtility.toNumString(_itemConfig.amount);

            _purchasedItemIcon.setIcon(_itemConfig.itemID);
        }

        private void activateBoostPurchase(BoostConfig boostConfig)
        {
            _boostItemIconDisplayGO.SetActive(true);
            _purchasedItemResourceAmountGO.SetActive(false);

            if (boostConfig.level == BoostLevelEnum.PLAYER)
            {
                _purchasedBoostItemIcon.setIcon(_boostConfig.id, _boostConfig.imgPath, _boostConfig.rank, true);
            }
            else
            {
                _purchasedBoostItemIcon.setIcon(_boostConfig.id, _boostConfig.imgPath, _boostConfig.rank);
            }
        }

        private void onConfirmClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            _uiManager.hidePurchaseConfirmationPanel();
            _itemsManager.purchaseShopItem(_shopConfig);
            purchaseConfirmedClickedAnalytics();
        }

        private void onCancelClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            _uiManager.hidePurchaseConfirmationPanel();
            purchaseCanceledClickedAnalytics();
        }

        private void purchaseConfirmedClickedAnalytics()
        {
            string currency = CurrencyUtility.getCurrency(_shopConfig);
            if (currency == CurrencyEnum.BUCKS) JobFactory.playFabManager.analytics(AnalyticsEnum.BUCKS_PURCHASE_CONFIRMED_CLICKED);
            else if (currency == CurrencyEnum.KREDS) JobFactory.playFabManager.analytics(AnalyticsEnum.KREDS_PURCHASE_CONFIRMED_CLICKED);
            else if (currency == CurrencyEnum.DOLLAR) JobFactory.playFabManager.analytics(AnalyticsEnum.DOLLAR_PURCHASE_CONFIRMED_CLICKED);
        }

        private void purchaseCanceledClickedAnalytics()
        {
            string currency = CurrencyUtility.getCurrency(_shopConfig);
            if (currency == CurrencyEnum.BUCKS) JobFactory.playFabManager.analytics(AnalyticsEnum.BUCKS_PURCHASE_CANCELED_CLICKED);
            else if (currency == CurrencyEnum.KREDS) JobFactory.playFabManager.analytics(AnalyticsEnum.KREDS_PURCHASE_CANCELED_CLICKED);
            else if (currency == CurrencyEnum.DOLLAR) JobFactory.playFabManager.analytics(AnalyticsEnum.DOLLAR_PURCHASE_CANCELED_CLICKED);
        }

    }
}
