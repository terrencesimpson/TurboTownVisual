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
    public class BuildingListUI
    {
        private readonly string PANEL_CONTENT = "Scroll View/Viewport/Content";
        private readonly string BUIDLING_ICON = "BuildingIcon";
        private readonly string SCROLL_VIEW = "Scroll View";

        private readonly string BUILDING_NAME_TEXT_2D = "buildingName";

        private readonly Vector3 LOCAL_SCALE = new Vector3(3, 3, 3);

        private readonly float UPDATE_ENABLE_TIME = 1f;

        public BuildingListTabInteractive currentTab;

        private GameObject _buildingListPanel;
        private GameObject _utilityListPanel;
        private GameObject _residentialListPanel;
        private GameObject _commercialListPanel;
        private GameObject _industrialListPanel;
        private GameObject _resourceCollectorListPanel;
        private GameObject _mrBListPanel;

        private List<BuildingListTabInteractive> _tabs = new List<BuildingListTabInteractive>();
        private List<ScrollRect> _scrollRects = new List<ScrollRect>();

        private ModelManager _modelManager;
        private CostManager _costManager;
        private PrefabManager _prefabManager;
        private UIManager _uiManager;

        private Dictionary<string, GameObject> entries = new Dictionary<string, GameObject>();
        private Dictionary<string, GameObject> buildingIcons = new Dictionary<string, GameObject>();

        private float _updateListEnablementTime = 0;

        public BuildingListTabInteractive utilityTab;
        public BuildingListTabInteractive residentialTab;
        public BuildingListTabInteractive commercialTab;
        public BuildingListTabInteractive industrialTab;
        public BuildingListTabInteractive resourceTab;
        public BuildingListTabInteractive mrBTab;

        public ScrollRect utilityScrollRect;
        public ScrollRect residentialScrollRect;
        public ScrollRect commercialScrollRect;
        public ScrollRect industrialScrollRect;
        public ScrollRect resourceScrollRect;
        public ScrollRect mrBScrollRect;

        public BuildingListUI(GameObject buildingListPanel, GameObject utilityListPanel, GameObject residentialListPanel, GameObject commercialListPanel, GameObject industrialListPanel, GameObject resourceCollectorListPanel, GameObject mrBPanel, CostManager costManager, ModelManager modelManager, PrefabManager prefabManager, UIManager uiManager)
        {
            _buildingListPanel = buildingListPanel;
            _utilityListPanel = utilityListPanel;
            _residentialListPanel = residentialListPanel;
            _commercialListPanel = commercialListPanel;
            _industrialListPanel = industrialListPanel;
            _resourceCollectorListPanel = resourceCollectorListPanel;
            _mrBListPanel = mrBPanel;

            _modelManager = modelManager;
            _costManager = costManager;
            _prefabManager = prefabManager;
            _uiManager = uiManager;

            buildListUI();

            utilityTab = _utilityListPanel.GetComponentInChildren<BuildingListTabInteractive>();
            residentialTab = _residentialListPanel.GetComponentInChildren<BuildingListTabInteractive>();
            commercialTab = _commercialListPanel.GetComponentInChildren<BuildingListTabInteractive>();
            industrialTab = _industrialListPanel.GetComponentInChildren<BuildingListTabInteractive>();
            resourceTab  = _resourceCollectorListPanel.GetComponentInChildren<BuildingListTabInteractive>();
            mrBTab = _mrBListPanel.GetComponentInChildren<BuildingListTabInteractive>();

            utilityScrollRect = _utilityListPanel.GetComponentInChildren<ScrollRect>();
            residentialScrollRect = _residentialListPanel.GetComponentInChildren<ScrollRect>();
            commercialScrollRect = _commercialListPanel.GetComponentInChildren<ScrollRect>();
            industrialScrollRect = _industrialListPanel.GetComponentInChildren<ScrollRect>();
            resourceScrollRect = _resourceCollectorListPanel.GetComponentInChildren<ScrollRect>();
            mrBScrollRect = _mrBListPanel.GetComponentInChildren<ScrollRect>();

            residentialTab.tabName = UIEnum.RESIDENCE_TAB;
            commercialTab.tabName = UIEnum.COMMERCIAL_TAB;
            resourceTab.tabName = UIEnum.RESOURCE_TAB;
            industrialTab.tabName = UIEnum.INDUSTRIAL_TAB;
            utilityTab.tabName = UIEnum.UTILITY_TAB;
            mrBTab.tabName = UIEnum.MR_B_TAB;

            utilityTab.parentPanel = _utilityListPanel;
            residentialTab.parentPanel = _residentialListPanel;
            commercialTab.parentPanel = _commercialListPanel;
            industrialTab.parentPanel = _industrialListPanel;
            resourceTab.parentPanel = _resourceCollectorListPanel;
            mrBTab.parentPanel = _mrBListPanel;

            _tabs.Add(utilityTab);
            _tabs.Add(residentialTab);
            _tabs.Add(commercialTab);
            _tabs.Add(industrialTab);
            _tabs.Add(resourceTab);
            _tabs.Add(mrBTab);

            _scrollRects.Add(utilityScrollRect);
            _scrollRects.Add(residentialScrollRect);
            _scrollRects.Add(commercialScrollRect);
            _scrollRects.Add(industrialScrollRect);
            _scrollRects.Add(resourceScrollRect);
            _scrollRects.Add(mrBScrollRect);

            switchToTab(_tabs[0]);
        }

        public void update()
        {
            if (StartUpManager.zoomedInToGameState)
            {
                checkBuildingListEnablement();
            }
        }

        public void highlightUI(string uiID, string fingerRotationState)
        {
            if (uiID == UIEnum.RESIDENCE_TAB) _uiManager.createHighlight(_residentialListPanel.transform.Find("Tab").gameObject, fingerRotationState);
            else if (uiID == UIEnum.COMMERCIAL_TAB) _uiManager.createHighlight(_commercialListPanel.transform.Find("Tab").gameObject, fingerRotationState);
            else if (uiID == UIEnum.RESOURCE_TAB) _uiManager.createHighlight(_resourceCollectorListPanel.transform.Find("Tab").gameObject, fingerRotationState);
            else if (uiID == UIEnum.INDUSTRIAL_TAB) _uiManager.createHighlight(_industrialListPanel.transform.Find("Tab").gameObject, fingerRotationState);
            else if (uiID == UIEnum.UTILITY_TAB) _uiManager.createHighlight(_utilityListPanel.transform.Find("Tab").gameObject, fingerRotationState);
            else if (uiID == UIEnum.MR_B_TAB) _uiManager.createHighlight(_mrBListPanel.transform.Find("Tab").gameObject, fingerRotationState);
            else if (buildingIcons.ContainsKey(uiID)) _uiManager.createHighlight(buildingIcons[uiID], fingerRotationState);
        }

        public void updateCost(string buildingID, Inventory inventory)
        {
            GameObject resourceCostEntryParent = entries[buildingID].GetComponentInChildren<GridLayoutGroup>().transform.gameObject;
            foreach (Transform child in resourceCostEntryParent.transform)
            {
                child.gameObject.SetActive(false);
            }

            int count = 0;
            foreach (KeyValuePair<string, Item> pair in inventory.items)
            {
                if (resourceCostEntryParent.transform.GetChild(count) == null)
                {
                    return;
                }

                GameObject resourceCostEntry = resourceCostEntryParent.transform.GetChild(count).gameObject;

                Text resourceText = resourceCostEntry.GetComponentInChildren<Text>();
                resourceText.text = StringUtility.toNumString(pair.Value.amount);

                ResourceIconUI resourceIcon = resourceCostEntry.GetComponentInChildren<ResourceIconUI>();
                string resourceFile = pair.Value.id;
                resourceIcon.setIcon(resourceFile);

                resourceCostEntry.SetActive(true);
                
                count++;
            }
        }

        public void clickOnTab(BuildingListTabInteractive selTab)
        {
            if (!JobFactory.tutorialManager.isUIFunctional(selTab.tabName)) return;
            switchToTab(selTab);
        }

        public void switchToTab(BuildingListTabInteractive selTab)
        {
            GameObject listPanel = selTab.parentPanel;

            for (int i=0;i<_tabs.Count;i++)
            {
                BuildingListTabInteractive t = _tabs[i];

                t.GetComponent<Image>().color = new Color(.5f, .5f, .5f);
                t.parentPanel.GetComponent<Image>().enabled = false;

                t.findScrollViewIfNull();
                t.listScrollView.SetActive(false);
            }

            currentTab = selTab;
            currentTab.listScrollView.SetActive(true);

            currentTab.GetComponent<Image>().color = new Color(1, 1, 1);
            listPanel.GetComponent<Image>().enabled = true;
            listPanel.transform.SetAsLastSibling();

            JobFactory.recordsManager.uiClick(selTab.tabName);

            updateEntriesBasedOnCost();
        }

        public void setAllTabsEnablement(bool enableState)
        {
            for (int i = 0; i < _tabs.Count; i++)
            {
                _tabs[i].enabled = enableState;
            }
        }
        
        public void setTabEnablementByID(string tabName, bool enableState)
        {
            for (int i = 0; i < _tabs.Count; i++)
            {
                if (_tabs[i].tabName == tabName)
                {
                    _tabs[i].enabled = enableState;
                    break;
                }
            }
        }

        public void setAllScrollRectsEnablement(bool enableState)
        {
            for (int i = 0; i < _scrollRects.Count; i++)
            {
                _scrollRects[i].enabled = enableState;
            }
        }

        public void updateEntriesBasedOnCost(bool sortState = true)
        {
            foreach (KeyValuePair<string, IConfig> pair in _modelManager.buildingsModel.configs)
            {
                BuildingsConfig config = pair.Value as BuildingsConfig;
                string buildingID = config.id;
                
                bool isInUI = config.isInUI; 

                if (!isInUI)
                {
                    continue;
                }

                checkEntrySatisfaction(entries[buildingID], buildingID);
            }

            if (sortState)
            {
                //Recomment when we're ready to rewrite the unsorted list acquistion
                List<BuildingIconUI> icons = sortListBasedOnCost(currentTab.parentPanel.transform.Find(PANEL_CONTENT));
                for (int i = icons.Count - 1; i > -1; i--)
                {
                    BuildingIconUI icon = icons[i];
                    icon.transform.parent.transform.SetAsFirstSibling();
                }
            }
        }

        private void checkBuildingListEnablement()
        {
            if (_buildingListPanel.activeSelf)
            {
                _updateListEnablementTime += Time.deltaTime;
                if (_updateListEnablementTime >= UPDATE_ENABLE_TIME)
                {
                    _updateListEnablementTime = 0;
                    updateEntriesBasedOnCost(false);
                }
            }
        }

        public void checkEntrySatisfaction(GameObject entry, string buildingID)
        {
            if (!JobFactory.tutorialManager.isComplete) return; //We don't want to lighten or darken while in tutorial mode. The tutorial takes care of that.

            if (_costManager.satisfied(buildingID))
            {
                setEntryEnablement(entry, true);
            }
            else
            {
                setEntryEnablement(entry, false);
            }
        }

        private void lightenEntry(GameObject entry)
        {
            entry.GetComponent<Image>().color = new Color(1, 1, 1);
            entry.GetComponentInChildren<BuildingIconUI>().isBGDarkened = false;
            entry.GetComponentInChildren<BuildingIconUI>().lightenIcon();
        }

        private void darkenEntry(GameObject entry)
        {
            entry.GetComponent<Image>().color = new Color(.5f, .5f, .5f);
            entry.GetComponentInChildren<BuildingIconUI>().isBGDarkened = true;
            entry.GetComponentInChildren<BuildingIconUI>().darkenIcon();
        }

        private void setEntryEnablement(GameObject entry, bool enableState)
        {
            BuildingIconButton entryBuildingButton = entry.transform.Find("Hotspot").gameObject.GetComponentInChildren<BuildingIconButton>();
            entryBuildingButton.enabled = enableState;
            if (enableState) lightenEntry(entry);
            else darkenEntry(entry);
        }

        public void setAllEntriesEnablement(bool enableState)
        {
            foreach (KeyValuePair<string, GameObject> pair in entries)
            {
                setEntryEnablement(pair.Value, enableState);
            }
        }

        public void setEntryEnablementByID(string buildingID, bool enableState)
        {
            setEntryEnablement(entries[buildingID], enableState);
        }

        private List<BuildingIconUI> sortListBasedOnCost(Transform parentContent)
        {
            List<BuildingIconUI> unsortedList = new List<BuildingIconUI>();

            for (int i=0;i< parentContent.childCount;i++)
            {
                BuildingIconUI icon = parentContent.GetChild(i).Find(BUIDLING_ICON).GetComponent<BuildingIconUI>();
                if (icon.buildingConfig != null)
                {
                    unsortedList.Add(icon);
                }
            }

            List<BuildingIconUI> sortedList = new List<BuildingIconUI>();

            if (unsortedList.Count > 0)
            {
                sortedList = unsortedList.OrderBy(entry => entry.isBGDarkened).ThenBy(entry => JobFactory.costManager.getReductionInventoryByBuildingID(entry.buildingConfig.id).getFirstItem().amount).ToList();
            }

            return sortedList;
        }

        private Transform getContentParentByType(string type)
        {
            Transform parentContent = null;

            if (type == BuildingTypesEnum.UTILITY || type == BuildingTypesEnum.PUBLIC_SERVICE)
            {
                parentContent = _utilityListPanel.transform.Find(PANEL_CONTENT);
            }
            else if (type == BuildingTypesEnum.RESIDENTIAL)
            {
                parentContent = _residentialListPanel.transform.Find(PANEL_CONTENT);
            }
            else if (type == BuildingTypesEnum.COMMERCIAL)
            {
                parentContent = _commercialListPanel.transform.Find(PANEL_CONTENT);
            }
            else if (type == BuildingTypesEnum.INDUSTRIAL)
            {
                parentContent = _industrialListPanel.transform.Find(PANEL_CONTENT);
            }
            else if (type == BuildingTypesEnum.RESOURCE_COLLECTOR)
            {
                parentContent = _resourceCollectorListPanel.transform.Find(PANEL_CONTENT);
            }
            else if (type == BuildingTypesEnum.MR_B)
            {
                parentContent = _mrBListPanel.transform.Find(PANEL_CONTENT);
            }

            return parentContent;
        }

        private void buildListUI()
        {
            foreach (KeyValuePair<string, IConfig> pair in _modelManager.buildingsModel.configs)
            {
                BuildingsConfig config = pair.Value as BuildingsConfig;
                bool isInUI = config.isInUI;
                string prefabPath = config.prefabPath;
                string buildingID = config.id;
                string type = config.types[0] as string;
                string buildingName = config.displayName;

                if (!isInUI)
                {
                    continue;
                }

                GameObject entry = GameObjectUtility.instantiateGameObject(PrefabsEnum.BUILDING_LIST_ENTRY);

                Inventory reductionInventory = _costManager.getReductionInventoryByBuildingID(buildingID);
                GameObject buildingNameGameObject = entry.transform.Find(BUILDING_NAME_TEXT_2D).gameObject;
                Text buildingNameText = buildingNameGameObject.GetComponent<Text>();

                entries.Add(buildingID, entry);

                buildingNameText.text = buildingName;

                BuildingIconButton entryBuildingButton = entry.GetComponentInChildren<BuildingIconButton>();
                BuildingIconUI entryBuildingIcon = entry.GetComponentInChildren<BuildingIconUI>();
                entryBuildingIcon.buildingConfig = config;
                entryBuildingIcon.setIcon(buildingID);

                entryBuildingButton.buildingConfig = config;
                entryBuildingButton.iconID = buildingID;

                buildingIcons.Add(buildingID, entryBuildingIcon.gameObject);

                GridLayoutGroup layout = entry.GetComponentInChildren<GridLayoutGroup>();

                updateCost(buildingID, reductionInventory);

                checkEntrySatisfaction(entry, buildingID);

                Transform parentContent = getContentParentByType(type);

                entry.transform.SetParent(parentContent, false);
                entry.transform.SetAsLastSibling();
            }
        }
    }
}
