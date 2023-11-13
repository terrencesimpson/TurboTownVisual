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
    public class AlertPopupUI
    {
        public delegate void OnAlertOkClickDelegate();
        public event OnAlertOkClickDelegate onAlertOkClickEvent;

        private UIManager _uiManager;

        private GameObject _panel;
        private Text _description;
        private Text _title;
        private Button _okButton;
        private Button _retryButton;

        private GameObject _okButtonGO;
        private GameObject _retryButtonGO;

        public AlertPopupUI(GameObject panel, UIManager uiManager)
        {
            _panel = panel;
            _uiManager = uiManager;

            _description = _panel.transform.Find("panel/descriptionText").gameObject.GetComponent<Text>();
            _title = _panel.transform.Find("panel/title").gameObject.GetComponent<Text>();
            _okButtonGO = _panel.transform.Find("panel/okButton").gameObject;
            _retryButtonGO = _panel.transform.Find("panel/retryButton").gameObject;
            _okButton = _okButtonGO.GetComponent<Button>();
            _retryButton = _retryButtonGO.GetComponent<Button>();

            _okButton.onClick.AddListener(onOkClick);
            _retryButton.onClick.AddListener(onRetryClick);
        }

        public void showAlert(string alertText, string titleText, bool showRetryButtonState = false)
        {
            _uiManager.showAlertPanel();
            _description.text = alertText;
            _title.text = titleText;

            showOkButton();

            if (showRetryButtonState)
            {
                showRetryButton();
            }
        }

        public void showAlertWithException(string alertText, string titleText)
        {
            _uiManager.showAlertPanel();
            _description.text = alertText;
            _title.text = titleText;
            hideOkButton();
            throw new Exception(alertText);
        }

        public void showOkButton()
        {
            _okButtonGO.SetActive(true);
            hideRetryButton();
        }

        public void hideOkButton()
        {
            _okButtonGO.SetActive(false); //Hide the OK Button, we want to freeze the game.
        }

        public void showRetryButton()
        {
            _retryButtonGO.SetActive(true);
            hideOkButton();
        }

        public void hideRetryButton()
        {
            _retryButtonGO.SetActive(false); //Hide the OK Button, we want to freeze the game.
        }

        private void onRetryClick()
        {
            _uiManager.hideConnectionErrorPanel();
            JobFactory.startUpManager.retryConnection();
        }

        private void onOkClick()
        {
            _uiManager.hideAlertPanel();

            if (onAlertOkClickEvent != null)
            {
                onAlertOkClickEvent();
            }
        }

    }
}
