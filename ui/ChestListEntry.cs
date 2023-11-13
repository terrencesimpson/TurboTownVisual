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
    public class ChestListEntry
    {

        private readonly string CHEST_ENTRY = "Prefabs/ChestEntry";
        private readonly string NAME_TEXT = "nameText";
        private readonly string DESCRIPTION_TEXT = "descriptionText";
        private readonly string CURRENCY_TEXT = "currencyText";
        private readonly string PRICE_TEXT = "priceText";
        private readonly string PURCHASE_BUTTON = "purchaseButton";

        private GameObject _entry;

        private ItemsManager _itemsManager;

        private ChestConfig _chestConfig;

        public ChestListEntry(GameObject content, ChestConfig chestConfig, ItemsManager itemsManager)
        {
            _chestConfig = chestConfig;
            _itemsManager = itemsManager;

            _entry = GameObjectUtility.instantiateGameObject(CHEST_ENTRY);

            _entry.transform.SetParent(content.transform);

            Text nameText = _entry.transform.Find(NAME_TEXT).gameObject.GetComponent<Text>();
            Text descriptionText = _entry.transform.Find(DESCRIPTION_TEXT).gameObject.GetComponent<Text>();
            Text currencyText = _entry.transform.Find(CURRENCY_TEXT).gameObject.GetComponent<Text>();
            Text priceText = _entry.transform.Find(PRICE_TEXT).gameObject.GetComponent<Text>();
            Button purchaseButton = _entry.transform.Find(PURCHASE_BUTTON).gameObject.GetComponent<Button>();

            nameText.text = _chestConfig.name;
            descriptionText.text = _chestConfig.description;
            currencyText.text = _chestConfig.currency;
            priceText.text = _chestConfig.price.ToString();

            purchaseButton.onClick.AddListener(onPurchaseButtonClick);
        }

        private void onPurchaseButtonClick()
        {
            //_itemsManager.purchaseChestItem(_chestConfig);
        }


    }
}
