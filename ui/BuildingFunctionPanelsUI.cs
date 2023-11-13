using com.super2games.idle.enums;
using com.super2games.idle.factory;
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
    public class BuildingFunctionPanelsUI
    {
        private readonly string TAB = "Tab";

        public BuildingFunctionPanelTabInteractive currentTab;
        private GameObject _jobsPanel;
        private BuildingFunctionPanelTabInteractive _jobsTab;

        private List<BuildingFunctionPanelTabInteractive> _tabs = new List<BuildingFunctionPanelTabInteractive>();

        private UIManager _uiManager;

        public BuildingFunctionPanelsUI(GameObject jobsPanel, UIManager uiManager)
        {
            _jobsPanel = jobsPanel;

            _uiManager = uiManager;

            _jobsTab = _jobsPanel.GetComponentInChildren<BuildingFunctionPanelTabInteractive>();

            _jobsTab.tabName = UIEnum.JOBS_TAB;

            _tabs.Add(_jobsTab);

            _jobsTab.GetComponent<Image>().color = new Color(1, 1, 1); //Dunno why this was dark on startup
        }

        public void highlightUI(string uiID, string fingerRotationState)
        {
            if (uiID == UIEnum.JOBS_TAB) _uiManager.createHighlight(_jobsTab.gameObject, fingerRotationState);
        }

        public void switchToJobsTab()
        {
            switchToTab(_jobsTab); 
        }

        public void clickOnTab(BuildingFunctionPanelTabInteractive selTab)
        {
            if (!JobFactory.tutorialManager.isUIFunctional(selTab.tabName)) return;
            switchToTab(selTab);
        }

        public void switchToTab(BuildingFunctionPanelTabInteractive selTab)
        {
            GameObject listPanel = selTab.parentPanel;

            for (int i=0;i<_tabs.Count;i++)
            {
                BuildingFunctionPanelTabInteractive t = _tabs[i];

                t.GetComponent<Image>().color = new Color(.5f, .5f, .5f);
                t.parentPanel.GetComponent<Image>().enabled = false;
            }

            _uiManager.showBuildingFunctionPanel();

            currentTab = selTab;

            JobFactory.recordsManager.uiClick(currentTab.tabName);

            currentTab.GetComponent<Image>().color = new Color(1, 1, 1);
            listPanel.GetComponent<Image>().enabled = true;
            listPanel.transform.SetAsLastSibling();
        }

    }
}
