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
using com.super2games.idle.delegates.building;

namespace com.super2games.idle.ui
{
    public class SellBuildingConfirmationUI
    {
        private readonly string CONFIRM_BUTTON = "panel/confirmButton";
        private readonly string CANCEL_BUTTON = "panel/cancelButton";

        private Button _confirmButton;
        private Button _cancelButton;

        private GameObject _parent;

        private UIManager _uiManager;
        private BuildingManager _buildingManager;

        public SellBuildingConfirmationUI(GameObject parent, UIManager uiManager, BuildingManager buildingManager)
        {
            _parent = parent;
            _uiManager = uiManager;
            _buildingManager = buildingManager;

            _confirmButton = _parent.transform.Find(CONFIRM_BUTTON).gameObject.GetComponent<Button>();
            _cancelButton = _parent.transform.Find(CANCEL_BUTTON).gameObject.GetComponent<Button>();

            _confirmButton.onClick.AddListener(onConfirmClick);
            _cancelButton.onClick.AddListener(onCancelClick);
        }

        private void onConfirmClick()
        {
            _uiManager.hideSellBuildingConfirmationPanel();
            BuildingBulldozerDelegate.sellSelectedBulldozeBuilding();
        }

        private void onCancelClick()
        {
            _uiManager.hideSellBuildingConfirmationPanel();
        }

    }
}
