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
    public class BuildingPopulationEntryView : IView
    { 
        private readonly string POPULATION_TEXT = "populationText";
        private readonly string SPEED_BONUS_TEXT = "speedBonusText";
        private readonly string ADD_BUTTON = "addButton";
        private readonly string SUBTRACT_BUTTON = "subtractButton";
        private readonly string MAX_SUBTRACT_BUTTON = "maxSubtractButton";
        private readonly string MAX_ADD_BUTTON = "maxAddButton";

        private GameObject _view;
        public GameObject view { get { return _view; } }

        private GameObject _populationText;
        public GameObject populationText { get { return _populationText; } }

        private GameObject _speedBonusText;
        public GameObject speedBonusText { get { return _speedBonusText; } }

        private GameObject _addButton;
        public GameObject addButton { get { return _addButton; } }

        private GameObject _subtractButton;
        public GameObject subtractButton { get { return _subtractButton; } }

        private GameObject _maxAddButton;
        public GameObject maxAddButton { get { return _maxAddButton; } }

        private GameObject _maxSubtractButton;
        public GameObject maxSubtractButton { get { return _maxSubtractButton; } }

        private PrefabManager _prefabManager;

        public BuildingPopulationEntryView(PrefabManager prefabManager)
        {
            _prefabManager = prefabManager;
        }

        public void release()
        {
            _view.SetActive(false);
            _view = null;
            _populationText = null;
            _addButton = null;
            _subtractButton = null;
        }

        public void refresh()
        {
            _view = JobFactory.uiManager.populationEntry;

            if (!_view.activeSelf)
            {
                _view.SetActive(true);
            }

            _populationText = _prefabManager.getUIGameObject(_view, POPULATION_TEXT);
            _addButton = _prefabManager.getUIGameObject(_view, ADD_BUTTON);
            _subtractButton = _prefabManager.getUIGameObject(_view, SUBTRACT_BUTTON);
            _speedBonusText = _prefabManager.getUIGameObject(_view, SPEED_BONUS_TEXT);
            _maxSubtractButton = _prefabManager.getUIGameObject(_view, MAX_SUBTRACT_BUTTON);
            _maxAddButton = _prefabManager.getUIGameObject(_view, MAX_ADD_BUTTON);
        }
    }
}
