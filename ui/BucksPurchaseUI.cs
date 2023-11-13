using com.super2games.idle.config;
using com.super2games.idle.enums;
using com.super2games.idle.factory;
using com.super2games.idle.manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class BucksPurchaseUI
    {
        private readonly string CLOSE_BUTTON = "closeButton";

        private GameObject _panel;
        private ItemsManager _itemsManager;
        private UIManager _uiManager;

        private Button _closeButton;

        private Dictionary<GameObject, ShopListEntry> _entries = new Dictionary<GameObject, ShopListEntry>();

        public BucksPurchaseUI(GameObject panel, ItemsManager itemsManager, UIManager uiManager)
        {
            _panel = panel;
            _itemsManager = itemsManager;
            _uiManager = uiManager;

            _closeButton = _panel.transform.Find(CLOSE_BUTTON).gameObject.GetComponent<Button>();

            _closeButton.onClick.AddListener(onCloseClick);
        }

        public void populateUI()
        {
            populateShopEntriesList(_itemsManager.bucksItemConfigs, _uiManager.turboBucksForPurchaseEntries, ItemsManager.TURBO_BUCKS_NUM_ENTRIES);
        }

        private void populateShopEntriesList(List<ItemConfig> itemConfigs, List<GameObject> parentList, int count)
        {
            for (int i = 0; i < count; ++i)
            {
                GameObject entryGO = parentList[i];
                ShopListEntry entry = null;
                ItemConfig itemConfig = itemConfigs[i];
                BoostConfig boostConfig = null;
                ShopConfig shopConfig = _itemsManager.getShopConfigByItemID(itemConfig.id);

                if (!_entries.ContainsKey(entryGO))
                {
                    entry = new ShopListEntry();
                    entry.onShopEntryClickEvent += onShopEntryClick;
                    _entries.Add(entryGO, entry);
                }
                else
                {
                    entry = _entries[entryGO];
                }

                entry.updateEntry(entryGO, shopConfig, itemConfig, boostConfig);
            }
        }

        private void onShopEntryClick(ShopListEntry entry)
        {
            _uiManager.showPurchaseConfirmationPanel();
            _uiManager.purchaseConfirmationPanelUI.populateUI(entry.shopConfig, entry.itemConfig, entry.boostConfig);
            JobFactory.playFabManager.analytics(AnalyticsEnum.PURCHASE_BUCKS_ITEM_CLICKED);
        }

        private void onCloseClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            _uiManager.hideBucksPurchasePanel();
        }

    }
}
