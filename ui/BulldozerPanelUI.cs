using com.super2games.idle.delegates.building;
using com.super2games.idle.manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class BulldozerPanelUI
    {
        private static readonly string DONE_BUTTON = "DoneBtn";

        private Button _doneButton;

        private BuildingManager _buildingManager;

        public BulldozerPanelUI(GameObject panel, BuildingManager buildingManager)
        {
            _buildingManager = buildingManager;

            _doneButton = panel.transform.Find(DONE_BUTTON).gameObject.GetComponent<Button>();

            _doneButton.onClick.AddListener(onDoneClick);
        }

        private void onDoneClick()
        {
            BuildingBulldozerDelegate.doneBulldozing();
        }


    }
}
