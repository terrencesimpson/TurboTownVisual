using com.super2games.idle.component.goods;
using com.super2games.idle.component.possessor;
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
using com.super2games.idle.factory;
using com.super2games.idle.delegates.building;
//using Linq;

namespace com.super2games.idle.ui
{
    public class PlayerBoostListUI
    {
        private readonly string PLAYER_BOOST_ITEMS_CONTENT_PREFAB = "Prefabs/ui/PlayerBoostItemsContent";
        private readonly string PAGE_BTNS_CONTAINER = "pageBtnsContainer";
        private readonly string GET_ALL_BUTTON = "getAllButton";

        private readonly int NUM_ENTRIES_PER_PAGE = 20;

        private PagedUI _pagedUI;
        private GameObject _pageBtnsContainer;

        private readonly Color ICON_NORMAL_COLOR = new Color(1, 1, 1);
        private readonly Color ICON_DARK_COLOR = new Color(.5f,.5f,.5f);
        private GameObject _boostItemsPanel;
        private PrefabManager _prefabManager;
        private Player _player;

        private ModelManager _modelManager;
        private BuildingManager _buildingManager;

        private Button _getAllButton;

        private Dictionary<string, PlayerBoostEntryUI> _entries = new Dictionary<string, PlayerBoostEntryUI>();

        private bool _fingerPositionFrameWaitState = false;
        private bool _fingerHighlightState = false;
        private int _fingerHighlightFrameCount = int.MaxValue;
        private string _fingerRotationState;

        public PlayerBoostListUI(GameObject boostItemsPanel, Player player, PrefabManager prefabManager, ModelManager modelManager, BuildingManager buildingManager)
        {
            _boostItemsPanel = boostItemsPanel;
            _player = player;
            _prefabManager = prefabManager;
            _modelManager = modelManager;
            _buildingManager = buildingManager;
            _pageBtnsContainer = _boostItemsPanel.transform.Find(PAGE_BTNS_CONTAINER).gameObject;
            _getAllButton = _boostItemsPanel.transform.Find(GET_ALL_BUTTON).gameObject.GetComponent<Button>();

            _getAllButton.onClick.AddListener(onGetAllClick);

            _pagedUI = _pageBtnsContainer.GetComponent<PagedUI>();
            _pagedUI.init(PLAYER_BOOST_ITEMS_CONTENT_PREFAB, boostItemsPanel, _pageBtnsContainer, prefabManager, NUM_ENTRIES_PER_PAGE);
        }

        public void postInitialize()
        {
            buildListUI();
        }

        public void update()
        {   //### SUPER DISGUSTING HACK! ###
            //This is just horrible. You have to wait a frame to get the GridLayout positioning correct or the hand will be incorrectly placed.
            if (_fingerHighlightState && !_fingerPositionFrameWaitState && _fingerHighlightFrameCount < Time.frameCount)
            {
                _fingerPositionFrameWaitState = true;
                JobFactory.uiManager.createHighlight(getFirst().view, _fingerRotationState);
            }

        }

        public void highlightUI(string uiID, string fingerRotationState)
        {
            if (uiID == UIEnum.BUILDING_BOOST)
            {
                _fingerHighlightState = true;
                _fingerPositionFrameWaitState = false;
                _fingerRotationState = fingerRotationState;
                _fingerHighlightFrameCount = (Time.frameCount + 2); //+2 just to be sure. For this disgusting hack...
            }
        }

        public void resetHighlightState()
        {
            _fingerHighlightState = false;
            _fingerHighlightFrameCount = int.MaxValue;
        }

        private PlayerBoostEntryUI getFirst()
        {
            foreach (KeyValuePair<string, PlayerBoostEntryUI> pair in _entries)
            {
                return pair.Value;
            }
            return null;
        }

        private void onGetAllClick()
        {
            BuildingUtilityDelegate.returnAllBuildingBoostsToPlayerInventoryAndRefresh();
        }

        public void buildListUI()
        {
            foreach (KeyValuePair<string, Item> pair in _player.inventory.items)
            {
                createEntry(pair.Key, pair.Value.amount);
            }
        }

        public void updateEntry(string boostID, double amount)
        {
            if (!_entries.ContainsKey(boostID))
            {
                createEntry(boostID, amount);
                return;
            }

            PlayerBoostEntryUI entry = _entries[boostID];
            entry.boostCountText.text = StringUtility.toNumString(amount);
        }

