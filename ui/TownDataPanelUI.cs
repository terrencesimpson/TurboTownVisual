using com.super2games.idle.component.goods;
using com.super2games.idle.component.possessor;
using com.super2games.idle.config;
using com.super2games.idle.datastructures;
using com.super2games.idle.delegates.building;
using com.super2games.idle.enums;
using com.super2games.idle.factory;
using com.super2games.idle.service.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class TownDataPanelUI
    {
        private readonly string NEW_TOWN_BUTTON = "NewTownButton";
        private readonly string BLOCK_RIDGE_BUTTON = "BlockRidgeButton";
        private readonly string VLADEUS_BUTTON = "VladeusButton";
        private readonly string NATE_BUTTON = "NateButton";
        private readonly string BTO_BUTTON = "BTOButton";
        private readonly string ZAK_BUTTON = "ZakButton";
        private readonly string ADD_RESOURCES_BUTTON = "AddResourcesButton";
        private readonly string ADD_BOOSTS_BUTTON = "AddBoostsButton";

        private readonly string BLOCK_RIDGE_PATH = "LocalData/TownData_BlockRidge";
        private readonly string VLADUS_PATH = "LocalData/TownData_Vladus";
        private readonly string NATE_PATH = "LocalData/TownData_Nate";
        private readonly string BTO_PATH = "LocalData/TownData_BTO";
        private readonly string ZAK_PATH = "LocalData/TownData_Zak";

        private Button _newTownButton;
        private Button _blockRidgeButton;
        private Button _vladeusButton;
        private Button _nateButton;
        private Button _btoButton;
        private Button _zakButton;
        private Button _addResourcesButton;
        private Button _addBoostsButton;

        private GameObject _townDataPanel;

        public TownDataPanelUI(GameObject panel)
        {
            _townDataPanel = panel;

            _newTownButton = panel.transform.Find(NEW_TOWN_BUTTON).gameObject.GetComponent<Button>();
            _blockRidgeButton = panel.transform.Find(BLOCK_RIDGE_BUTTON).gameObject.GetComponent<Button>();
            _vladeusButton = panel.transform.Find(VLADEUS_BUTTON).gameObject.GetComponent<Button>();
            _nateButton = panel.transform.Find(NATE_BUTTON).gameObject.GetComponent<Button>();
            _btoButton = panel.transform.Find(BTO_BUTTON).gameObject.GetComponent<Button>();
            _zakButton = panel.transform.Find(ZAK_BUTTON).gameObject.GetComponent<Button>();
            _addResourcesButton = panel.transform.Find(ADD_RESOURCES_BUTTON).gameObject.GetComponent<Button>();
            _addBoostsButton = panel.transform.Find(ADD_BOOSTS_BUTTON).gameObject.GetComponent<Button>();

            _newTownButton.onClick.AddListener(newTownButtonClick);
            _blockRidgeButton.onClick.AddListener(blockRidgeButtonClick);
            _vladeusButton.onClick.AddListener(vladeusButtonClick);
            _nateButton.onClick.AddListener(nateButtonClick);
            _btoButton.onClick.AddListener(btoButtonClick);
            _zakButton.onClick.AddListener(zakButtonClick);
            _addResourcesButton.onClick.AddListener(addResourcesClick);
            _addBoostsButton.onClick.AddListener(addBoostsClick);
        }

        private void newTownButtonClick()
        {
            resetMap();
            startTutorial();
        }

        private void blockRidgeButtonClick()
        {
            loadTown(BLOCK_RIDGE_PATH);
        }

        private void vladeusButtonClick()
        {
            loadTown(VLADUS_PATH);
        }

        private void nateButtonClick()
        {
            loadTown(NATE_PATH);
        }

        private void btoButtonClick()
        {
            loadTown(BTO_PATH);
        }

        private void zakButtonClick()
        {
            loadTown(ZAK_PATH);
        }

        private void addResourcesClick()
        {
            addResources();
        }

        private void addBoostsClick()
        {
            addBoosts();
        }

        private void addResources()
        {
            Dictionary<string, IConfig> configs = JobFactory.modelManager.resourcesModel.configs;
            foreach (KeyValuePair<string, IConfig> pair in configs)
            {
                ResourcesConfig config = pair.Value as ResourcesConfig;
                if (config.isInUI)
                {
                    JobFactory.player.inventory.addItem(new Item(pair.Value.id, "", "", null, 99999999999));
                }
            }
            _townDataPanel.SetActive(false);
        }

        private void loadTown(string filePath)
        {
            clear();
            string dataStream = (Resources.Load(filePath) as TextAsset).text;
            JobFactory.buildingManager.buildingsDataStream = dataStream;
            BuildingSaveDataService.deserialize(dataStream);
            JobFactory.buildingManager.createBuildings();
        }

        private void clear()
        {
            JobFactory.buildingManager.buildingSaveDatas = new List<BuildingSaveDataDeprecated>();
            JobFactory.buildingManager.buildingsDataStream = "";
            JobFactory.buildingManager.buildingCount = 0;
            JobFactory.buildingManager.buildingCreationIndex = 0;
            stopTutorial();
            JobFactory.jobManager.clear();
            JobFactory.buildingManager.clear();
            JobFactory.gridManager.reset();
            JobFactory.cubieManager.clear();
            JobFactory.vehicleManager.clear();
            JobFactory.prestigeManager.prestigeSequence.resetCameraPosition();
            _townDataPanel.SetActive(false);
        }

        private void resetMap()
        {
            clear();
            JobFactory.buildingManager.reset();
        }

        private void stopTutorial()
        {
            //JobFactory.tutorialManager.setAllGoalsAsComplete();
            //JobFactory.uiManager.removeHighlights();
            //JobFactory.fxManager.hideBuildingHighlight();
        }

        private void startTutorial()
        {
            //JobFactory.tutorialManager.resetTutorial();
            //JobFactory.tutorialManager.startTutorial();
        }

        private void addBoosts()
        {
            Inventory inventory = JobFactory.player.inventory;
            inventory.addItem(new Item("Boost_404", ItemTypeEnum.BOOST, "", null, 100));
            inventory.addItem(new Item("Boost_414", ItemTypeEnum.BOOST, "", null, 100));
            inventory.addItem(new Item("Boost_424", ItemTypeEnum.BOOST, "", null, 100));
            inventory.addItem(new Item("Boost_434", ItemTypeEnum.BOOST, "", null, 100));
            inventory.addItem(new Item("Boost_444", ItemTypeEnum.BOOST, "", null, 100));
            inventory.addItem(new Item("Boost_454", ItemTypeEnum.BOOST, "", null, 100));
            inventory.addItem(new Item("Boost_464", ItemTypeEnum.BOOST, "", null, 100));
            inventory.addItem(new Item("Boost_474", ItemTypeEnum.BOOST, "", null, 100));
            inventory.addItem(new Item("Boost_484", ItemTypeEnum.BOOST, "", null, 100));
            inventory.addItem(new Item("Boost_494", ItemTypeEnum.BOOST, "", null, 100));
            inventory.addItem(new Item("Boost_504", ItemTypeEnum.BOOST, "", null, 100));
            inventory.addItem(new Item("Boost_514", ItemTypeEnum.BOOST, "", null, 100));
            inventory.addItem(new Item("Boost_524", ItemTypeEnum.BOOST, "", null, 100));
            inventory.addItem(new Item("Boost_534", ItemTypeEnum.BOOST, "", null, 100));
            inventory.addItem(new Item("Boost_544", ItemTypeEnum.BOOST, "", null, 100));
            inventory.addItem(new Item("Boost_554", ItemTypeEnum.BOOST, "", null, 100));
            inventory.addItem(new Item("Boost_564", ItemTypeEnum.BOOST, "", null, 100));
            inventory.addItem(new Item("Boost_574", ItemTypeEnum.BOOST, "", null, 100));
            inventory.addItem(new Item("Boost_584", ItemTypeEnum.BOOST, "", null, 100));
            inventory.addItem(new Item("Boost_594", ItemTypeEnum.BOOST, "", null, 100));
            inventory.addItem(new Item("Boost_604", ItemTypeEnum.BOOST, "", null, 100));
            _townDataPanel.SetActive(false);
        }

    }
}
