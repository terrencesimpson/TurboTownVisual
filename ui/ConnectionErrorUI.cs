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

namespace com.super2games.idle.ui
{
    public class ConnectionErrorUI
    {
        private readonly string RETRY_BUTTON = "panel/retryBtn";
        private readonly string DESCRIPTION_TEXT = "panel/descriptionText";

        private Button _retryButton;
        private Text _descriptionText;

        private GameObject _parent;

        private UIManager _uiManager;

        public ConnectionErrorUI(GameObject parent, UIManager uiManager)
        {
            _parent = parent;
            _uiManager = uiManager;

            _retryButton = _parent.transform.Find(RETRY_BUTTON).gameObject.GetComponent<Button>();
            _descriptionText = _parent.transform.Find(DESCRIPTION_TEXT).gameObject.GetComponent<Text>();

            _retryButton.onClick.AddListener(onRetryClick);
        }

        public void setText(string value)
        {
            _descriptionText.text = value;
        }

        private void onRetryClick()
        {
            _uiManager.hideConnectionErrorPanel();
            JobFactory.startUpManager.retryConnection();
        }

    }
}
