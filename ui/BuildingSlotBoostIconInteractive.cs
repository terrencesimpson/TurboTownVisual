using com.super2games.idle.component.boosts;
using com.super2games.idle.manager;
using com.super2games.idle.factory;
using com.super2games.idle.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using com.super2games.idle.delegates.building;
using com.super2games.idle.enums;

namespace com.super2games.idle.ui
{
    public class BuildingSlotBoostIconInteractive : MonoBehaviour
    {
        [HideInInspector]
        public BoostComponent boostComponent;

        public void onClick()
        {
            if (!JobFactory.tutorialManager.isUIFunctional(UIEnum.BUILDING_BOOST_SLOT)) return;

            JobFactory.uiManager.showBuildingFunctionPanel();
            JobFactory.uiManager.playerInventoryPanelsUI.switchToItemsTab();

            if (JobFactory.buildingManager.currentSelectedBuilding != null && !ComponentContainerTypeEnum.isPark(JobFactory.buildingManager.currentSelectedBuilding.id))
            {
                JobFactory.uiManager.buildingFunctionPanelsUI.switchToJobsTab();
            }
            else if (JobFactory.buildingManager.currentSelectedBuilding == null)
            {
                JobFactory.uiManager.buildingFunctionPanelsUI.switchToJobsTab();
            }

            if (boostComponent != null)
            {
                BuildingFunctionalityDelegate.removeBoostFromBuilding(boostComponent, JobFactory.buildingManager.currentSelectedBuilding);

                if (JobFactory.buildingManager.currentSelectedBuilding != null)
                {   //Calling it from JobFactory is yucky but I am frustrated and tired
                    JobFactory.uiManager.playerBoostListUI.sortListBasedOnBuilding(JobFactory.modelManager.buildingsModel.getConfig(JobFactory.buildingManager.currentSelectedBuilding.id) as BuildingsConfig);
                }
            }

            SoundManager.instance.playSound(SoundManager.SOUND_BOOST_POP);
            JobFactory.recordsManager.uiClick(UIEnum.BUILDING_BOOST_SLOT);
        }

    }
}
