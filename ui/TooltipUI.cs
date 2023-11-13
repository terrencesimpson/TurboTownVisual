using UnityEngine;
using UnityEngine.UI;
using com.super2games.idle.manager;
using com.super2games.idle.config;
using com.super2games.idle.utilities;
using com.super2games.idle.component.possessor;
using com.super2games.idle.component.goods;
using com.super2games.idle.enums;
using com.super2games.idle.component.boosts.collection;
using com.super2games.idle.component.boosts.player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.super2games.idle.factory;

public class TooltipUI : MonoBehaviour {

    public static readonly string TOOLTIP_TYPE_GENERIC = "generic";
    public static readonly string TOOLTIP_TYPE_TITLE_AND_DESCRIPTION = "titleAndDescription";

    public static readonly string ASSET_TYPE_RESOURCE_ICON = "assetResourceIcon";
    public static readonly string ASSET_TYPE_BOOST_ICON = "assetBoostIcon";
    public static readonly string ASSET_TYPE_BUILDING_ICON = "assetBuildingIcon";
    public static readonly string ASSET_TYPE_PLAYER_SLOT_BOOST_ICON = "assetPlayerSlotBoostIcon";

    public static readonly int OFFSET_X = -20;//40
    public static readonly int OFFSET_Y = 30;//-10 //65

    public static TooltipUI instance;

    public GameObject genericPanel;
    public GameObject titleAndDescriptionPanel;

    //private Text _currentTextField;

    private ModelManager _modelManager;
    public ModelManager modelManager { set { _modelManager = value; } }

    private Player _player;
    public Player player { set { _player = value; } }

    void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void displaySimpleTooltip(string titleString, Vector3 pos, string tipType = "", string descriptionString = "", int offsetX=0, int offsetY=0)
    {
        if (!JobFactory.tutorialManager.isComplete)
        {
            return;
        }

        gameObject.SetActive(true);

        Text titleText = titleAndDescriptionPanel.transform.GetChild(0).GetComponent<Text>();
        Text descriptionText = titleAndDescriptionPanel.transform.GetChild(1).GetComponent<Text>();

        string title = titleString;
        string description = descriptionString;

        if (tipType == "")
        {
            tipType = TOOLTIP_TYPE_GENERIC;
        }

        if (tipType == TOOLTIP_TYPE_GENERIC)
        {
            Text genericText = genericPanel.GetComponentInChildren<Text>();
            genericPanel.SetActive(true);
            genericText.text = title;

            determinePosition(pos, genericPanel, OFFSET_X+ offsetX, OFFSET_Y+ offsetY);
        }
        else if (tipType == TOOLTIP_TYPE_TITLE_AND_DESCRIPTION)
        {
            titleAndDescriptionPanel.SetActive(true);
            titleText.text = title;
            descriptionText.text = description;

            determinePosition(pos, titleAndDescriptionPanel, OFFSET_X + offsetX, OFFSET_Y + offsetY);
        }
    }

