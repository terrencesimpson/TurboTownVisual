using com.super2games.idle.manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.super2games.idle.ui
{
    public class BuildingFunctionPanelTabInteractive : MonoBehaviour
    {
        public GameObject parentPanel;

        private BuildingFunctionPanelsUI _buildingFunctionPanelsUI;
        public UIManager uiManager;

        public string tabName;

        public void selectTab()
        {
            if (_buildingFunctionPanelsUI == null)
            {
                _buildingFunctionPanelsUI = uiManager.buildingFunctionPanelsUI;
            }

            _buildingFunctionPanelsUI.clickOnTab(this);
            SoundManager.instance.playSound(SoundManager.SOUND_TAB_SWITCH);
        }

    }
}
