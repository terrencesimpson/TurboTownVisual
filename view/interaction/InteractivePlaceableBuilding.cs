using com.super2games.idle.component.core;
using com.super2games.idle.enums;
using com.super2games.idle.component.interaction;
using com.super2games.idle.component.possessor;
using com.super2games.idle.component.view;
using com.super2games.idle.manager;
using com.super2games.idle.component.view.building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;
using UnityEngine;
using com.super2games.idle.config;
using com.super2games.idle.factory;
using com.super2games.idle.component.task;
using com.super2games.idle.utilities;
using com.super2games.idle.utilties;
using com.super2games.idle.delegates.building;

namespace com.super2games.idle.component.interaction.building
{
    public class InteractivePlaceableBuilding : IMouseableComponent
    {
        public bool enabled { get; set; } 

        public void onLeftMouseButtonDown()
        {
            //Debug.Log("[PlaceableBuilding] onLeftMouseButtonDown");
            JobFactory.buildingManager.buildingMouseDown = true;
            JobFactory.buildingManager.buildingDragState = true;
            BuildingPlacementDelegate.setPlacementOffset();
            MouseCameraControl.edgeOfScreenCameraMovement();

            if (!JobFactory.tutorialManager.isComplete)
            {
                JobFactory.fxManager.hideMovingHand();
            }
        }

        public void onLeftMouseButtonUp()
        {
            //Debug.Log("[PlaceableBuilding].onLeftMouseButtonUp");
            BuildingPlacementDelegate.dropBuilding();
        }

        public void onLeftMouseButtonHeldDown()
        {
            //Debug.Log("[PlaceableBuilding].onLeftMouseButtonHeldDown");
        }

        public void onRightMouseButtonDown()
        {
            //Debug.Log("[PlaceableBuilding].onRightMouseButtonDown");
        }

        public void onRightMouseButtonUp()
        {
            //Debug.Log("[PlaceableBuilding].onRightMouseButtonUp");
        }

        public void onRightMouseButtonHeldDown()
        {
            //Debug.Log("[PlaceableBuilding].onRightMouseButtonHeldDown");
        }

        public void onLeftMouseClick()
        {
            //Debug.Log("[PlaceableBuilding].onLeftMouseClick");
        }

        public void onRightMouseClick()
        {
            //Debug.Log("[PlaceableBuilding].onRightMouseClick");
        }

        public void onRightMouseExit()
        {
            //Debug.Log("[PlaceableBuilding].onRightMouseExit");
        }
    }
}