    public void displayItemIDBasedTooltip(string itemID, Vector3 pos, string tipType, string assetType)
    {
        if (!JobFactory.tutorialManager.isComplete || itemID == "")
        {
            return;
        }

        Text titleText = titleAndDescriptionPanel.transform.GetChild(0).GetComponent<Text>();
        Text descriptionText = titleAndDescriptionPanel.transform.GetChild(1).GetComponent<Text>();

        string completedText = "";
        string name = "";
        string description = "";
        string target = "";
        string types = "Type: ";
        string boostTypes = "Enhances: ";
        string boostAmount = "Enhancement Percentage: ";
        string boostRange = "Enhancement Range: ";
        string jobProduces = "Produces: ";
        string jobConsumes = "Consumes: ";
        string popCap = "Population Cap: ";
        string numOwned = "Inventory: ";
        string angelWings = "Angel Wings: ";
        string plunkoValue = "Plunko Value: ";

        string subString = "";

        gameObject.SetActive(true);

        //Debug.Log("[ToolTipUI] displayTooltip() itemID: " + itemID);

        if (tipType == TOOLTIP_TYPE_GENERIC)
        {
            Text genericText = genericPanel.GetComponentInChildren<Text>();
            genericPanel.SetActive(true);

            if (assetType == ASSET_TYPE_RESOURCE_ICON)
            {
                ResourcesConfig resourceConfig = _modelManager.resourcesModel.getConfig(itemID) as ResourcesConfig;
                Item item = _player.inventory.getItem(itemID);

                if (item == null)
                {
                    ConsoleUtility.Log("[TooltipUI].displayItemIDBasedTooltip -- Requesting an item that doesn't exist in the Inventory: " + itemID);
                    return; //requesting an item that doesn't exist in the inventory.
                }

                description = resourceConfig.description;

                numOwned += StringUtility.toNumString(item.amount);

                genericText.text = description + "\n" + numOwned;

                determinePosition(pos, genericPanel, OFFSET_X, OFFSET_Y);
            }
            else if (assetType == ASSET_TYPE_BUILDING_ICON)
            {
                //display what the building can produce, what it can expend, if it boosts building what it boosts, and what the range is
                //BUILDING TYPES
                BuildingsConfig buildingConfig = _modelManager.buildingsModel.configs[itemID] as BuildingsConfig;

                angelWings += buildingConfig.angelWings;
                plunkoValue += buildingConfig.prestigeValue;

                //if prestige building, present unique tooltip
                if (buildingConfig.isPrestigeBuilding)
                {
                    completedText = "Mr. Business only has your town's best interests\nin mind. Especially that part about the interest\nyou owe him for that initial loan...\nThe end is nigh!";
                    genericText.text = completedText;

                    determinePosition(pos, genericPanel, OFFSET_X * 2, OFFSET_Y * 5, ASSET_TYPE_BUILDING_ICON);
                    return;
                }

                if (buildingConfig.cap != 0)
                {
                    popCap += buildingConfig.cap;
                }
                else
                {
                    popCap += "NA";
                }

                for (int i=0;i< buildingConfig.types.Length;i++)
                {
                    subString = StringUtility.capitalizeFirstLetter(buildingConfig.types[i]);
                    subString = StringUtility.AddSpacesToSentence(subString, false);

                    if (i == buildingConfig.types.Length-1)
                    {
                        types += subString;
                    }
                    else
                    {
                        types += subString + ", ";
                    }
                    
                }

                //BOOST TYPES
                BuildingBoostersConfig boosterConfig;
                if (_modelManager.buildingBoostersModel.configs.ContainsKey(itemID) != false)
                {
                    boosterConfig = _modelManager.buildingBoostersModel.configs[itemID] as BuildingBoostersConfig;
                    BoostConfig boostConfig = _modelManager.boostsModel.getConfig(boosterConfig.boostID) as BoostConfig;

                    for (int k = 0; k < boosterConfig.buildingTypes.Length; k++)
                    {
                        subString = StringUtility.capitalizeFirstLetter(boosterConfig.buildingTypes[k]);
                        subString = StringUtility.AddSpacesToSentence(subString, false);

                        if (k == boosterConfig.buildingTypes.Length - 1)
                        {
                            boostTypes += subString;
                        }
                        else
                        {
                            boostTypes += subString + ", ";
                        }
                    }

                    boostRange += boosterConfig.range;
                    boostAmount += StringUtility.percentToString((float)boostConfig.amount);
                }
                else
                {
                    boostTypes += "NA";
                    boostAmount += "NA";
                    boostRange += "NA";
                }

                //RESOURCE PRODUCTION
                //JobsConfig jobConfig = _modelManager.jobsModel
                //_modelManager.jobsModel.
                List<IConfig> jobs;
                if (_modelManager.jobsModel.hasIDInConfigsList(itemID) != false)
                {
                    jobs = _modelManager.jobsModel.getConfigsListWithID(itemID);
                    JobsConfig jobsConfig;
                    for (int m = 0; m < jobs.Count; m++)
                    {
                        jobsConfig = jobs[m] as JobsConfig;

                        for (int u = 0; u < jobsConfig.rewardResources.Length; u++)
                        {
                            if (jobsConfig.rewardResources[u] != "NA")
                            {
                                if (jobProduces.Contains(jobsConfig.rewardResources[u]) == false)
                                {
                                        jobProduces += jobsConfig.rewardResources[u] + ", ";
                                }
                            }
                        }

                        for (int t = 0; t < jobsConfig.jobResourcesToRun.Length; t++)
                        {
                            if (jobsConfig.jobResourcesToRun[t] != "NA")
                            {
                                if (jobConsumes.Contains(jobsConfig.jobResourcesToRun[t]) == false)
                                {
                                        jobConsumes += jobsConfig.jobResourcesToRun[t] + ", ";
                                }
                            }
                        }

                        if (jobsConfig.jobResourcesToRun.Length == 0)
                        {
                            jobConsumes += "NA";
                        }
                    }

                    jobProduces = StringUtility.AddSpacesToSentence(jobProduces, false, false);
                    jobConsumes = StringUtility.AddSpacesToSentence(jobConsumes, false, false);

                    if (jobProduces != "NA")
                    {
                        jobProduces = jobProduces.Remove(jobProduces.Length - 2);
                    }

                    if (jobConsumes != "NA")
                    {
                        jobConsumes = jobConsumes.Remove(jobConsumes.Length - 2);
                    }
                }
                else
                {
                    jobProduces += "NA";
                    jobConsumes += "NA";
                }

                completedText = types + "\n" + angelWings + "\n" + plunkoValue + "\n" + popCap + "\n" + jobProduces + "\n" + jobConsumes + "\n" + boostTypes + "\n" + boostAmount + "\n" + boostRange;
                genericText.text = completedText;

                determinePosition(pos, genericPanel, OFFSET_X, OFFSET_Y, ASSET_TYPE_BUILDING_ICON);
            }
        }
        else if (tipType == TOOLTIP_TYPE_TITLE_AND_DESCRIPTION)
        {
            titleAndDescriptionPanel.SetActive(true);

            if (assetType == ASSET_TYPE_BOOST_ICON)
            {
                BoostConfig boostConfig = _modelManager.boostsModel.configs[itemID] as BoostConfig;
                name = boostConfig.name;
                description = boostConfig.description;

                target = StringUtility.capitalizeFirstLetter(boostConfig.target);
                target = StringUtility.AddSpacesToSentence(target, false);

                completedText = description + " by " + StringUtility.percentToString((float)boostConfig.amount) + "\n" + "Affects: " + target;

                titleText.text = name;
                descriptionText.text = completedText;

                determinePosition(pos, titleAndDescriptionPanel, OFFSET_X, OFFSET_Y);
            }
            else if (assetType == ASSET_TYPE_PLAYER_SLOT_BOOST_ICON)
            {
                displayPlayerSlotBoostTooltip(itemID, pos);
            }
        }
    }

