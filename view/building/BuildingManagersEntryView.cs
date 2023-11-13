using com.super2games.idle.enums;
using com.super2games.idle.component.possessor;
using com.super2games.idle.manager;
using com.super2games.idle.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.view.building
{
    public class BuildingManagersEntryView : IView
    { 
        private readonly string MANAGERS_ENTRY = "Prefabs/ManagersEntry";

        public static readonly string NOT_ACTIVATED_MATERIAL = "Materials/TempManagerNotActivated";
        public static readonly string NOT_USED_MATERIAL = "Materials/TempManagerNotUsed";
        public static readonly string USED_MATERIAL = "Materials/TempManagerUsed";

        private readonly string MANAGER_INDICATOR = "managerIndicator";
        public static readonly int NUM_INDICATORS = 11;

        private GameObject _view;
        public GameObject view { get { return _view; } }

        private List<GameObject> _indicators = new List<GameObject>();
        public List<GameObject> indicators { get { return _indicators; } }

        private Material _notActivated;
        private Material _notUsedActivated;
        private Material _usedActivated;

        private Dictionary<string, Material> material = new Dictionary<string, Material>();

        private PrefabManager _prefabManager;

        public BuildingManagersEntryView(PrefabManager prefabManager)
        {
            _prefabManager = prefabManager;

            _notActivated = Resources.Load(NOT_ACTIVATED_MATERIAL, typeof(Material)) as Material;
            _notUsedActivated = Resources.Load(NOT_USED_MATERIAL, typeof(Material)) as Material;
            _usedActivated = Resources.Load(USED_MATERIAL, typeof(Material)) as Material;

            material.Add(NOT_ACTIVATED_MATERIAL, _notActivated);
            material.Add(NOT_USED_MATERIAL, _notUsedActivated);
            material.Add(USED_MATERIAL, _usedActivated);
        }

        private void buildingIndicatorList()
        {
            for (int i = 1; i <= NUM_INDICATORS; ++i)
            {
                _indicators.Add(_view.transform.Find(MANAGER_INDICATOR + i).gameObject);
            }
        }

        public void changeIndicatorMaterial(GameObject indicator, string materialPath)
        {
            int slashIndex = materialPath.IndexOf("/") + 1;
            string newMaterialName = materialPath.Substring(slashIndex);
            string materialName = indicator.GetComponent<Image>().material.name;
            //Debug.Log("[BuildingManagersEntryView].changeIndicatorMaterial -- materialName: " + materialName + " newMaterialName: " + newMaterialName);
            if (newMaterialName != materialName)
            {
                indicator.GetComponent<Image>().material = material[materialPath];
            }
        }

        public void release()
        {
            _indicators.Clear();
            _prefabManager.returnPrefab(MANAGERS_ENTRY, _view);
            _view = null;
        }

        public void refresh()
        {
            _view = _prefabManager.getPrefab(MANAGERS_ENTRY);
            buildingIndicatorList();
        }
    }
}
