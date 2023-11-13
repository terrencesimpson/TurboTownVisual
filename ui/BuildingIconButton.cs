using com.super2games.idle.config;
using com.super2games.idle.delegates.building;
using com.super2games.idle.factory;
using com.super2games.idle.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class BuildingIconButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler
    {
        private readonly float X_OFFSET = -120;
        private readonly float DRAG_TIME = .5f;

        [HideInInspector]
        public string iconID = "";

        [HideInInspector]
        public BuildingsConfig buildingConfig;

        private bool _pointerDown = false;
        private float _pointerDownTime = 0;

        public bool enabled = true;

        public void OnPointerEnter(PointerEventData eventData)
        {
            Vector3 pos = new Vector3(transform.position.x + X_OFFSET * gameObject.GetComponent<Image>().canvas.scaleFactor, transform.position.y, transform.position.z);
            TooltipUI.instance.displayItemIDBasedTooltip(iconID, pos, TooltipUI.TOOLTIP_TYPE_GENERIC, TooltipUI.ASSET_TYPE_BUILDING_ICON);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipUI.instance.hideTooltip();
            if (!enabled) return;
            if (_pointerDown && isDragged() && !RayCastUtility.IsPointerOverUIObject())
            {
                initBuiding(true);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!enabled) return;
            initBuiding();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!enabled) return;
            _pointerDown = true;
            _pointerDownTime = Time.realtimeSinceStartup;
        }

        private void initBuiding(bool dragBuilding = false)
        {
            BuildingPlacementDelegate.clickOnBuildUI(buildingConfig, dragBuilding);
            JobFactory.recordsManager.uiClick(iconID);
            _pointerDown = false;
            _pointerDownTime = float.MaxValue - 10; //Set lower when pointer is down
        }

        private bool isDragged()
        {
            return ((_pointerDownTime + DRAG_TIME) > Time.realtimeSinceStartup);
        }
        
    }
}
