using com.super2games.idle.component.core;
using com.super2games.idle.component.goods;
using com.super2games.idle.component.possessor;
using com.super2games.idle.component.task;
using com.super2games.idle.config;
using com.super2games.idle.delegates.building;
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
    public class RecordsListsUI
    {
        private GameObject _recordsPanel;
        private RecordsManager _recordsManager;

        private readonly string CLOSE_BUTTON = "exitBtnHotspot";

        private readonly string CURRENT_COLUMN = "ResourcesScrollView/Viewport/Content/currentColumn";
        private readonly string EARNED_COLUMN = "ResourcesScrollView/Viewport/Content/earnedColumn";
        private readonly string SPENT_COLUMN = "ResourcesScrollView/Viewport/Content/spentColumn";
        private readonly string NET_COLUMN = "ResourcesScrollView/Viewport/Content/netColumn";

        private readonly string BUILDING_COLUMN = "BuildingListScrollView/Viewport/Content/buildingColumn";

        private readonly int HOUR = 3600;

        private Button _closeButton;

        private GameObject _currentColumn;
        private GameObject _earnedColumn;
        private GameObject _spentColumn;
        private GameObject _netColumn;

        private GameObject _buildingColumn;

        private Dictionary<string, RecordsBuildingEntry> _buildingEntries = new Dictionary<string, RecordsBuildingEntry>();

        private UIManager _uiManager;
        private BuildingManager _buildingManager;
        private PrefabManager _prefabManager;

        private List<ResourceEntryUI> _entries = new List<ResourceEntryUI>();

        public RecordsListsUI(GameObject recordsPanel, RecordsManager recordsManager, UIManager uiManager, BuildingManager buildingManager, PrefabManager prefabManager)
        {
            _recordsPanel = recordsPanel;
            _recordsManager = recordsManager;
            _closeButton = recordsPanel.transform.Find(CLOSE_BUTTON).gameObject.GetComponent<Button>();
            _uiManager = uiManager;
            _buildingManager = buildingManager;
            _prefabManager = prefabManager;

            _currentColumn = recordsPanel.transform.Find(CURRENT_COLUMN).gameObject;
            _earnedColumn = recordsPanel.transform.Find(EARNED_COLUMN).gameObject;
            _spentColumn = recordsPanel.transform.Find(SPENT_COLUMN).gameObject;
            _netColumn = recordsPanel.transform.Find(NET_COLUMN).gameObject;

            _buildingColumn = recordsPanel.transform.Find(BUILDING_COLUMN).gameObject;

            _closeButton.onClick.AddListener(onCloseClick);
        }

        private void clean()
        {
            for (int i = 0; i < _entries.Count; ++i)
            {
                _entries[i].returnToPrefabManager();
            }

            foreach (KeyValuePair<string, RecordsBuildingEntry> pair in _buildingEntries)
            {
                pair.Value.returnToPrefabManager();
            }

            _currentColumn.transform.DetachChildren();
            _earnedColumn.transform.DetachChildren();
            _spentColumn.transform.DetachChildren();
            _netColumn.transform.DetachChildren();

            _buildingColumn.transform.DetachChildren();

            _buildingEntries.Clear();
            _entries.Clear();
        }

        public void updatePanel()
        {
            clean();

            Dictionary<string, double> earnedResources = BuildingFunctionalityDelegate.getAllBuildingsResourcesPreSecond();
            Dictionary<string, double> spentResources = BuildingFunctionalityDelegate.getAllBuildingsResourcesPreSecond(false);
            Dictionary<string, double> netResources = InventoryUtility.resourcesNet(earnedResources, spentResources);
            Dictionary<string, double> currentResources = resourcesCurrent(earnedResources);

            buildEntries(currentResources, _currentColumn);
            buildEntries(earnedResources, _earnedColumn, HOUR);
            buildEntries(spentResources, _spentColumn, HOUR);
            buildEntries(netResources, _netColumn, HOUR, true);

            buildBuidlingsEntries();
        }

        private void buildBuidlingsEntries()
        {
            List<ComponentContainer> buildings = JobFactory.buildingManager.getAll();
            Dictionary<string, Dictionary<string, double>> costs = new Dictionary<string, Dictionary<string, double>>();
            Dictionary<string, Dictionary<string, double>> rewards = new Dictionary<string, Dictionary<string, double>>();
            RecordsBuildingEntry entry;
            for (int i = 0; i < buildings.Count; ++i)
            {
                Building building = buildings[i] as Building;
                JobAccessorComponent jobAccessor = building.findComponent(ComponentIDEnum.JOB_ACCESSOR) as JobAccessorComponent;
                BuildingsConfig config = JobFactory.modelManager.buildingsModel.getConfig(building.id) as BuildingsConfig;

                if (jobAccessor == null || !jobAccessor.hasJobs() || config.isResource) continue;

                if (!_buildingEntries.ContainsKey(building.id))
                {
                    entry = new RecordsBuildingEntry();
                    _buildingEntries.Add(building.id, entry);
                    entry.buildingIcon.setIcon(building.id);
                    entry.buildingName.text = config.displayName;
                    entry.view.transform.SetParent(_buildingColumn.transform);
                    entry.view.transform.localScale = new Vector3(1, 1, 1);
                }

                if (!rewards.ContainsKey(building.id)) rewards.Add(building.id, new Dictionary<string, double>());
                if (!costs.ContainsKey(building.id)) costs.Add(building.id, new Dictionary<string, double>());

                BuildingFunctionalityDelegate.getBuildingResourcesPreSecond(building, rewards[building.id], true);
                BuildingFunctionalityDelegate.getBuildingResourcesPreSecond(building, costs[building.id], false);
            }

            foreach (KeyValuePair<string, RecordsBuildingEntry> entriesPair in _buildingEntries)
            {
                entry = entriesPair.Value;
                Dictionary<string, double> costDict = costs[entriesPair.Key];
                Dictionary<string, double> rewardsDict = rewards[entriesPair.Key];
                int count = 1;
                foreach (KeyValuePair<string, double> costsPair in costDict)
                {
                    entry.populateEntry(entry.jobCostsPanel, count, costsPair.Key, (costsPair.Value * HOUR), true);
                    ++count;
                }
                count = 1;
                foreach (KeyValuePair<string, double> rewardsPair in rewardsDict)
                {
                    entry.populateEntry(entry.jobRewardsPanel, count, rewardsPair.Key, (rewardsPair.Value * HOUR), false);
                    ++count;
                }
            }
        }

        private void buildEntries(Dictionary<string, double> rewardResources, GameObject column, int multiplier = 1, bool showColor = false)
        {
            foreach (KeyValuePair<string, double> pair in rewardResources)
            {
                addEntryToParent(pair.Key, pair.Value, column, multiplier, false, showColor);
            }
            addEntryToParent("", 0, column, 0, true, false);
        }

        private void addEntryToParent(string key, double value, GameObject column, int multiplier, bool isSpacer = false, bool showColor = false)
        {
            ResourceEntryUI entry;
            if (isSpacer)
            {
                entry = new ResourceEntryUI(_prefabManager, key, value, isSpacer, showColor);
            }
            else
            {
                entry = new ResourceEntryUI(_prefabManager, key, (value * multiplier), isSpacer, showColor);
            }
            entry.view.transform.SetParent(column.transform, false);
            _entries.Add(entry);
        }

        private Dictionary<string, double> resourcesCurrent(Dictionary<string, double> reference)
        {
            Dictionary<string, double> current = new Dictionary<string, double>();
            foreach (KeyValuePair<string, double> pair in reference)
            {
                current.Add(pair.Key, JobFactory.player.inventory.getItem(pair.Key).amount);
            }
            return current;
        }

        private void onCloseClick()
        {
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            _uiManager.hideRecordsPanel();
        }



    }
}
