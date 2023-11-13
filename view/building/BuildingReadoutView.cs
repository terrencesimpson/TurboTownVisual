using com.super2games.idle.enums;
using com.super2games.idle.component.possessor;
using com.super2games.idle.manager;
using com.super2games.idle.config;
using com.super2games.idle.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using com.super2games.idle.factory;
using com.super2games.idle.delegates.building;
using com.super2games.idle.ui;

namespace com.super2games.idle.view.building
{
    public class BuildingReadoutView : IView
    {
        private readonly string PRESTIGE_PANEL = "PrestigePanel";
        private readonly string NO_POPULATION_OR_JOBS_PANEL = "NoPopulationOrJobsPanel";
        private readonly string RESOURCE_BUILDING_PANEL = "ResourceBuildingPanel";
        private readonly string BUILDING_POPULATION_PANEL = "BuildingPopulationPanel";
        private readonly string BUILDING_UPGRADE_PANEL = "BuildingUpgradePanel";

        private readonly string BUILDING_TYPE_TEXT = "BuildingReadout/buildingTypeText";

        public static readonly string CLOSE_BTN = "closeButton";
        public static readonly string MOVE_BTN = "moveButton";

        private readonly string PRESTIGE_BTN = "PrestigePanel/prestigeButton";
        private readonly string ANGEL_WINGS_TEXT = "PrestigePanel/angelWingsText";

        private readonly string BUILDING_LEVEL_TEXT = "BuildingUpgradePanel/buildingLevelText";
        private readonly string PRODUCTION_BONUS_TEXT = "BuildingUpgradePanel/productionBonusText";

        private readonly string ENHANCING_TEXT = "NextJobUnlockPanel/enhancingText";
        
        private readonly string RESOURCE_PROGRESS = "ResourceBuildingPanel/resourceProgress";
        private readonly string RESOURCE_REWARD = "ResourceBuildingPanel/resourceReward";
        private readonly string RESOURCE_TIME = "ResourceBuildingPanel/resourceTime";
        private readonly string RESOURCE_CYCLES = "ResourceBuildingPanel/resourceCycles";

        private readonly string RESOURCE_ID_TEXT = "ResourceBuildingPanel/ResourceJobEntry/idText";
        private readonly string RESOURCE_REWARD_AMOUNT_TEXT = "ResourceBuildingPanel/ResourceJobEntry/rewardAmount";
        private readonly string RESOURCE_REWARD_RESOURCE_ICON = "ResourceBuildingPanel/ResourceJobEntry/rewardResourceIcon";
        private readonly string RESOURCE_PROGRESS_BAR = "ResourceBuildingPanel/ResourceJobEntry/progressBar";
        private readonly string RESOURCE_PROGRESS_TEXT = "ResourceBuildingPanel/ResourceJobEntry/progressText";

        private readonly Color NORMAL_COLOR = new Color(1, 1, 1);
        private readonly Color DISABLED_COLOR = new Color(.5f, .5f, .5f);

        private GameObject _view;
        public GameObject view { get { return _view; } }

        private GameObject _buildingInfoPanel;
        private GameObject _buildingPopulationPanel;
        private GameObject _buildingUpgradePanel;
        public GameObject prestigePanel;
        public GameObject noPopulationOrJobsPanel;
        public GameObject resourceBuildingPanel;
        private GameObject _slotsEntry;
        private GameObject _jobsPanel;

        public GameObject viewParent;

        private Text _buildingTypeText;
        public Text buildingTypeText { get { return _buildingTypeText; } }

        private Text _buildingLevelText;
        public Text buildingLevelText { get { return _buildingLevelText; } }

        private Text _enhancingText;
        public Text enhancingText { get { return _enhancingText; } }

        private Text _productionBonusText;
        public Text productionBonusText { get { return _productionBonusText; } }

        private Text _resourceCycles;
        public Text resourceCycles { get { return _resourceCycles; } }

        private Text _levelText;
        public Text levelText { get { return _levelText; } }

        private Text _populationText;
        public Text populationText { get { return _populationText; } }

        private Text _resourceIDText;
        public Text resourceIDText { get { return _resourceIDText; } }

        private Text _resourceRewardAmountText;
        public Text resourceRewardAmountText { get { return _resourceRewardAmountText; } }

        private ResourceIconUI _resourceRewardResourceIcon;
        public ResourceIconUI resourceRewardResourceIcon { get { return _resourceRewardResourceIcon; } }

        private GameObject _resourceProgressBar;
        public GameObject resourceProgressBar { get { return _resourceProgressBar; } }

        private Text _resourceProgressText;
        public Text resourceProgressText { get { return _resourceProgressText; } }

