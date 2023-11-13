using com.super2games.idle.component.possessor;
using com.super2games.idle.config;
using com.super2games.idle.delegates.building;
using com.super2games.idle.enums;
using com.super2games.idle.factory;
using com.super2games.idle.manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.super2games.idle.ui
{
    public class PlayerBoostIconInteractive : MonoBehaviour
    {
        [HideInInspector]
        public BoostConfig boostConfig;

        [HideInInspector]
        public BuildingManager buildingManager;

        [HideInInspector]
        public ModelManager modelManager;

        [HideInInspector]
        public PlayerBoostListUI playerBoostListUI;

        public void onClick()
        {
            if (!JobFactory.tutorialManager.isUIFunctional(UIEnum.BUILDING_BOOST)) return;
            BuildingFunctionalityDelegate.assignBoostToBuilding(boostConfig, buildingManager.currentSelectedBuilding);
            TooltipUI.instance.hideTooltip();
            JobFactory.recordsManager.uiClick(UIEnum.BUILDING_BOOST);
            SoundManager.instance.playSound(SoundManager.SOUND_BOOST_POP);
        }
    }
}