        //it would be good to sort by 
        //Type->Specific Item ->Ranks
        //I think I need some sort of higher level sorting going on.
        //For example, this function should just pull the "blocks" of usable item types and items (sorted by rank)
        public void sortListBasedOnBuilding(BuildingsConfig buildingConfig)
        {
            _pagedUI.clearAllPages(true);

            String[] buildingTypes = buildingConfig.types;
            List<PlayerBoostEntryUI> unsortedList = sortListByTypeAndRank();
            List<PlayerBoostEntryUI> sortedList = new List<PlayerBoostEntryUI>();
            int entryMovedToFrontCount = 0;

            for (int i=0;i<unsortedList.Count;i++)
            {
                PlayerBoostEntryUI entry = unsortedList[i];

                entry.view.GetComponent<Image>().color = ICON_DARK_COLOR;

                if (buildingTypes.Contains(entry.interactive.boostConfig.target) || entry.interactive.boostConfig.target == BoostTargetsEnum.ANY_BUILDING)
                {
                    //Exeption for boosts that increase range
                    if (entry.interactive.boostConfig.type == BoostTypeEnum.INCREASE_BOOST_RANGE && !_modelManager.buildingBoostersModel.configs.ContainsKey(buildingConfig.id))
                    {
                        sortedList.Add(entry);
                        entry.view.GetComponent<Image>().color = ICON_DARK_COLOR;
                        continue;
                    }

                    if (ComponentContainerTypeEnum.isPark(buildingConfig.id) && entry.interactive.boostConfig.type != BoostTypeEnum.INCREASE_BOOST_RANGE)
                    {   //For Parks. They should only get range boosts.
                        continue;
                    }

                    sortedList.Insert(entryMovedToFrontCount, entry);
                    entryMovedToFrontCount++;
                    entry.view.GetComponent<Image>().color = ICON_NORMAL_COLOR;
                }
                else
                {
                    sortedList.Add(entry);
                    entry.view.GetComponent<Image>().color = ICON_DARK_COLOR;
                }
            }

            for (int i=0; i<sortedList.Count; i++)
            {
                _pagedUI.addEntryToPage(sortedList[i].view);
            }

            _pagedUI.showPage(0);
        }

        private List<PlayerBoostEntryUI> sortListByTypeAndRank()
        {
            List<PlayerBoostEntryUI> unsortedList = new List<PlayerBoostEntryUI>();

            foreach (KeyValuePair<string, PlayerBoostEntryUI> pair in _entries)
            {
                unsortedList.Add(pair.Value);
            }

            List<PlayerBoostEntryUI> sortedList = new List<PlayerBoostEntryUI>();

            if (unsortedList.Count > 0)
            {
                sortedList = unsortedList.OrderBy(entry => entry.interactive.boostConfig.target).ThenBy(entry => entry.interactive.boostConfig.name).ThenBy(entry => entry.interactive.boostConfig.rank).ToList();
            }

            return sortedList;
        }

        public void removeEntry(string boostID)
        {
            if (!_entries.ContainsKey(boostID))
            {
                return; //Must have already been removed.
            }

            PlayerBoostEntryUI entry = _entries[boostID];

            _pagedUI.removeEntry(entry.view);
            //entry.view.transform.SetParent(null);

            GameObject.Destroy(entry.view);
            _entries.Remove(boostID);
        }

        private void createEntry(string boostID, double amount)
        {
            BoostConfig boostConfig = _modelManager.boostsModel.getConfig(boostID) as BoostConfig;
            if (boostConfig == null || boostConfig.level != BoostLevelEnum.BUILDING || _entries.ContainsKey(boostID))
            {
                return;
            }

            PlayerBoostEntryUI entry = new PlayerBoostEntryUI(_prefabManager);
            entry.interactive.boostConfig = boostConfig;
            entry.interactive.buildingManager = _buildingManager;
            entry.interactive.modelManager = _modelManager;
            entry.interactive.playerBoostListUI = this;
            entry.boostNameText.text = boostConfig.name;
            entry.boostCountText.text = StringUtility.toNumString(amount);
            _pagedUI.addEntryToPage(entry.view);
            _entries.Add(boostConfig.id, entry);

            BoostItemIconUI icon = entry.view.GetComponent<BoostItemIconUI>();
            icon.setIcon(boostID, boostConfig.imgPath, boostConfig.rank);
        }

    }
}
