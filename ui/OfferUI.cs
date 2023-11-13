using com.super2games.idle.config;
using com.super2games.idle.enums;
using com.super2games.idle.factory;
using com.super2games.idle.manager;
using com.super2games.idle.utilities;
using com.super2games.idle.utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class OfferUI
    {

        private readonly string OFFER_BUTTON = "OfferButton";

        private readonly string OFFER_IGNITION = "Offer_Ignition";
        private readonly string OFFER_IGNITION_CONFIRM = "Offer_Ignition/ConfirmBtns/ConfirmBtn";
        private readonly string OFFER_IGNITION_NO_THANKS = "Offer_Ignition/ConfirmBtns/CancelBtn";
        private readonly string OFFER_IGNITION_BOOSTS_CONTAINER = "Offer_Ignition/BoostsPanel/BoostsContainer";
        private readonly string OFFER_IGNITION_RESOURCES_CONTAINER = "Offer_Ignition/ResourcesPanel/ResourcesContainer";
        private readonly string OFFER_IGNITION_BUNDLE_PRICE_TEXT = "Offer_Ignition/bundlePriceText";
        private readonly int OFFER_IGNITION_NUM_OF_BOOSTS = 3;
        private readonly int OFFER_IGNITION_NUM_OF_RESOURCES = 2;

        private readonly string OFFER_SPARK = "Offer_Spark";
        private readonly string OFFER_SPARK_CONFIRM = "Offer_Spark/ConfirmBtns/ConfirmBtn";
        private readonly string OFFER_SPARK_NO_THANKS = "Offer_Spark/ConfirmBtns/CancelBtn";
        private readonly string OFFER_SPARK_BOOSTS_CONTAINER = "Offer_Spark/BoostsPanel/BoostsContainer";
        private readonly string OFFER_SPARK_RESOURCES_CONTAINER = "Offer_Spark/ResourcesPanel/ResourcesContainer";
        private readonly string OFFER_SPARK_BUNDLE_PRICE_TEXT = "Offer_Spark/bundlePriceText";
        private readonly int OFFER_SPARK_NUM_OF_BOOSTS = 5;
        private readonly int OFFER_SPARK_NUM_OF_RESOURCES = 2;

        private readonly string OFFER_FLARE = "Offer_Flare";
        private readonly string OFFER_FLARE_CONFIRM = "Offer_Flare/ConfirmBtns/ConfirmBtn";
        private readonly string OFFER_FLARE_NO_THANKS = "Offer_Flare/ConfirmBtns/CancelBtn";
        private readonly string OFFER_FLARE_BOOSTS_CONTAINER = "Offer_Flare/BoostsPanel/BoostsContainer";
        private readonly string OFFER_FLARE_RESOURCES_CONTAINER = "Offer_Flare/ResourcesPanel/ResourcesContainer";
        private readonly string OFFER_FLARE_BUNDLE_PRICE_TEXT = "Offer_Flare/bundlePriceText";
        private readonly int OFFER_FLARE_NUM_OF_BOOSTS = 5;
        private readonly int OFFER_FLARE_NUM_OF_RESOURCES = 2;

        private readonly string OFFER_BLAZE = "Offer_Blaze";
        private readonly string OFFER_BLAZE_CONFIRM = "Offer_Blaze/ConfirmBtns/ConfirmBtn";
        private readonly string OFFER_BLAZE_NO_THANKS = "Offer_Blaze/ConfirmBtns/CancelBtn";
        private readonly string OFFER_BLAZE_BOOSTS_CONTAINER = "Offer_Blaze/BoostsPanel/BoostsContainer";
        private readonly string OFFER_BLAZE_RESOURCES_CONTAINER = "Offer_Blaze/ResourcesPanel/ResourcesContainer";
        private readonly string OFFER_BLAZE_BUNDLE_PRICE_TEXT = "Offer_Blaze/bundlePriceText";
        private readonly int OFFER_BLAZE_NUM_OF_BOOSTS = 6;
        private readonly int OFFER_BLAZE_NUM_OF_RESOURCES = 2;

        private readonly string OFFER_STARTER = "Offer_Starter";
        private readonly string OFFER_STARTER_CONFIRM = "Offer_Starter/ConfirmBtns/ConfirmBtn";
        private readonly string OFFER_STARTER_NO_THANKS = "Offer_Starter/ConfirmBtns/CancelBtn";
        private readonly string OFFER_STARTER_BOOSTS_CONTAINER = "Offer_Starter/BoostsPanel/BoostsContainer";
        private readonly string OFFER_STARTER_RESOURCES_CONTAINER = "Offer_Starter/ResourcesPanel/ResourcesContainer";
        private readonly string OFFER_STARTER_BUNDLE_PRICE_TEXT = "Offer_Starter/bundlePriceText";
        private readonly int OFFER_STARTER_NUM_OF_BOOSTS = 10;
        private readonly int OFFER_STARTER_NUM_OF_RESOURCES = 6;

        private readonly string OFFER_BUILDER = "Offer_Builder";
        private readonly string OFFER_BUILDER_CONFIRM = "Offer_Builder/ConfirmBtns/ConfirmBtn";
        private readonly string OFFER_BUILDER_NO_THANKS = "Offer_Builder/ConfirmBtns/CancelBtn";
        private readonly string OFFER_BUILDER_BOOSTS_CONTAINER = "Offer_Builder/BoostsPanel/BoostsContainer";
        private readonly string OFFER_BUILDER_RESOURCES_CONTAINER = "Offer_Builder/ResourcesPanel";
        private readonly string OFFER_BUILDER_BUNDLE_PRICE_TEXT = "Offer_Builder/bundlePriceText";
        private readonly int OFFER_BUILDER_NUM_OF_BOOSTS = 15;
        private readonly int OFFER_BUILDER_NUM_OF_RESOURCES = 3;

        private readonly string OFFER_MONSTER = "Offer_Monster";
        private readonly string OFFER_MONSTER_CONFIRM = "Offer_Monster/ConfirmBtns/ConfirmBtn";
        private readonly string OFFER_MONSTER_NO_THANKS = "Offer_Monster/ConfirmBtns/CancelBtn";
        private readonly string OFFER_MONSTER_BOOSTS_CONTAINER = "Offer_Monster/BoostsPanel/BoostsContainer";
        private readonly string OFFER_MONSTER_RESOURCES_CONTAINER = "Offer_Monster/ResourcesPanel";
        private readonly string OFFER_MONSTER_BUNDLE_PRICE_TEXT = "Offer_Monster/bundlePriceText";
        private readonly int OFFER_MONSTER_NUM_OF_BOOSTS = 19;
        private readonly int OFFER_MONSTER_NUM_OF_RESOURCES = 1;

        private readonly string OFFER_MASTER = "Offer_Master";
        private readonly string OFFER_MASTER_CONFIRM = "Offer_Master/ConfirmBtns/ConfirmBtn";
        private readonly string OFFER_MASTER_NO_THANKS = "Offer_Master/ConfirmBtns/CancelBtn";
        private readonly string OFFER_MASTER_BOOSTS_CONTAINER = "Offer_Master/BoostsPanel/BoostsContainer";
        private readonly string OFFER_MASTER_RESOURCES_CONTAINER = "Offer_Master/ResourcesPanel";
        private readonly string OFFER_MASTER_BUNDLE_PRICE_TEXT = "Offer_Master/bundlePriceText";
        private readonly int OFFER_MASTER_NUM_OF_BOOSTS = 21;
        private readonly int OFFER_MASTER_NUM_OF_RESOURCES = 1;

        private readonly string OFFER_COMMANDER = "Offer_Commander";
        private readonly string OFFER_COMMANDER_CONFIRM = "Offer_Commander/ConfirmBtns/ConfirmBtn";
        private readonly string OFFER_COMMANDER_NO_THANKS = "Offer_Commander/ConfirmBtns/CancelBtn";
        private readonly string OFFER_COMMANDER_BOOSTS_CONTAINER = "Offer_Commander/BoostsPanel/BoostsContainer";
        private readonly string OFFER_COMMANDER_RESOURCES_CONTAINER = "Offer_Commander/ResourcesPanel";
        private readonly string OFFER_COMMANDER_BUNDLE_PRICE_TEXT = "Offer_Commander/bundlePriceText";
        private readonly int OFFER_COMMANDER_NUM_OF_BOOSTS = 21;
        private readonly int OFFER_COMMANDER_NUM_OF_RESOURCES = 1;

        private readonly string OFFER_EMPEROR = "Offer_Emperor";
        private readonly string OFFER_EMPEROR_CONFIRM = "Offer_Emperor/ConfirmBtns/ConfirmBtn";
        private readonly string OFFER_EMPEROR_NO_THANKS = "Offer_Emperor/ConfirmBtns/CancelBtn";
        private readonly string OFFER_EMPEROR_BOOSTS_CONTAINER = "Offer_Emperor/BoostsPanel/BoostsContainer";
        private readonly string OFFER_EMPEROR_RESOURCES_CONTAINER = "Offer_Emperor/ResourcesPanel";
        private readonly string OFFER_EMPEROR_BUNDLE_PRICE_TEXT = "Offer_Emperor/bundlePriceText";
        private readonly int OFFER_EMPEROR_NUM_OF_BOOSTS = 21;
        private readonly int OFFER_EMPEROR_NUM_OF_RESOURCES = 1;

        private readonly string OFFER_DEITY = "Offer_Deity";
        private readonly string OFFER_DEITY_CONFIRM = "Offer_Deity/ConfirmBtns/ConfirmBtn";
        private readonly string OFFER_DEITY_NO_THANKS = "Offer_Deity/ConfirmBtns/CancelBtn";
        private readonly string OFFER_DEITY_BOOSTS_CONTAINER = "Offer_Deity/BoostsPanel/BoostsContainer";
        private readonly string OFFER_DEITY_RESOURCES_CONTAINER = "Offer_Deity/ResourcesPanel";
        private readonly string OFFER_DEITY_BUNDLE_PRICE_TEXT = "Offer_Deity/bundlePriceText";
        private readonly int OFFER_DEITY_NUM_OF_BOOSTS = 21;
        private readonly int OFFER_DEITY_NUM_OF_RESOURCES = 1;

        private readonly string BOOST_ITEM_ICON = "BoostItemIcon";
        private readonly string RESOURCE_ICON = "ResourceIcon";
        private readonly string RESOURCE_COUNT = "resourceCount";
        private readonly string BOOST_COUNT_TEXT = "boostCountText";
        private readonly string LEFT_ARROW = "leftArrow";
        private readonly string RIGHT_ARROW = "rightArrow";

        private Button _leftArrow;
        private Button _rightArrow;

        private string _currentOfferShowing;
        private List<string> _offers = new List<string>();
        private int _offerIndex = 0;

        private Button _offerButton;
        private GameObject _offerButtonGO;

        private ModelManager _modelManager;
        private ItemsManager _itemsManager;
        private UIManager _uiManager;

        private GameObject _offerIgnition;
        private Button _confirmButton_offerIgnition;
        private Button _cancelButton_offerIgnition;
        private GameObject _boostItemIconsContainer_offerIgnition;
        private List<BoostItemIconUI> _boostItemIcons_offerIgnition = new List<BoostItemIconUI>();
        private List<Text> _boostCountTexts_offerIgnition = new List<Text>();
        private GameObject _resourceIconsContainer_offerIgnition;
        private List<ResourceIconUI> _resourceIcons_offerIgnition = new List<ResourceIconUI>();
        private List<Text> _resourceCountTexts_offerIgnition = new List<Text>();
        private Text _bundlePriceText_offerIgnition;

        private GameObject _offerSpark;
        private Button _confirmButton_offerSpark;
        private Button _cancelButton_offerSpark;
        private GameObject _boostItemIconsContainer_offerSpark;
        private List<BoostItemIconUI> _boostItemIcons_offerSpark = new List<BoostItemIconUI>();
        private List<Text> _boostCountTexts_offerSpark = new List<Text>();
        private GameObject _resourceIconsContainer_offerSpark;
        private List<ResourceIconUI> _resourceIcons_offerSpark = new List<ResourceIconUI>();
        private List<Text> _resourceCountTexts_offerSpark = new List<Text>();
        private Text _bundlePriceText_offerSpark;

        private GameObject _offerFlare;
        private Button _confirmButton_offerFlare;
        private Button _cancelButton_offerFlare;
        private GameObject _boostItemIconsContainer_offerFlare;
        private List<BoostItemIconUI> _boostItemIcons_offerFlare = new List<BoostItemIconUI>();
        private List<Text> _boostCountTexts_offerFlare = new List<Text>();
        private GameObject _resourceIconsContainer_offerFlare;
        private List<ResourceIconUI> _resourceIcons_offerFlare = new List<ResourceIconUI>();
        private List<Text> _resourceCountTexts_offerFlare = new List<Text>();
        private Text _bundlePriceText_offerFlare;

        private GameObject _offerBlaze;
        private Button _confirmButton_offerBlaze;
        private Button _cancelButton_offerBlaze;
        private GameObject _boostItemIconsContainer_offerBlaze;
        private List<BoostItemIconUI> _boostItemIcons_offerBlaze = new List<BoostItemIconUI>();
        private List<Text> _boostCountTexts_offerBlaze = new List<Text>();
        private GameObject _resourceIconsContainer_offerBlaze;
        private List<ResourceIconUI> _resourceIcons_offerBlaze = new List<ResourceIconUI>();
        private List<Text> _resourceCountTexts_offerBlaze = new List<Text>();
        private Text _bundlePriceText_offerBlaze;

        private GameObject _offerStarter;
        private Button _confirmButton_offer1;
        private Button _cancelButton_offer1;
        private GameObject _boostItemIconsContainer_offer1;
        private List<BoostItemIconUI> _boostItemIcons_offer1 = new List<BoostItemIconUI>();
        private List<Text> _boostCountTexts_offer1 = new List<Text>();
        private GameObject _resourceIconsContainer_offer1;
        private List<ResourceIconUI> _resourceIcons_offer1 = new List<ResourceIconUI>();
        private List<Text> _resourceCountTexts_offer1 = new List<Text>();
        private Text _bundlePriceText_offer1;

        private GameObject _offerBuilder;
        private Button _confirmButton_offer2;
        private Button _cancelButton_offer2;
        private GameObject _boostItemIconsContainer_offer2;
        private List<BoostItemIconUI> _boostItemIcons_offer2 = new List<BoostItemIconUI>();
        private List<Text> _boostCountTexts_offer2 = new List<Text>();
        private GameObject _resourceIconsContainer_offer2;
        private List<ResourceIconUI> _resourceIcons_offer2 = new List<ResourceIconUI>();
        private List<Text> _resourceCountTexts_offer2 = new List<Text>();
        private Text _bundlePriceText_offer2;

        private GameObject _offerMonster;
        private Button _confirmButton_offer3;
        private Button _cancelButton_offer3;
        private GameObject _boostItemIconsContainer_offer3;
        private List<BoostItemIconUI> _boostItemIcons_offer3 = new List<BoostItemIconUI>();
        private List<Text> _boostCountTexts_offer3 = new List<Text>();
        private GameObject _resourceIconsContainer_offer3;
        private List<ResourceIconUI> _resourceIcons_offer3 = new List<ResourceIconUI>();
        private List<Text> _resourceCountTexts_offer3 = new List<Text>();
        private Text _bundlePriceText_offer3;

        private GameObject _offerMaster;
        private Button _confirmButton_offerMaster;
        private Button _cancelButton_offerMaster;
        private GameObject _boostItemIconsContainer_offerMaster;
        private List<BoostItemIconUI> _boostItemIcons_offerMaster = new List<BoostItemIconUI>();
        private List<Text> _boostCountTexts_offerMaster = new List<Text>();
        private GameObject _resourceIconsContainer_offerMaster;
        private List<ResourceIconUI> _resourceIcons_offerMaster = new List<ResourceIconUI>();
        private List<Text> _resourceCountTexts_offerMaster = new List<Text>();
        private Text _bundlePriceText_offerMaster;

        private GameObject _offerCommander;
        private Button _confirmButton_offerCommander;
        private Button _cancelButton_offerCommander;
        private GameObject _boostItemIconsContainer_offerCommander;
        private List<BoostItemIconUI> _boostItemIcons_offerCommander = new List<BoostItemIconUI>();
        private List<Text> _boostCountTexts_offerCommander = new List<Text>();
        private GameObject _resourceIconsContainer_offerCommander;
        private List<ResourceIconUI> _resourceIcons_offerCommander = new List<ResourceIconUI>();
        private List<Text> _resourceCountTexts_offerCommander = new List<Text>();
        private Text _bundlePriceText_offerCommander;

        private GameObject _offerEmperor;
        private Button _confirmButton_offerEmperor;
        private Button _cancelButton_offerEmperor;
        private GameObject _boostItemIconsContainer_offerEmperor;
        private List<BoostItemIconUI> _boostItemIcons_offerEmperor = new List<BoostItemIconUI>();
        private List<Text> _boostCountTexts_offerEmperor = new List<Text>();
        private GameObject _resourceIconsContainer_offerEmperor;
        private List<ResourceIconUI> _resourceIcons_offerEmperor = new List<ResourceIconUI>();
        private List<Text> _resourceCountTexts_offerEmperor = new List<Text>();
        private Text _bundlePriceText_offerEmperor;

        private GameObject _offerDeity;
        private Button _confirmButton_offerDeity;
        private Button _cancelButton_offerDeity;
        private GameObject _boostItemIconsContainer_offerDeity;
        private List<BoostItemIconUI> _boostItemIcons_offerDeity = new List<BoostItemIconUI>();
        private List<Text> _boostCountTexts_offerDeity = new List<Text>();
        private GameObject _resourceIconsContainer_offerDeity;
        private List<ResourceIconUI> _resourceIcons_offerDeity = new List<ResourceIconUI>();
        private List<Text> _resourceCountTexts_offerDeity = new List<Text>();
        private Text _bundlePriceText_offerDeity;

        private ShopConfig _shopConfig; //TODO: These popups need to be housed in another data structure for more than one offer popup.
        private Dictionary<string, ShopConfig> _shopConfigs = new Dictionary<string, ShopConfig>();

        public OfferUI(GameObject panel, Button offerButton, ModelManager modelManager, ItemsManager itemsManager, UIManager uiManager)
        {
            _offerButton = offerButton;
            _offerButtonGO = _offerButton.gameObject;
            _modelManager = modelManager;
            _itemsManager = itemsManager;
            _uiManager = uiManager;

            _offerIgnition = panel.transform.Find(OFFER_IGNITION).gameObject;
            _offerSpark = panel.transform.Find(OFFER_SPARK).gameObject;
            _offerFlare = panel.transform.Find(OFFER_FLARE).gameObject;
            _offerBlaze = panel.transform.Find(OFFER_BLAZE).gameObject;

            _offerStarter = panel.transform.Find(OFFER_STARTER).gameObject;
            _offerBuilder = panel.transform.Find(OFFER_BUILDER).gameObject;
            _offerMonster = panel.transform.Find(OFFER_MONSTER).gameObject;

            _offerMaster = panel.transform.Find(OFFER_MASTER).gameObject;
            _offerCommander = panel.transform.Find(OFFER_COMMANDER).gameObject;
            _offerEmperor = panel.transform.Find(OFFER_EMPEROR).gameObject;
            _offerDeity = panel.transform.Find(OFFER_DEITY).gameObject;

            _confirmButton_offerIgnition = panel.transform.Find(OFFER_IGNITION_CONFIRM).gameObject.GetComponent<Button>();
            _cancelButton_offerIgnition = panel.transform.Find(OFFER_IGNITION_NO_THANKS).gameObject.GetComponent<Button>();
            _boostItemIconsContainer_offerIgnition = panel.transform.Find(OFFER_IGNITION_BOOSTS_CONTAINER).gameObject;
            _resourceIconsContainer_offerIgnition = panel.transform.Find(OFFER_IGNITION_RESOURCES_CONTAINER).gameObject;
            _bundlePriceText_offerIgnition = panel.transform.Find(OFFER_IGNITION_BUNDLE_PRICE_TEXT).gameObject.GetComponent<Text>();

            _confirmButton_offerSpark = panel.transform.Find(OFFER_SPARK_CONFIRM).gameObject.GetComponent<Button>();
            _cancelButton_offerSpark = panel.transform.Find(OFFER_SPARK_NO_THANKS).gameObject.GetComponent<Button>();
            _boostItemIconsContainer_offerSpark = panel.transform.Find(OFFER_SPARK_BOOSTS_CONTAINER).gameObject;
            _resourceIconsContainer_offerSpark = panel.transform.Find(OFFER_SPARK_RESOURCES_CONTAINER).gameObject;
            _bundlePriceText_offerSpark = panel.transform.Find(OFFER_SPARK_BUNDLE_PRICE_TEXT).gameObject.GetComponent<Text>();

            _confirmButton_offerFlare = panel.transform.Find(OFFER_FLARE_CONFIRM).gameObject.GetComponent<Button>();
            _cancelButton_offerFlare = panel.transform.Find(OFFER_FLARE_NO_THANKS).gameObject.GetComponent<Button>();
            _boostItemIconsContainer_offerFlare = panel.transform.Find(OFFER_FLARE_BOOSTS_CONTAINER).gameObject;
            _resourceIconsContainer_offerFlare = panel.transform.Find(OFFER_FLARE_RESOURCES_CONTAINER).gameObject;
            _bundlePriceText_offerFlare = panel.transform.Find(OFFER_FLARE_BUNDLE_PRICE_TEXT).gameObject.GetComponent<Text>();

            _confirmButton_offerBlaze = panel.transform.Find(OFFER_BLAZE_CONFIRM).gameObject.GetComponent<Button>();
            _cancelButton_offerBlaze = panel.transform.Find(OFFER_BLAZE_NO_THANKS).gameObject.GetComponent<Button>();
            _boostItemIconsContainer_offerBlaze = panel.transform.Find(OFFER_BLAZE_BOOSTS_CONTAINER).gameObject;
            _resourceIconsContainer_offerBlaze = panel.transform.Find(OFFER_BLAZE_RESOURCES_CONTAINER).gameObject;
            _bundlePriceText_offerBlaze = panel.transform.Find(OFFER_BLAZE_BUNDLE_PRICE_TEXT).gameObject.GetComponent<Text>();

            _confirmButton_offer1 = panel.transform.Find(OFFER_STARTER_CONFIRM).gameObject.GetComponent<Button>();
            _cancelButton_offer1 = panel.transform.Find(OFFER_STARTER_NO_THANKS).gameObject.GetComponent<Button>();
            _boostItemIconsContainer_offer1 = panel.transform.Find(OFFER_STARTER_BOOSTS_CONTAINER).gameObject;
            _resourceIconsContainer_offer1 = panel.transform.Find(OFFER_STARTER_RESOURCES_CONTAINER).gameObject;
            _bundlePriceText_offer1 = panel.transform.Find(OFFER_STARTER_BUNDLE_PRICE_TEXT).gameObject.GetComponent<Text>();

            _confirmButton_offer2 = panel.transform.Find(OFFER_BUILDER_CONFIRM).gameObject.GetComponent<Button>();
            _cancelButton_offer2 = panel.transform.Find(OFFER_BUILDER_NO_THANKS).gameObject.GetComponent<Button>();
            _boostItemIconsContainer_offer2 = panel.transform.Find(OFFER_BUILDER_BOOSTS_CONTAINER).gameObject;
            _resourceIconsContainer_offer2 = panel.transform.Find(OFFER_BUILDER_RESOURCES_CONTAINER).gameObject;
            _bundlePriceText_offer2 = panel.transform.Find(OFFER_BUILDER_BUNDLE_PRICE_TEXT).gameObject.GetComponent<Text>();

            _confirmButton_offer3 = panel.transform.Find(OFFER_MONSTER_CONFIRM).gameObject.GetComponent<Button>();
            _cancelButton_offer3 = panel.transform.Find(OFFER_MONSTER_NO_THANKS).gameObject.GetComponent<Button>();
            _boostItemIconsContainer_offer3 = panel.transform.Find(OFFER_MONSTER_BOOSTS_CONTAINER).gameObject;
            _resourceIconsContainer_offer3 = panel.transform.Find(OFFER_MONSTER_RESOURCES_CONTAINER).gameObject;
            _bundlePriceText_offer3 = panel.transform.Find(OFFER_MONSTER_BUNDLE_PRICE_TEXT).gameObject.GetComponent<Text>();

            _confirmButton_offerMaster = panel.transform.Find(OFFER_MASTER_CONFIRM).gameObject.GetComponent<Button>();
            _cancelButton_offerMaster = panel.transform.Find(OFFER_MASTER_NO_THANKS).gameObject.GetComponent<Button>();
            _boostItemIconsContainer_offerMaster = panel.transform.Find(OFFER_MASTER_BOOSTS_CONTAINER).gameObject;
            _resourceIconsContainer_offerMaster = panel.transform.Find(OFFER_MASTER_RESOURCES_CONTAINER).gameObject;
            _bundlePriceText_offerMaster = panel.transform.Find(OFFER_MASTER_BUNDLE_PRICE_TEXT).gameObject.GetComponent<Text>();

            _confirmButton_offerCommander = panel.transform.Find(OFFER_COMMANDER_CONFIRM).gameObject.GetComponent<Button>();
            _cancelButton_offerCommander = panel.transform.Find(OFFER_COMMANDER_NO_THANKS).gameObject.GetComponent<Button>();
            _boostItemIconsContainer_offerCommander = panel.transform.Find(OFFER_COMMANDER_BOOSTS_CONTAINER).gameObject;
            _resourceIconsContainer_offerCommander = panel.transform.Find(OFFER_COMMANDER_RESOURCES_CONTAINER).gameObject;
            _bundlePriceText_offerCommander = panel.transform.Find(OFFER_COMMANDER_BUNDLE_PRICE_TEXT).gameObject.GetComponent<Text>();

            _confirmButton_offerEmperor = panel.transform.Find(OFFER_EMPEROR_CONFIRM).gameObject.GetComponent<Button>();
            _cancelButton_offerEmperor = panel.transform.Find(OFFER_EMPEROR_NO_THANKS).gameObject.GetComponent<Button>();
            _boostItemIconsContainer_offerEmperor = panel.transform.Find(OFFER_EMPEROR_BOOSTS_CONTAINER).gameObject;
            _resourceIconsContainer_offerEmperor = panel.transform.Find(OFFER_EMPEROR_RESOURCES_CONTAINER).gameObject;
            _bundlePriceText_offerEmperor = panel.transform.Find(OFFER_EMPEROR_BUNDLE_PRICE_TEXT).gameObject.GetComponent<Text>();

            _confirmButton_offerDeity = panel.transform.Find(OFFER_DEITY_CONFIRM).gameObject.GetComponent<Button>();
            _cancelButton_offerDeity = panel.transform.Find(OFFER_DEITY_NO_THANKS).gameObject.GetComponent<Button>();
            _boostItemIconsContainer_offerDeity = panel.transform.Find(OFFER_DEITY_BOOSTS_CONTAINER).gameObject;
            _resourceIconsContainer_offerDeity = panel.transform.Find(OFFER_DEITY_RESOURCES_CONTAINER).gameObject;
            _bundlePriceText_offerDeity = panel.transform.Find(OFFER_DEITY_BUNDLE_PRICE_TEXT).gameObject.GetComponent<Text>();

            populateBoostIcons(OFFER_IGNITION_NUM_OF_BOOSTS, _boostItemIcons_offerIgnition, _boostCountTexts_offerIgnition, _boostItemIconsContainer_offerIgnition, BOOST_ITEM_ICON, BOOST_COUNT_TEXT);
            populateResourceUI(OFFER_IGNITION_NUM_OF_RESOURCES, _resourceIcons_offerIgnition, _resourceCountTexts_offerIgnition, _resourceIconsContainer_offerIgnition, RESOURCE_ICON, RESOURCE_COUNT);

            populateBoostIcons(OFFER_SPARK_NUM_OF_BOOSTS, _boostItemIcons_offerSpark, _boostCountTexts_offerSpark, _boostItemIconsContainer_offerSpark, BOOST_ITEM_ICON, BOOST_COUNT_TEXT);
            populateResourceUI(OFFER_SPARK_NUM_OF_RESOURCES, _resourceIcons_offerSpark, _resourceCountTexts_offerSpark, _resourceIconsContainer_offerSpark, RESOURCE_ICON, RESOURCE_COUNT);

            populateBoostIcons(OFFER_FLARE_NUM_OF_BOOSTS, _boostItemIcons_offerFlare, _boostCountTexts_offerFlare, _boostItemIconsContainer_offerFlare, BOOST_ITEM_ICON, BOOST_COUNT_TEXT);
            populateResourceUI(OFFER_FLARE_NUM_OF_RESOURCES, _resourceIcons_offerFlare, _resourceCountTexts_offerFlare, _resourceIconsContainer_offerFlare, RESOURCE_ICON, RESOURCE_COUNT);

            populateBoostIcons(OFFER_BLAZE_NUM_OF_BOOSTS, _boostItemIcons_offerBlaze, _boostCountTexts_offerBlaze, _boostItemIconsContainer_offerBlaze, BOOST_ITEM_ICON, BOOST_COUNT_TEXT);
            populateResourceUI(OFFER_BLAZE_NUM_OF_RESOURCES, _resourceIcons_offerBlaze, _resourceCountTexts_offerBlaze, _resourceIconsContainer_offerBlaze, RESOURCE_ICON, RESOURCE_COUNT);

            populateBoostIcons(OFFER_STARTER_NUM_OF_BOOSTS, _boostItemIcons_offer1, _boostCountTexts_offer1, _boostItemIconsContainer_offer1, BOOST_ITEM_ICON, BOOST_COUNT_TEXT);
            populateResourceUI(OFFER_STARTER_NUM_OF_RESOURCES, _resourceIcons_offer1, _resourceCountTexts_offer1, _resourceIconsContainer_offer1, RESOURCE_ICON, RESOURCE_COUNT);

            populateBoostIcons(OFFER_BUILDER_NUM_OF_BOOSTS, _boostItemIcons_offer2, _boostCountTexts_offer2, _boostItemIconsContainer_offer2, BOOST_ITEM_ICON, BOOST_COUNT_TEXT);
            populateResourceUI(OFFER_BUILDER_NUM_OF_RESOURCES, _resourceIcons_offer2, _resourceCountTexts_offer2, _resourceIconsContainer_offer2, RESOURCE_ICON, RESOURCE_COUNT);

            populateBoostIcons(OFFER_MONSTER_NUM_OF_BOOSTS, _boostItemIcons_offer3, _boostCountTexts_offer3, _boostItemIconsContainer_offer3, BOOST_ITEM_ICON, BOOST_COUNT_TEXT);
            populateResourceUI(OFFER_MONSTER_NUM_OF_RESOURCES, _resourceIcons_offer3, _resourceCountTexts_offer3, _resourceIconsContainer_offer3, RESOURCE_ICON, RESOURCE_COUNT);

            populateBoostIcons(OFFER_MASTER_NUM_OF_BOOSTS, _boostItemIcons_offerMaster, _boostCountTexts_offerMaster, _boostItemIconsContainer_offerMaster, BOOST_ITEM_ICON, BOOST_COUNT_TEXT);
            populateResourceUI(OFFER_MASTER_NUM_OF_RESOURCES, _resourceIcons_offerMaster, _resourceCountTexts_offerMaster, _resourceIconsContainer_offerMaster, RESOURCE_ICON, RESOURCE_COUNT);

            populateBoostIcons(OFFER_COMMANDER_NUM_OF_BOOSTS, _boostItemIcons_offerCommander, _boostCountTexts_offerCommander, _boostItemIconsContainer_offerCommander, BOOST_ITEM_ICON, BOOST_COUNT_TEXT);
            populateResourceUI(OFFER_COMMANDER_NUM_OF_RESOURCES, _resourceIcons_offerCommander, _resourceCountTexts_offerCommander, _resourceIconsContainer_offerCommander, RESOURCE_ICON, RESOURCE_COUNT);

            populateBoostIcons(OFFER_EMPEROR_NUM_OF_BOOSTS, _boostItemIcons_offerEmperor, _boostCountTexts_offerEmperor, _boostItemIconsContainer_offerEmperor, BOOST_ITEM_ICON, BOOST_COUNT_TEXT);
            populateResourceUI(OFFER_EMPEROR_NUM_OF_RESOURCES, _resourceIcons_offerEmperor, _resourceCountTexts_offerEmperor, _resourceIconsContainer_offerEmperor, RESOURCE_ICON, RESOURCE_COUNT);

            populateBoostIcons(OFFER_DEITY_NUM_OF_BOOSTS, _boostItemIcons_offerDeity, _boostCountTexts_offerDeity, _boostItemIconsContainer_offerDeity, BOOST_ITEM_ICON, BOOST_COUNT_TEXT);
            populateResourceUI(OFFER_DEITY_NUM_OF_RESOURCES, _resourceIcons_offerDeity, _resourceCountTexts_offerDeity, _resourceIconsContainer_offerDeity, RESOURCE_ICON, RESOURCE_COUNT);

            _leftArrow = panel.transform.Find(LEFT_ARROW).gameObject.GetComponent<Button>();
            _rightArrow = panel.transform.Find(RIGHT_ARROW).gameObject.GetComponent<Button>();

            _offerButton.onClick.AddListener(onOfferClick);

            _leftArrow.onClick.AddListener(onLeftArrowClick);
            _rightArrow.onClick.AddListener(onRightArrowClick);

            _confirmButton_offerIgnition.onClick.AddListener(onConfirmClick);
            _cancelButton_offerIgnition.onClick.AddListener(onCancelClick);

            _confirmButton_offerSpark.onClick.AddListener(onConfirmClick);
            _cancelButton_offerSpark.onClick.AddListener(onCancelClick);

            _confirmButton_offerFlare.onClick.AddListener(onConfirmClick);
            _cancelButton_offerFlare.onClick.AddListener(onCancelClick);

            _confirmButton_offerBlaze.onClick.AddListener(onConfirmClick);
            _cancelButton_offerBlaze.onClick.AddListener(onCancelClick);

            _confirmButton_offer1.onClick.AddListener(onConfirmClick);
            _cancelButton_offer1.onClick.AddListener(onCancelClick);

            _confirmButton_offer2.onClick.AddListener(onConfirmClick);
            _cancelButton_offer2.onClick.AddListener(onCancelClick);

            _confirmButton_offer3.onClick.AddListener(onConfirmClick);
            _cancelButton_offer3.onClick.AddListener(onCancelClick);

            _confirmButton_offerMaster.onClick.AddListener(onConfirmClick);
            _cancelButton_offerMaster.onClick.AddListener(onCancelClick);

            _confirmButton_offerCommander.onClick.AddListener(onConfirmClick);
            _cancelButton_offerCommander.onClick.AddListener(onCancelClick);

            _confirmButton_offerEmperor.onClick.AddListener(onConfirmClick);
            _cancelButton_offerEmperor.onClick.AddListener(onCancelClick);

            _confirmButton_offerDeity.onClick.AddListener(onConfirmClick);
            _cancelButton_offerDeity.onClick.AddListener(onCancelClick);

            //showOfferButton(); //TESTING: For Offers
        }

        private void populateBoostIcons(int numOfIcons, List<BoostItemIconUI> icons, List<Text> iconTexts, GameObject iconContainer, string iconName, string iconCountText)
        {
            for (int i = 1; i <= numOfIcons; ++i)
            {
                icons.Add(iconContainer.transform.Find(iconName + i).gameObject.GetComponent<BoostItemIconUI>());
                iconTexts.Add(iconContainer.transform.Find((iconName + i) + "/" + iconCountText).gameObject.GetComponent<Text>());
            }
        }

        private void populateResourceUI(int numOfIcons, List<ResourceIconUI> icons, List<Text> iconTexts, GameObject iconContainer, string iconName, string iconCountText)
        {
            for (int i = 1; i <= numOfIcons; ++i)
            {
                icons.Add(iconContainer.transform.Find((iconName + i) + "/" + iconName).gameObject.GetComponent<ResourceIconUI>());
                iconTexts.Add(iconContainer.transform.Find((iconName + i) + "/" + iconCountText).gameObject.GetComponent<Text>());
            }
        }

        private void onLeftArrowClick()
        {
            --_offerIndex;
            if (_offerIndex < 0)
            {
                _offerIndex = _offers.Count - 1;
            }
            _currentOfferShowing = _offers[_offerIndex];
            showOfferPanel();
        }

        private void onRightArrowClick()
        {
            ++_offerIndex;
            if (_offerIndex > (_offers.Count - 1))
            {
                _offerIndex = 0;
            }
            _currentOfferShowing = _offers[_offerIndex];
            showOfferPanel();
        }

        public void showOffer(ShopConfig shopConfig) //Populate the UI based off the ShopConfig and then the ItemConfigs it's related to.
        {
            if (!_shopConfigs.ContainsValue(shopConfig))
            {
                _shopConfigs.Add(shopConfig.thirdPartyID, shopConfig);
                _offers.Add(shopConfig.thirdPartyID);
                populateOfferUI(shopConfig);
            }
            if (_shopConfigs.Count > 1)
            {
                _leftArrow.gameObject.SetActive(true);
                _rightArrow.gameObject.SetActive(true);
            }
            _currentOfferShowing = shopConfig.thirdPartyID;
            showOfferButton();
            showOfferPanel();
        }

        public void hideOffer(ShopConfig shopConfig) //Doesn't need shop config now, but in the future will have to close the appropriate offer.
        {
            _shopConfigs.Remove(shopConfig.thirdPartyID);
            _offers.Remove(shopConfig.thirdPartyID);
            hideOfferButton();
            hideOfferPanel();
        }

        public void populateOfferUI(ShopConfig shopConfig)
        {
            if (shopConfig.thirdPartyID == KongregatePurchasableEnum.TT_OFFER_1) populateUI(shopConfig, _bundlePriceText_offer1, _boostItemIcons_offer1, _resourceIcons_offer1, _boostCountTexts_offer1, _resourceCountTexts_offer1);
            else if (shopConfig.thirdPartyID == KongregatePurchasableEnum.TT_OFFER_2) populateUI(shopConfig, _bundlePriceText_offer2, _boostItemIcons_offer2, _resourceIcons_offer2, _boostCountTexts_offer2, _resourceCountTexts_offer2);
            else if (shopConfig.thirdPartyID == KongregatePurchasableEnum.TT_OFFER_3) populateUI(shopConfig, _bundlePriceText_offer3, _boostItemIcons_offer3, _resourceIcons_offer3, _boostCountTexts_offer3, _resourceCountTexts_offer3);
            else if (shopConfig.thirdPartyID == KongregatePurchasableEnum.TT_IGNITION_OFFER) populateUI(shopConfig, _bundlePriceText_offerIgnition, _boostItemIcons_offerIgnition, _resourceIcons_offerIgnition, _boostCountTexts_offerIgnition, _resourceCountTexts_offerIgnition);
            else if (shopConfig.thirdPartyID == KongregatePurchasableEnum.TT_SPARK_OFFER) populateUI(shopConfig, _bundlePriceText_offerSpark, _boostItemIcons_offerSpark, _resourceIcons_offerSpark, _boostCountTexts_offerSpark, _resourceCountTexts_offerSpark);
            else if (shopConfig.thirdPartyID == KongregatePurchasableEnum.TT_FLARE_OFFER) populateUI(shopConfig, _bundlePriceText_offerFlare, _boostItemIcons_offerFlare, _resourceIcons_offerFlare, _boostCountTexts_offerFlare, _resourceCountTexts_offerFlare);
            else if (shopConfig.thirdPartyID == KongregatePurchasableEnum.TT_BLAZE_OFFER) populateUI(shopConfig, _bundlePriceText_offerBlaze, _boostItemIcons_offerBlaze, _resourceIcons_offerBlaze, _boostCountTexts_offerBlaze, _resourceCountTexts_offerBlaze);
            else if (shopConfig.thirdPartyID == KongregatePurchasableEnum.TT_MASTER_OFFER) populateUI(shopConfig, _bundlePriceText_offerMaster, _boostItemIcons_offerMaster, _resourceIcons_offerMaster, _boostCountTexts_offerMaster, _resourceCountTexts_offerMaster);
            else if (shopConfig.thirdPartyID == KongregatePurchasableEnum.TT_COMMANDER_OFFER) populateUI(shopConfig, _bundlePriceText_offerCommander, _boostItemIcons_offerCommander, _resourceIcons_offerCommander, _boostCountTexts_offerCommander, _resourceCountTexts_offerCommander);
            else if (shopConfig.thirdPartyID == KongregatePurchasableEnum.TT_EMPEROR_OFFER) populateUI(shopConfig, _bundlePriceText_offerEmperor, _boostItemIcons_offerEmperor, _resourceIcons_offerEmperor, _boostCountTexts_offerEmperor, _resourceCountTexts_offerEmperor);
            else if (shopConfig.thirdPartyID == KongregatePurchasableEnum.TT_DEITY_OFFER) populateUI(shopConfig, _bundlePriceText_offerDeity, _boostItemIcons_offerDeity, _resourceIcons_offerDeity, _boostCountTexts_offerDeity, _resourceCountTexts_offerDeity);
        }

        private void populateUI(ShopConfig shopConfig, Text priceText, List<BoostItemIconUI> boostItemIcons, List<ResourceIconUI> resourceIcons, List<Text> boostCountTexts, List<Text> resourceCountTexts)
        {
            priceText.text = CurrencyUtility.getDescription(shopConfig);
            ItemConfig itemConfig = _modelManager.itemsModel.getConfig(shopConfig.itemID) as ItemConfig;
            string[] itemPool = itemConfig.itemPool;
            int boostIndex = 0;
            int resourceIndex = 0;
            for (int i = 0; i < itemPool.Length; ++i)
            {
                string itemID = itemPool[i];
                itemConfig = _modelManager.itemsModel.getConfig(itemID) as ItemConfig;
                if (itemConfig.type == ItemTypeEnum.BOOST)
                {
                    BoostConfig boostConfig = _modelManager.boostsModel.getConfig(itemConfig.itemID) as BoostConfig;
                    boostItemIcons[boostIndex].setIcon(boostConfig.id, boostConfig.imgPath, boostConfig.rank);
                    boostCountTexts[boostIndex].text = "x" + StringUtility.toNumString(itemConfig.amount);
                    boostIndex++;
                }
                else if (itemConfig.type == ItemTypeEnum.RESOURCE)
                {
                    resourceIcons[resourceIndex].setIcon(itemConfig.itemID);
                    resourceCountTexts[resourceIndex].text = StringUtility.toNumString(itemConfig.amount);
                    resourceIndex++;
                }
            }
        }

        private void debug_onOfferClick()
        {
            if (StartUpManager.DEBUG)
            {
                showOffer(_modelManager.shopModel.getConfig("Shop_56700") as ShopConfig);
                showOffer(_modelManager.shopModel.getConfig("Shop_56750") as ShopConfig);
                showOffer(_modelManager.shopModel.getConfig("Shop_56800") as ShopConfig);
                showOffer(_modelManager.shopModel.getConfig("Shop_80000") as ShopConfig);
                showOffer(_modelManager.shopModel.getConfig("Shop_81000") as ShopConfig);
                showOffer(_modelManager.shopModel.getConfig("Shop_82000") as ShopConfig);
                showOffer(_modelManager.shopModel.getConfig("Shop_83000") as ShopConfig);
                showOffer(_modelManager.shopModel.getConfig("Shop_84000") as ShopConfig);
                showOffer(_modelManager.shopModel.getConfig("Shop_85000") as ShopConfig);
                showOffer(_modelManager.shopModel.getConfig("Shop_86000") as ShopConfig);
                showOffer(_modelManager.shopModel.getConfig("Shop_87000") as ShopConfig);
            }
        }

        private void onOfferClick()
        {
            //if (_offers.Count <= 0)
            //{
            //    return;
            //}
            debug_onOfferClick();
            showOffer(_shopConfigs[_offers[0]]);
            JobFactory.playFabManager.analytics(AnalyticsEnum.OFFER_BUTTON_CLICKED);
        }

        private void onConfirmClick()
        {
            hideOfferPanel();
            _itemsManager.purchaseShopItem(_shopConfigs[_currentOfferShowing]);
            JobFactory.playFabManager.analytics(AnalyticsEnum.OFFER_BUY_NOW_CLICKED);
        }

        private void onCancelClick()
        {
            hideOfferPanel();
            JobFactory.playFabManager.analytics(AnalyticsEnum.OFFER_NO_THANKS_CLICKED);
        }

        public void showOfferPanel()
        {
            _uiManager.showOffersPanel();
            showCurrentOffer();
        }

        public void hideOfferPanel()
        {
            _uiManager.hideOffersPanel();
        }

        public void showCurrentOffer()  //matches: shopConfig.kongID
        {
            _offerStarter.SetActive(false);
            _offerBuilder.SetActive(false);
            _offerMonster.SetActive(false);
            _offerIgnition.SetActive(false);
            _offerSpark.SetActive(false);
            _offerFlare.SetActive(false);
            _offerBlaze.SetActive(false);
            _offerMaster.SetActive(false);
            _offerCommander.SetActive(false);
            _offerEmperor.SetActive(false);
            _offerDeity.SetActive(false);

            if (_currentOfferShowing == KongregatePurchasableEnum.TT_OFFER_1)  _offerStarter.SetActive(true);
            else if (_currentOfferShowing == KongregatePurchasableEnum.TT_OFFER_2) _offerBuilder.SetActive(true);
            else if (_currentOfferShowing == KongregatePurchasableEnum.TT_OFFER_3) _offerMonster.SetActive(true);
            else if (_currentOfferShowing == KongregatePurchasableEnum.TT_IGNITION_OFFER) _offerIgnition.SetActive(true);
            else if (_currentOfferShowing == KongregatePurchasableEnum.TT_SPARK_OFFER) _offerSpark.SetActive(true);
            else if (_currentOfferShowing == KongregatePurchasableEnum.TT_FLARE_OFFER) _offerFlare.SetActive(true);
            else if (_currentOfferShowing == KongregatePurchasableEnum.TT_BLAZE_OFFER) _offerBlaze.SetActive(true);
            else if (_currentOfferShowing == KongregatePurchasableEnum.TT_MASTER_OFFER) _offerMaster.SetActive(true);
            else if (_currentOfferShowing == KongregatePurchasableEnum.TT_COMMANDER_OFFER) _offerCommander.SetActive(true);
            else if (_currentOfferShowing == KongregatePurchasableEnum.TT_EMPEROR_OFFER) _offerEmperor.SetActive(true);
            else if (_currentOfferShowing == KongregatePurchasableEnum.TT_DEITY_OFFER) _offerDeity.SetActive(true);
        }

        public void showOfferButton()
        {
            _offerButtonGO.SetActive(true);
        }

        public void hideOfferButton()
        {
            if (_shopConfigs.Count <= 0)
            {
                _offerButtonGO.SetActive(false);
            }
        }

    }
}
