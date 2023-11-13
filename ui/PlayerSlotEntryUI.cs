using com.super2games.idle.factory;
using com.super2games.idle.manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class PlayerSlotEntryUI
    {
        private readonly string BOOST_PLAYER_ICON = "Prefabs/ui/PlayerLevelBoostEntry";

        private readonly string BUILDING_TYPE_TEXT = "BuildingTypeText";

        private readonly string TIME_ICON = "TimeIcon";
        private readonly string TIME_PRECENTAGE = "TimePercentage";

        private readonly string REWARD_ICON = "RewardIcon";
        private readonly string REWARD_PRECENTAGE = "RewardPercentage";

        private readonly string COST_ICON = "CostIcon";
        private readonly string COST_PRECENTAGE = "CostPercentage";

        public GameObject view;

        public Text buildingTypeText;

        public PlayerSlotIconUI timeIcon;
        public Text timePercentage;

        public PlayerSlotIconUI rewardIcon;
        public Text rewardPercentage;

        public PlayerSlotIconUI costIcon;
        public Text costPercentage;

        public PlayerSlotEntryUI()
        {
            view = JobFactory.prefabManager.getPrefab(BOOST_PLAYER_ICON);

            buildingTypeText = view.transform.Find(BUILDING_TYPE_TEXT).gameObject.GetComponent<Text>();

            timeIcon = view.transform.Find(TIME_ICON).gameObject.GetComponent<PlayerSlotIconUI>();
            timePercentage = view.transform.Find(TIME_PRECENTAGE).gameObject.GetComponent<Text>();

            rewardIcon = view.transform.Find(REWARD_ICON).gameObject.GetComponent<PlayerSlotIconUI>();
            rewardPercentage = view.transform.Find(REWARD_PRECENTAGE).gameObject.GetComponent<Text>();

            costIcon = view.transform.Find(COST_ICON).gameObject.GetComponent<PlayerSlotIconUI>();
            costPercentage = view.transform.Find(COST_PRECENTAGE).gameObject.GetComponent<Text>();
        }
    }
}
