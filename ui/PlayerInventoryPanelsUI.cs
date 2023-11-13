using com.super2games.idle.component.goods;
using com.super2games.idle.component.interaction;
using com.super2games.idle.config;
using com.super2games.idle.datastructures;
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
    public class PlayerInventoryPanelsUI
    {
        private readonly string SCROLL_VIEW = "ScrollView";
        private readonly string TAB = "Tab";

        public PlayerInventoryPanelTabInteractive currentTab;

        private GameObject _playerLevelBoostsPanel;
        private GameObject _boostItemsPanel;
        private GameObject _playerResourcesPanel;
        private GameObject _buildingDetailsPanel;

        private PlayerInventoryPanelTabInteractive _boostItemsTab;
        private PlayerInventoryPanelTabInteractive _playerResourcesTab;
        private PlayerInventoryPanelTabInteractive _buildingDetailsTab;
        private PlayerInventoryPanelTabInteractive _playerLevelBoostsTab;

        private List<PlayerInventoryPanelTabInteractive> _tabs = new List<PlayerInventoryPanelTabInteractive>();

        private UIManager _uiManager;

        public PlayerInventoryPanelsUI(GameObject playerLevelBoostsPanel, GameObject boostItemsPanel, GameObject playerResourcesPanel, GameObject buildingDetailsPanel, UIManager uiManager)
        {
            _playerLevelBoostsPanel = playerLevelBoostsPanel;
            _playerResourcesPanel = playerResourcesPanel;
            _boostItemsPanel = boostItemsPanel;
            _buildingDetailsPanel = buildingDetailsPanel;

            _uiManager = uiManager;

            _playerResourcesTab = _playerResourcesPanel.GetComponentInChildren<PlayerInventoryPanelTabInteractive>();
            _boostItemsTab = _boostItemsPanel.GetComponentInChildren<PlayerInventoryPanelTabInteractive>();
            _buildingDetailsTab = _buildingDetailsPanel.GetComponentInChildren<PlayerInventoryPanelTabInteractive>();
            _playerLevelBoostsTab = _playerLevelBoostsPanel.GetComponentInChildren<PlayerInventoryPanelTabInteractive>();

            _playerResourcesTab.tabName = UIEnum.RESOURCES_TAB;
            _boostItemsTab.tabName = UIEnum.ITEMS_TAB;
            _buildingDetailsTab.tabName = UIEnum.BUILDING_DETAILS_TAB;
            _playerLevelBoostsTab.tabName = UIEnum.PLAYER_LEVEL_BOOSTS_TAB;

            _tabs.Add(_playerResourcesTab);
            _tabs.Add(_boostItemsTab);
            _tabs.Add(_buildingDetailsTab);
            _tabs.Add(_playerLevelBoostsTab);
        }

        public void highlightUI(string uiID, string fingerRotationState)
        {
            if (uiID == UIEnum.ITEMS_TAB) _uiManager.createHighlight(_boostItemsTab.gameObject, fingerRotationState);
            else if (uiID == UIEnum.RESOURCES_TAB) _uiManager.createHighlight(_playerResourcesTab.gameObject, fingerRotationState);
            else if (uiID == UIEnum.BUILDING_DETAILS_TAB) _uiManager.createHighlight(_buildingDetailsTab.gameObject, fingerRotationState);
            else if (uiID == UIEnum.PLAYER_LEVEL_BOOSTS_TAB) _uiManager.createHighlight(_playerLevelBoostsTab.gameObject, fingerRotationState);
        }

        public void switchToItemsTab()
        {
            switchToTab(_boostItemsTab);
        }

        public void switchToResourcesTab()
        {
            switchToTab(_playerResourcesTab);
        }

        public void switchToBuildingDetailsTab()
        {
            switchToTab(_buildingDetailsTab);
        }

        public void switchToPlayerLevelBoostsTab()
        {
            switchToTab(_playerLevelBoostsTab);
        }

        public void clickOnTab(PlayerInventoryPanelTabInteractive selTab)
        {
            if (!JobFactory.tutorialManager.isUIFunctional(selTab.tabName)) return;
            switchToTab(selTab);
        }

        public void switchToTab(PlayerInventoryPanelTabInteractive selTab)
        {
            ConsoleUtility.Log("[PlayerInventoryPanelsUI] Switching to Tab: " + selTab.transform.parent.name);
            GameObject listPanel = selTab.parentPanel;

            for (int i=0;i<_tabs.Count;i++)
            {
                PlayerInventoryPanelTabInteractive t = _tabs[i];

                t.GetComponent<Image>().color = new Color(.5f, .5f, .5f);
            }

            currentTab = selTab;

            JobFactory.recordsManager.uiClick(currentTab.tabName);

            listPanel.GetComponent<Image>().enabled = true;
            currentTab.GetComponent<Image>().color = new Color(1, 1, 1);
            listPanel.transform.SetAsLastSibling();
        }
    }
}