        private Button _closeBtn;
        public Button moveBtn;
        public Button prestigeBtn;
        public Text angelWingsText;

        private PrefabManager _prefabManager;
        private UIManager _uiManager;
        private BuildingManager _buildingManager;
        private PrestigeManager _prestigeManager;
        private bool _showBuildingMoveBtn = false;
        private bool _showPrestigeButton = false;

        public BuildingReadoutView(PrefabManager prefabManager, UIManager uiManager, BuildingManager buildingManager, PrestigeManager prestigeManager, bool showBuildingMoveBtn, bool showPrestigeBtn)
        {
            _prefabManager = prefabManager;
            _uiManager = uiManager;
            _buildingManager = buildingManager;
            _showBuildingMoveBtn = showBuildingMoveBtn;
            _showPrestigeButton = showPrestigeBtn;
            _prestigeManager = prestigeManager;
        }

        public void release()
        {
            _view = null;
            _buildingInfoPanel = null;
            _buildingTypeText = null;
            _buildingLevelText = null;
        }

        public void hideAllUI()
        {
            _buildingTypeText.gameObject.SetActive(false);
            _closeBtn.gameObject.SetActive(false);
            _buildingLevelText.gameObject.SetActive(false);
            _enhancingText.gameObject.SetActive(false);
            _productionBonusText.gameObject.SetActive(false);
            _resourceCycles.gameObject.SetActive(false);
            moveBtn.gameObject.SetActive(false);
            prestigeBtn.gameObject.SetActive(false);
            angelWingsText.gameObject.SetActive(false);
            prestigePanel.SetActive(false);
            noPopulationOrJobsPanel.SetActive(false);
            resourceBuildingPanel.SetActive(false);
            _buildingPopulationPanel.SetActive(false);
            _buildingUpgradePanel.SetActive(false);
            _slotsEntry.SetActive(false);
            _jobsPanel.SetActive(false);
        }

        public void showNormalBuildingUI()
        {
            JobFactory.uiManager.showBuildingFunctionPanel();
            _jobsPanel.SetActive(true);
            _slotsEntry.SetActive(true);
            _buildingPopulationPanel.SetActive(true);
            _buildingUpgradePanel.SetActive(true);
            _buildingTypeText.gameObject.SetActive(true);
            _closeBtn.gameObject.SetActive(true);
            _buildingLevelText.gameObject.SetActive(true);
            _enhancingText.gameObject.SetActive(true);
            _productionBonusText.gameObject.SetActive(true);
            moveBtn.gameObject.SetActive(true);
        }

        public void showResourcesUI()
        {
            JobFactory.uiManager.showBuildingFunctionPanel();
            _buildingPopulationPanel.SetActive(true);
            resourceBuildingPanel.SetActive(true);
            _closeBtn.gameObject.SetActive(true);
            _buildingTypeText.gameObject.SetActive(true);
            _resourceCycles.gameObject.SetActive(true);
        }

        public void showPrestigeUI()
        {
            _uiManager.playerInventoryPanelsUI.switchToBuildingDetailsTab();
            JobFactory.uiManager.showBuildingFunctionPanel();
            prestigePanel.SetActive(true);
            _closeBtn.gameObject.SetActive(true);
            _buildingTypeText.gameObject.SetActive(true);
            prestigeBtn.gameObject.SetActive(true);
            angelWingsText.gameObject.SetActive(true);
            moveBtn.gameObject.SetActive(true);
        }

        public void showNoPopulationOrJobsPanel()
        {
            _uiManager.playerInventoryPanelsUI.switchToBuildingDetailsTab();
            JobFactory.uiManager.showBuildingFunctionPanel();
            noPopulationOrJobsPanel.SetActive(true);
            _closeBtn.gameObject.SetActive(true);
            _buildingTypeText.gameObject.SetActive(true);
            moveBtn.gameObject.SetActive(true);
        }

