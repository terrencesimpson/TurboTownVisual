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

namespace com.super2games.idle.ui
{
    public class LocalSaveNotFoundUI
    {
        //private readonly string CONFIRM_BUTTON = "panel/confirmBtn";
        private readonly string CANCEL_BUTTON = "panel/cancelBtn";

        private Button _confirmButton;
        private Button _cancelButton;

        private GameObject _parent;

        private UIManager _uiManager;
        private StartUpManager _startUpManager;

        public LocalSaveNotFoundUI(GameObject parent, UIManager uiManager)
        {
            _parent = parent;
            _uiManager = uiManager;

            //_confirmButton = _parent.transform.FindChild(CONFIRM_BUTTON).gameObject.GetComponent<Button>();
            _cancelButton = _parent.transform.Find(CANCEL_BUTTON).gameObject.GetComponent<Button>();

            //_confirmButton.onClick.AddListener(onConfirmClick);
            _cancelButton.onClick.AddListener(onCancelClick);
        }

        //private void onConfirmClick()
        //{
        //    _startUpManager.newPlayer();
        //    _uiManager.hideNewGameConfirmPanel();
        //}

        private void onCancelClick()
        {
            _uiManager.hideLocalSaveNotFoundPanel();

            _uiManager.showPlayerDataOptionsPanel();
            _uiManager.newPlayerButton.gameObject.SetActive(true);
            _uiManager.loadLocalButton.gameObject.SetActive(true);
        }

    }
}
