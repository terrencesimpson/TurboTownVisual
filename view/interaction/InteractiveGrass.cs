using com.super2games.idle.delegates.building;
using com.super2games.idle.manager;
using com.super2games.idle.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace com.super2games.idle.view.interaction
{
    public class InteractiveGrass : MonoBehaviour 
    { 
        private UIManager _uiManager;
        private BuildingManager _buildingManager;
        private MouseCameraControl _mouseCameraControl;

        void Start()
        {
            GameObject managers = GameObject.Find("Managers");
            _uiManager = managers.GetComponent<UIManager>();
            _buildingManager = managers.GetComponent<BuildingManager>();
            _mouseCameraControl = Camera.main.GetComponent<MouseCameraControl>();
        }

        void OnMouseDown()
        {
            //Debug.Log("[InteractiveGrass].OnMouseDown");
            if (EventSystem.current.IsPointerOverGameObject() || RayCastUtility.IsPointerOverUIObject() || _buildingManager.buildingMovingState || _buildingManager.buildingForPurchaseState)
            {
                return;
            }

            _uiManager.hideAllPanels(false);
            _uiManager.releaseViewCollection();
        }

    }
}