        public void refresh()
        {
            _view = JobFactory.uiManager.buildingDetailsPanel;
            _buildingInfoPanel = JobFactory.uiManager.buildingInfoPanel;
            _slotsEntry = JobFactory.uiManager.slotsEntry;
            _jobsPanel = JobFactory.uiManager.jobsPanel;

            if (!_view.activeSelf) _view.SetActive(true);
            if (!_buildingInfoPanel.activeSelf) _buildingInfoPanel.SetActive(true);

            prestigePanel = _prefabManager.getUIGameObject(_view, PRESTIGE_PANEL);
            noPopulationOrJobsPanel = _prefabManager.getUIGameObject(_view, NO_POPULATION_OR_JOBS_PANEL); 
            resourceBuildingPanel = _prefabManager.getUIGameObject(_view, RESOURCE_BUILDING_PANEL);
            _buildingPopulationPanel = _prefabManager.getUIGameObject(_view, BUILDING_POPULATION_PANEL);
            _buildingUpgradePanel = _prefabManager.getUIGameObject(_view, BUILDING_UPGRADE_PANEL);

            _buildingTypeText = _prefabManager.getUIGameObject(_buildingInfoPanel, BUILDING_TYPE_TEXT).GetComponent<Text>();

            _buildingLevelText = _prefabManager.getUIGameObject(_view, BUILDING_LEVEL_TEXT).GetComponent<Text>();
            _enhancingText = _prefabManager.getUIGameObject(_view, ENHANCING_TEXT).GetComponent<Text>();
            _productionBonusText = _prefabManager.getUIGameObject(_view, PRODUCTION_BONUS_TEXT).GetComponent<Text>();

            _resourceCycles = _prefabManager.getUIGameObject(_view, RESOURCE_CYCLES).GetComponent<Text>();

            _resourceIDText = _prefabManager.getUIGameObject(_view, RESOURCE_ID_TEXT).GetComponent<Text>();
            _resourceRewardAmountText = _prefabManager.getUIGameObject(_view, RESOURCE_REWARD_AMOUNT_TEXT).GetComponent<Text>();
            _resourceRewardResourceIcon = _prefabManager.getUIGameObject(_view, RESOURCE_REWARD_RESOURCE_ICON).GetComponent<ResourceIconUI>();
            _resourceProgressBar = _prefabManager.getUIGameObject(_view, RESOURCE_PROGRESS_BAR);
            _resourceProgressText = _prefabManager.getUIGameObject(_view, RESOURCE_PROGRESS_TEXT).GetComponent<Text>();

            moveBtn = _prefabManager.getUIGameObject(_view, MOVE_BTN).GetComponent<Button>();
            _closeBtn = _prefabManager.getUIGameObject(_view, CLOSE_BTN).GetComponent<Button>();

            _closeBtn.onClick.RemoveAllListeners();
            _closeBtn.onClick.AddListener(onCloseClick);
            
            if (_showBuildingMoveBtn)
            {
                moveBtn.gameObject.SetActive(true);
                moveBtn.onClick.RemoveAllListeners();
                moveBtn.onClick.AddListener(onMoveClick);
            }
            else
            {
                moveBtn.gameObject.SetActive(false);
            }

            prestigeBtn = _prefabManager.getUIGameObject(_view, PRESTIGE_BTN).GetComponent<Button>();
            angelWingsText = _prefabManager.getUIGameObject(_view, ANGEL_WINGS_TEXT).GetComponent<Text>();

            if (_showPrestigeButton)
            {
                angelWingsText.gameObject.SetActive(true);
                prestigeBtn.gameObject.SetActive(true);
                prestigeBtn.onClick.RemoveAllListeners();
                prestigeBtn.onClick.AddListener(onPrestigeClick);
                enablePrestigeButton();
            }
            else
            {
                prestigeBtn.gameObject.SetActive(false);
                angelWingsText.gameObject.SetActive(false);
            }
        }

        private void onCloseClick()
        {
            if (!JobFactory.tutorialManager.isUIFunctional(UIEnum.CLOSE)) return;
            _uiManager.hideBuildingInfoPanel();
            _uiManager.hidePlayerInventoryPanel();
            _uiManager.hideBuildingFunctionPanel();
            JobFactory.recordsManager.uiClick(UIEnum.CLOSE);
        }

        private void onMoveClick()
        {
            if (!JobFactory.tutorialManager.isUIFunctional(UIEnum.MOVE)) return;
            BuildingPlacementDelegate.movePlacement();
            JobFactory.recordsManager.uiClick(UIEnum.MOVE);
        }

        public void disablePrestigeButton()
        {
            if (prestigeBtn == null)
            {
                return;
            }
            prestigeBtn.GetComponent<Button>().enabled = false;
            prestigeBtn.GetComponent<Image>().color = DISABLED_COLOR;
            prestigeBtn.GetComponentInChildren<Text>().color = DISABLED_COLOR;
        }

        public void enablePrestigeButton()
        {
            if (prestigeBtn == null)
            {
                return;
            }
            prestigeBtn.GetComponent<Button>().enabled = true;
            prestigeBtn.GetComponent<Image>().color = NORMAL_COLOR;
            prestigeBtn.GetComponentInChildren<Text>().color = NORMAL_COLOR;
        }

        private void onPrestigeClick()
        {
            _prestigeManager.prestigePopulationReached(_buildingManager.currentSelectedBuilding);
        }
    }
}
