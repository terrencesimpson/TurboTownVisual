using com.super2games.idle.factory;
using com.super2games.idle.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class RecordsBuildingEntry
    {
        private readonly string PLAYER_RESOURCES_ENTRY = "Prefabs/ui/RecordsBuildingEntry";

        private readonly string BUILDING_NAME = "buildingName";
        private readonly string JOB_REWARDS_PANEL = "jobRewardsPanel";
        private readonly string JOB_COSTS_PANEL = "jobCostsPanel";
        private readonly string BUILDING_ICON = "buildingIcon";
        public readonly string JOB_RESOURCE_ENTRY = "JobResourceEntry";

        public readonly string RESOURCE_NUMBER = "resourceNumber";
        public readonly string RESOURCE_ICON = "ResourceIcon";

        private readonly int NUM_OF_ENTRIES = 8;

        public GameObject view;

        public Text buildingName;
        public GameObject jobRewardsPanel;
        public GameObject jobCostsPanel;
        public BuildingIconUI buildingIcon;

        private Color _redColor = new Color(1f, .39f, 0f);

        public RecordsBuildingEntry()
        {
            view = JobFactory.prefabManager.getPrefab(PLAYER_RESOURCES_ENTRY);

            buildingName = view.transform.Find(BUILDING_NAME).gameObject.GetComponent<Text>();
            jobRewardsPanel = view.transform.Find(JOB_REWARDS_PANEL).gameObject;
            jobCostsPanel = view.transform.Find(JOB_COSTS_PANEL).gameObject;
            buildingIcon = view.transform.Find(BUILDING_ICON).gameObject.GetComponent<BuildingIconUI>();

            hideAllEntries();
        }

        public void populateEntry(GameObject panel, int index, string itemID, double resourceAmount, bool isCost)
        {
            GameObject entry = panel.transform.Find(JOB_RESOURCE_ENTRY + index).gameObject;
            Text resourceNumber = panel.transform.Find(JOB_RESOURCE_ENTRY + index + "/" + RESOURCE_NUMBER).gameObject.GetComponent<Text>();
            ResourceIconUI resourceIcon = panel.transform.Find(JOB_RESOURCE_ENTRY + index + "/" + RESOURCE_ICON).gameObject.GetComponent<ResourceIconUI>();
            string stringAmount = StringUtility.toNumString(Math.Abs(Math.Round(resourceAmount)));

            resourceNumber.text = stringAmount;
            resourceIcon.setIcon(itemID);

            entry.SetActive(true);

            if (isCost)
            {
                resourceNumber.color = _redColor;
            }
            else
            {
                resourceNumber.color = Color.green;
            }
        }

        private void hideAllEntries()
        {
            for (int i = 1; i <= NUM_OF_ENTRIES; ++i)
            {
                jobRewardsPanel.transform.Find(JOB_RESOURCE_ENTRY + i).gameObject.SetActive(false);
            }

            for (int i = 1; i <= NUM_OF_ENTRIES; ++i)
            {
                jobCostsPanel.transform.Find(JOB_RESOURCE_ENTRY + i).gameObject.SetActive(false);
            }
        }

        public void returnToPrefabManager()
        {
            JobFactory.prefabManager.returnPrefab(PLAYER_RESOURCES_ENTRY, view);
        }

    }
}
