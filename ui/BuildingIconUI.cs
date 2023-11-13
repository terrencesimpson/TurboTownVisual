using com.super2games.idle.config;
using com.super2games.idle.manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using com.super2games.idle.factory;
using com.super2games.idle.delegates.building;

namespace com.super2games.idle.ui
{
    public class BuildingIconUI : MonoBehaviour
    {
        private readonly string BASE_PATH = "Textures/ui/icons/building/";

        public List<GameObject> resourceCostEntries = new List<GameObject>();

        private BuildingManager _buildingManager;
        private MouseCameraControl _mouseCameraControl;

        [HideInInspector]
        public BuildingsConfig buildingConfig;

        [HideInInspector]
        public string iconID = "";

        [HideInInspector]
        public bool isBGDarkened = false;

        void Start()
        {
            GameObject managers = GameObject.Find("Managers");
            _buildingManager = managers.GetComponent<BuildingManager>();
            _mouseCameraControl = Camera.main.GetComponent<MouseCameraControl>();
        }

        public void setIcon(string iconPath)
        {
            iconID = iconPath;

            Sprite sprite = Resources.Load(BASE_PATH + iconPath, typeof(Sprite)) as Sprite;
            if (sprite != null) 
            {
                GetComponent<Image>().sprite = sprite;
            }
        }

        public void darkenIcon()
        {
            GetComponent<Image>().color = Color.black;
        }

        public void lightenIcon()
        {
            GetComponent<Image>().color = Color.white;
        }

    }
}
