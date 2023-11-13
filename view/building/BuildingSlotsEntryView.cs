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
using com.super2games.idle.view.common;
using com.super2games.idle.factory;

namespace com.super2games.idle.view.building
{
    public class BuildingSlotsEntryView : IView, ISlotsView
    { 
        private readonly string SLOTS_ENTRY = "Prefabs/ui/SlotsEntry";

        public static readonly string NOT_USED_MATERIAL = "Materials/TempSlotNotUsed";
        public static readonly string USED_MATERIAL = "Materials/TempSlotUsed";

        private readonly string SLOT = "slot";
        public static readonly int NUM_SLOTS = 6;

        private GameObject _view;
        public GameObject view { get { return _view; } }

        private List<GameObject> _slots = new List<GameObject>();
        public List<GameObject> slots {get { return _slots; }}

        private Material _notUsedActivated;
        private Material _usedActivated;

        private Dictionary<string, Material> material = new Dictionary<string, Material>();

        private PrefabManager _prefabManager;

        public BuildingSlotsEntryView(PrefabManager prefabManager)
        {
            _prefabManager = prefabManager;

            _notUsedActivated = Resources.Load(NOT_USED_MATERIAL, typeof(Material)) as Material;
            _usedActivated = Resources.Load(USED_MATERIAL, typeof(Material)) as Material;

            material.Add(NOT_USED_MATERIAL, _notUsedActivated);
            material.Add(USED_MATERIAL, _usedActivated);
        }

        private void setupSlots()
        {
            _slots.Clear();
            for (int i = 1; i <= NUM_SLOTS; ++i)
            {
                GameObject go = _prefabManager.getUIGameObject(_view, (SLOT + i));
                go.GetComponent<Button>().onClick.RemoveAllListeners();
                _slots.Add(go);
            }
        }

        public void changeSlotMaterial(GameObject slot, string materialPath)
        {
            int slashIndex = materialPath.IndexOf("/") + 1;
            string newMaterialName = materialPath.Substring(slashIndex);
            string materialName = slot.GetComponent<Image>().material.name;
            //Debug.Log("[BuildingManagersEntryView].changeIndicatorMaterial -- materialName: " + materialName + " newMaterialName: " + newMaterialName);
            if (newMaterialName != materialName)
            {
                slot.GetComponent<Image>().material = material[materialPath];
            }
        }

        public void release()
        {
            _slots.Clear();
            _view.GetComponent<CanvasGroup>().alpha = 0;
            _view.GetComponent<CanvasGroup>().blocksRaycasts = false;
            _view = null;
        }

        public void refresh()
        {
            _view = JobFactory.uiManager.slotsEntry;
            if (!_view.activeSelf)
            {
                _view.SetActive(true);
            }
            _view.GetComponent<CanvasGroup>().alpha = 1;
            _view.GetComponent<CanvasGroup>().blocksRaycasts = true;
            setupSlots();
        }
    }
}
