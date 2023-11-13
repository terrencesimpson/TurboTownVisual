using com.super2games.idle.manager;
using com.super2games.idle.view.common;
using com.super2games.idle.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using com.super2games.idle.factory;

namespace com.super2games.idle.view.job
{
    public class JobEntryView : IView, ISlotsView
    { 
        public static readonly string JOB_ENTRY = "Prefabs/ui/JobEntry";

        private readonly string BAR_QUICK_FILL_EFFECT = "BarQuickFillEffect";
        private readonly string ID_TEXT = "idText";
        private readonly string PROGRESS_BAR = "progressBar";
        private readonly string PROGRESS_TEXT = "progressText";
        private readonly string TIME_LEFT_TEXT = "timeLeftText";
        private readonly string JOB_ACTIVE_TOGGLE = "jobActiveToggle";
        private readonly string REWARDS_PANEL = "jobRewardsPanel";
        private readonly string COSTS_PANEL = "jobCostsPanel";
        private readonly string LOCKED_PANEL = "lockedPanel";
        private readonly string UNLOCK_POP_TEXT = "populationText";
        private readonly string UNLOCK_LVL_TEXT = "levelText";

        private readonly string SLOT = "slot";
        public static readonly int NUM_SLOTS = 7;

        public static readonly string NOT_USED_MATERIAL = "Materials/TempSlotNotUsed";
        public static readonly string USED_MATERIAL = "Materials/TempSlotUsed";

        public bool useOverClockedEffect = false;

        private GameObject _barQuickFillEffect;
        public GameObject barQuickFillEffect { get { return _barQuickFillEffect; } }
        private GameObject _idText;
        public GameObject idText { get { return _idText; } }
        private GameObject _rewardText;
        public GameObject rewardText { get { return _rewardText; } }
        private GameObject _unlockText;
        public GameObject unlockText { get { return _unlockText; } }
        private GameObject _ownedText;
        public GameObject ownedText { get { return _ownedText; } }
        private GameObject _progressBar;
        public GameObject progressBar { get { return _progressBar; } }
        private GameObject _progressText;
        public GameObject progressText { get { return _progressText; } }
        private GameObject _timeLeftText;
        public GameObject timeLeftText { get { return _timeLeftText; } }
        private GameObject _jobActiveToggle;
        public GameObject jobActiveToggle { get { return _jobActiveToggle; } }
        private GameObject _rewardsPanel;
        public GameObject rewardsPanel { get { return _rewardsPanel; } }
        private GameObject _costsPanel;
        public GameObject costsPanel { get { return _costsPanel; } }
        private GameObject _lockedPanel;
        public GameObject lockedPanel { get { return _lockedPanel; } }
        private GameObject _unlockPopText;
        public GameObject unlockPopText { get { return _unlockPopText; } }
        private GameObject _unlockLvlText;
        public GameObject unlockLvlText { get { return _unlockLvlText; } }

        private GameObject _view;
        public GameObject view { get { return _view; } }

        private List<GameObject> _slots = new List<GameObject>();
        public List<GameObject> slots { get { return _slots; } }

        private Dictionary<string, Material> material = new Dictionary<string, Material>();

        private Material _notUsedActivated;
        private Material _usedActivated;

        private PrefabManager _prefabManager;

        public bool cycleButtonsEnabled = true;

        public JobEntryView(PrefabManager prefabManager)
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
                GameObject go = _view.transform.Find(SLOT + i).gameObject;
                go.GetComponent<Button>().onClick.RemoveAllListeners();
                _slots.Add(go);
            }
        }

        public void changeSlotMaterial(GameObject indicator, string materialPath)
        {
            if (!hasMaterial(indicator, materialPath))
            {
                indicator.GetComponent<Image>().material = material[materialPath];
            }
        }

        private bool hasMaterial(GameObject indicator, string materialPath)
        {
            int slashIndex = materialPath.IndexOf("/") + 1;
            string newMaterialName = materialPath.Substring(slashIndex);
            string materialName = indicator.GetComponent<Image>().material.name;
            //Debug.Log("[JobEntryView].changeIndicatorMaterial -- materialName: " + materialName + " newMaterialName: " + newMaterialName);
            if (newMaterialName == materialName)
            {
                return true;
            }
            return false;
        }
        
        public void setCycleButtonsState(bool state)
        {
            cycleButtonsEnabled = state;
        }

        public void release()
        {
            //As noted below release() only needs to be called once but it will be called from lots of different
            //components. When it is it will only care about the first time it's called. Doing this stops multiple
            //removals and/or refreshes.
            if (_view != null)
            {
                _prefabManager.returnPrefab(JOB_ENTRY, _view, JobFactory.uiManager.getJobEntryIndex(_view)); //Indexs for Job Entries to keep them ordered.

                _view.GetComponent<CanvasGroup>().alpha = 0;
                _view.GetComponent<CanvasGroup>().blocksRaycasts = false;
                _view.GetComponent<CanvasGroup>().interactable = false;

                _view = null;
                _idText = null;
                _rewardText = null;
                _unlockText = null;
                _ownedText = null;
                _progressBar = null;
                _progressText = null;
                _timeLeftText = null;
                _jobActiveToggle = null;
                _rewardsPanel = null;
                _costsPanel = null;
                _lockedPanel = null;
                _unlockPopText = null;
                _unlockLvlText = null;
            }
        }

        public void refresh()
        {
            //refresh() will be called from multiple components as it's used in a lot of different component classes
            //with all of them calling refresh(). This only needs to be refreshed once and it's when the view is null.
            if (_view == null) 
            {
                _view = _prefabManager.getPrefab(JOB_ENTRY);
                _view.GetComponent<CanvasGroup>().alpha = 1;
                _view.GetComponent<CanvasGroup>().blocksRaycasts = true;
                _view.GetComponent<CanvasGroup>().interactable = true;

                _barQuickFillEffect = _prefabManager.getUIGameObject(_view, BAR_QUICK_FILL_EFFECT);
                _idText = _prefabManager.getUIGameObject(_view, ID_TEXT);
                _progressBar = _prefabManager.getUIGameObject(_view, PROGRESS_BAR);
                _progressText = _prefabManager.getUIGameObject(_view, PROGRESS_TEXT);
                _timeLeftText = _prefabManager.getUIGameObject(_view, TIME_LEFT_TEXT);
                _jobActiveToggle = _prefabManager.getUIGameObject(_view, JOB_ACTIVE_TOGGLE);
                _rewardsPanel = _prefabManager.getUIGameObject(_view, REWARDS_PANEL);
                _costsPanel = _prefabManager.getUIGameObject(_view, COSTS_PANEL);
                _lockedPanel = _prefabManager.getUIGameObject(_view, LOCKED_PANEL);
                _unlockPopText = _prefabManager.getUIGameObject(_lockedPanel, UNLOCK_POP_TEXT);
                _unlockLvlText = _prefabManager.getUIGameObject(_lockedPanel, UNLOCK_LVL_TEXT);
            }
        }
    }
}
