using com.super2games.idle.delegates.building;
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
    public class BuildingPlacementUI
    {

        private static readonly string CANCEL_BUTTON = "CancelBtn";
        private static readonly string CONFIRM_BUTTON = "ConfirmBtn";
        private static readonly string ROTATE_BUTTON = "RotateBtn";
        private static readonly string DONE_BUTTON = "DoneBtn";

        private Button _cancelButton;
        private Button _confirmButton;
        private Button _rotateButton;
        private Button _doneButton;

        private BuildingManager _buildingManager;

        public bool buttonHighlitState = false;

        public BuildingPlacementUI(GameObject panel, BuildingManager buildingManager)
        {
            _buildingManager = buildingManager;

            _cancelButton = panel.transform.Find(CANCEL_BUTTON).gameObject.GetComponent<Button>();
            _confirmButton = panel.transform.Find(CONFIRM_BUTTON).gameObject.GetComponent<Button>();
            _rotateButton = panel.transform.Find(ROTATE_BUTTON).gameObject.GetComponent<Button>();
            _doneButton = panel.transform.Find(DONE_BUTTON).gameObject.GetComponent<Button>();

            _cancelButton.onClick.AddListener(onCancelClick);
            _confirmButton.onClick.AddListener(onConfirmClick);
            _rotateButton.onClick.AddListener(onRotateClick);
            _doneButton.onClick.AddListener(onDoneClick);

            hideBuildingButtons();
        }

        public void showDoneButton()
        {
            if (!JobFactory.tutorialManager.isComplete && !JobFactory.tutorialManager.allGoalsComplete()) return; //For tutorial only

            hideBuildingButtons();
            _doneButton.gameObject.SetActive(true);
        }

        public void hideDoneButton()
        {
            _doneButton.gameObject.SetActive(false);
        }

        public void showBuildingButtons()
        {
            _confirmButton.gameObject.SetActive(true);
            if (JobFactory.tutorialManager.isComplete)
            {
                _cancelButton.gameObject.SetActive(true);
                _rotateButton.gameObject.SetActive(true);
            }
            hideDoneButton();
        }

        public void highlightUI(bool highlightDone = false)
        {
            if (buttonHighlitState) return;

            if (highlightDone && JobFactory.tutorialManager.allGoalsComplete()) JobFactory.uiManager.createHighlight(_doneButton.gameObject, DirectionEnum.SOUTH);
            else if (!highlightDone) JobFactory.uiManager.createHighlight(_confirmButton.gameObject, DirectionEnum.SOUTH);

            buttonHighlitState = true;
        }

        public void resetHighlightState()
        {
            buttonHighlitState = false; 
        }

        public void hideBuildingButtons()
        {
            _cancelButton.gameObject.SetActive(false);
            _confirmButton.gameObject.SetActive(false);
            _rotateButton.gameObject.SetActive(false);
        }

        private void onCancelClick()
        {
            BuildingPlacementDelegate.cancelBuildingPlacement();
        }

        private void onConfirmClick()
        {
            BuildingPlacementDelegate.confirmBuildingPlacement();
        }

        private void onRotateClick()
        {
            BuildingUtilityDelegate.rotateCurrentBuilding();
        }

        private void onDoneClick()
        {
            BuildingPlacementDelegate.doneBuildingPlacement();
        }

    }
}
