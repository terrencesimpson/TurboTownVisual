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
    public class AdButtonUI
    {
        private readonly string AD_BTN = "adButton";
        private readonly string HOTSPOT = "hotspot";

        private Button _adButton;

        private GameObject _adButtonForBucksGO;
        private UIManager _uiManager;

        public AdButtonUI(GameObject adButtonForBucksGO, UIManager uiManager)
        {
            _uiManager = uiManager;
            _adButtonForBucksGO = adButtonForBucksGO;

            _adButton = _adButtonForBucksGO.GetComponent<Button>();
            _adButton.onClick.AddListener(onClick);
        }

        public void highlightUI(string uiID)
        {
            //if (uiID == UIEnum.AD)
            //{
            //    GameObject highlightGO = _uiManager.createHighlight(_adButtonForBucksGO);
            //    highlightGO.transform.localScale = new Vector3(3, 1.5f, 1);
            //}
        }

        private void onClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            _uiManager.showAdConfirmPanel();
            JobFactory.playFabManager.analytics(AnalyticsEnum.AD_BUTTON_CLICK);
        }

    }
}
