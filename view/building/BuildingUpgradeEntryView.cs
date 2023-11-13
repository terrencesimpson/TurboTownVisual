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
using com.super2games.idle.factory;

namespace com.super2games.idle.view.building
{
    public class BuildingUpgradeEntryView : IView
    { 
        private readonly string UPGRADE_BUTTON = "upgradeButton";
        private readonly string UPGRADE_COST_PANEL = "upgradeCostPanel";

        private GameObject _view;
        public GameObject view { get { return _view; } }

        private Button _upgradeButton;
        public Button upgradeButton { get { return _upgradeButton; } }

        private GameObject _upgradeCostPanel;
        public GameObject upgradeCostPanel { get { return _upgradeCostPanel; } }

        private PrefabManager _prefabManager;

        public BuildingUpgradeEntryView(PrefabManager prefabManager)
        {
            _prefabManager = prefabManager;
        }

        public void release()
        {
            _view = null;
            _upgradeButton = null;
            _upgradeCostPanel = null;
        }

        public void refresh()
        {
            _view = JobFactory.uiManager.upgradeBuildingEntry;
            if (!_view.activeSelf)
            {
                _view.SetActive(true);
            }
            _upgradeButton = _prefabManager.getUIGameObject(_view, UPGRADE_BUTTON).GetComponent<Button>();
            _upgradeCostPanel = _prefabManager.getUIGameObject(_view, UPGRADE_COST_PANEL);
        }
    }
}
