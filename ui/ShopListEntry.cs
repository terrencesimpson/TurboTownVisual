using com.super2games.idle.config;
using com.super2games.idle.enums;
using com.super2games.idle.manager;
using com.super2games.idle.utilities;
using com.super2games.idle.utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class ShopListEntry
    {
        private readonly string ITEM_ICON_TARGET = "itemIconTarget";
        private readonly string COST_TEXT = "costText";
        private readonly string TITLE = "title";

        private readonly string BUCKS_PURCHASE_TEXT = "bucksPurchaseText";
        private readonly string KREDS_COST_TEXT = "kredsCostText";
        private readonly string KREDS_COST_ICON = "kredsCostIcon";

        private readonly Vector3 REWARD_ICON_RESOURCES_SCALE = new Vector3(2, 2, 2);

        public delegate void OnShopEntryClickDelegate(ShopListEntry entry);
        public event OnShopEntryClickDelegate onShopEntryClickEvent;

        public GameObject entryGO;
        public bool hasSold = false;
        public ShopConfig shopConfig;
        public ItemConfig itemConfig;
        public BoostConfig boostConfig;
        private string _type;

        private GameObject _rewardIcon = null;

        public void updateEntry(GameObject entry, ShopConfig shopConfig, ItemConfig itemConfig, BoostConfig boostConfig)
        {
            entryGO = entry;
            this.shopConfig = shopConfig;
            this.itemConfig = itemConfig;
            this.boostConfig = boostConfig;
            _type = itemConfig.type;

            hasSold = false;

            if (isBucksOrPeriodicEntry()) //Bucks is a special case because the entry is different.
            {
                createBucksEntry();
            }
            else
            {
                createShopEntry();
            }
        }

        private void createBucksEntry()
        {
            Button entryButton = entryGO.GetComponent<Button>();
            Text bucksPurchaseText = entryGO.transform.Find(BUCKS_PURCHASE_TEXT).gameObject.GetComponent<Text>();
            Text kredsCostText = entryGO.transform.Find(KREDS_COST_TEXT).gameObject.GetComponent<Text>();
            Image kredsCostIcon = entryGO.transform.Find(KREDS_COST_ICON).gameObject.GetComponent<Image>();

            bucksPurchaseText.text = itemConfig.amount.ToString();
            kredsCostText.text = CurrencyUtility.getPrice(shopConfig).ToString();

            ImageUtility.loadAndSetImage(kredsCostIcon, ResourceIconUI.BASE_PATH + CurrencyUtility.getCurrency(shopConfig));

            entryButton.onClick.RemoveListener(onEntryClick);
            entryButton.onClick.AddListener(onEntryClick);
        }

        private void createShopEntry()
        {
            Button entryButton = entryGO.GetComponent<Button>();
            Text titleText = entryGO.transform.Find(TITLE).gameObject.GetComponent<Text>();
            Text costText = entryGO.transform.Find(COST_TEXT).gameObject.GetComponent<Text>();
            Vector3 iconPosition = entryGO.transform.Find(ITEM_ICON_TARGET).gameObject.transform.localPosition;

            titleText.text = getTitleText();
            costText.text = CurrencyUtility.getPrice(shopConfig).ToString();

            createIcon(entryGO, iconPosition);

            entryButton.onClick.RemoveListener(onEntryClick);
            entryButton.onClick.AddListener(onEntryClick);
        }

        private void onEntryClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            dispatchShopEntryEvent();
        }

        private void dispatchShopEntryEvent()
        {
            if (onShopEntryClickEvent != null)
            {
                onShopEntryClickEvent(this);
            }
        }

        private string getTitleText()
        {
            if (_type == ItemTypeEnum.BOOST)
            {
                return boostConfig.name;
            }
            else if (_type == ItemTypeEnum.RESOURCE)
            {
                return itemConfig.itemID + " " + StringUtility.toNumString(itemConfig.amount);
            }
            else if (_type == ItemTypeEnum.TIME_WARP)
            {
                return itemConfig.name + " " + NumberUtility.secondsToHours(itemConfig.amount) + " Hours";
            }
            return "";
        }

        private void createIcon(GameObject parent, Vector3 position)
        {
            if (_rewardIcon == null)
            {
                if (_type == ItemTypeEnum.BOOST)
                {
                    _rewardIcon = GameObjectUtility.instantiateGameObject(PrefabsEnum.BOOST_ITEM_ICON_DISPLAY_PREFAB);
                }
                else
                {
                    _rewardIcon = GameObjectUtility.instantiateGameObject(PrefabsEnum.RESOURCE_ICON_PREFAB);
                }
            }

            if (_type == ItemTypeEnum.BOOST)
            {
                if (boostConfig.level == BoostLevelEnum.PLAYER)
                {
                    _rewardIcon.GetComponent<BoostItemIconUI>().setIcon(boostConfig.id, boostConfig.imgPath, boostConfig.rank, false);
                }
                else
                {
                    _rewardIcon.GetComponent<BoostItemIconUI>().setIcon(boostConfig.id, boostConfig.imgPath, boostConfig.rank, false);
                }
            }
            else
            {
                _rewardIcon.GetComponent<ResourceIconUI>().setIcon(itemConfig.itemID, "", 0, false);
            }

            if (_type == ItemTypeEnum.TIME_WARP)
            {
                _rewardIcon.transform.localScale = REWARD_ICON_RESOURCES_SCALE;
            }

            _rewardIcon.transform.SetParent(parent.transform, false);
            _rewardIcon.transform.localPosition = position;
        }

        public bool isBucksOrPeriodicEntry()
        {
            if (itemConfig.type == ItemTypeEnum.BUCKS || 
                itemConfig.type == ItemTypeEnum.PERIODIC) //Bucks is a special case because the entry is different.
            {
                return true;
            }
            return false;
        }

    }
}