    public void hideTooltip()
    {
        gameObject.SetActive(false);
        genericPanel.SetActive(false);
        titleAndDescriptionPanel.SetActive(false);
    }

    private void displayPlayerSlotBoostTooltip(string itemID, Vector3 pos)
    {
        if (!JobFactory.tutorialManager.isComplete)
        {
            return;
        }

        titleAndDescriptionPanel.SetActive(true);
        Text titleText = titleAndDescriptionPanel.transform.GetChild(0).GetComponent<Text>();
        Text descriptionText = titleAndDescriptionPanel.transform.GetChild(1).GetComponent<Text>();

        string completedText = "";
        string name = "";

        List<string> boostTypesProcessed = new List<string>();
        List<string> targetTypesProcessed = new List<string>();

        BoostConfig selectedBoostConfig = _modelManager.boostsModel.getConfig(itemID) as BoostConfig;
        PlayerBoostCollectionComponent boostCollectionComp = _player.findComponent(ComponentIDEnum.PLAYER_SLOT_BOOST_COLLECTION) as PlayerBoostCollectionComponent;

        string boostAmountString = "";
        string target = "";

        //If an Ad boost, use different logic
        if (selectedBoostConfig.timeOut > 0)
        {
            target = selectedBoostConfig.target;
            boostAmountString = BoostUtility.getBoostPercentStringBasedOnConfig(selectedBoostConfig, BoostLimitsEnum.PERMANENT, boostCollectionComp, target, true);
            target = StringUtility.capitalizeFirstLetter(target);
            target = StringUtility.AddSpacesToSentence(target, false);
            completedText += target + ": " + boostAmountString + "(Temporary)";
        }
        else
        {
            for (int i = 0; i < BoostTargetsEnum.PLAYER_TARGETS.Length; i++)
            {
                target = BoostTargetsEnum.PLAYER_TARGETS[i];
                boostAmountString = BoostUtility.getBoostPercentStringBasedOnConfig(selectedBoostConfig, BoostLimitsEnum.PERMANENT, boostCollectionComp, target);

                target = StringUtility.capitalizeFirstLetter(target);
                target = StringUtility.AddSpacesToSentence(target, false);

                if (i == BoostTargetsEnum.PLAYER_TARGETS.Length - 1)
                {
                    completedText += target + ": " + boostAmountString;
                }
                else
                {
                    completedText += target + ": " + boostAmountString + "\n";
                }
            }
        }

        name = selectedBoostConfig.type;
        name = StringUtility.capitalizeFirstLetter(name);
        name = StringUtility.AddSpacesToSentence(name, false);

        titleText.text = name;
        descriptionText.text = completedText;

        determinePosition(pos, titleAndDescriptionPanel, OFFSET_X, OFFSET_Y);
    }

    private void determinePosition(Vector3 pos, GameObject panel, float offsetX, float offsetY, string assetType="")
    {
        Canvas.ForceUpdateCanvases();

        float tipWidth = panel.GetComponent<RectTransform>().rect.width * gameObject.GetComponent<Image>().canvas.scaleFactor;
        float tipHeight = panel.GetComponent<RectTransform>().rect.height * gameObject.GetComponent<Image>().canvas.scaleFactor;

        float scaleFactor = gameObject.GetComponent<Image>().canvas.scaleFactor;
        float ratioDif = ScreenUtility.getAspectRatio() / UIManager.ORIG_SCREEN_ASPECT_RATIO;

        float xPos = pos.x + offsetX * gameObject.GetComponent<Image>().canvas.scaleFactor - tipWidth;
        float yPos = pos.y + offsetY * gameObject.GetComponent<Image>().canvas.scaleFactor  + tipHeight;

        Canvas canvas = gameObject.GetComponent<Image>().canvas;

        float canvasWidth = gameObject.GetComponent<Image>().canvas.pixelRect.width;
        float canvasHeight = gameObject.GetComponent<Image>().canvas.pixelRect.height;
        
        if (xPos + tipWidth > canvasWidth)
        {
            xPos = canvasWidth - tipWidth;
        }
        else if (xPos < 10)
        {
            xPos = 10;
        }

        if (yPos - tipHeight < 10)
        {
            yPos = tipHeight + 10;
        }
        else if (yPos > canvasHeight)
        {
            yPos = canvasHeight - 10;
        }

        transform.position = new Vector3(xPos, yPos);
    }
}
