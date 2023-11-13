using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using com.super2games.idle.ui;
using com.super2games.idle.enums;

namespace com.super2games.idle.view.player
{
    public class PlayerReadoutView
    { 
        private readonly string POPULATION_TEXT = "populationPanel/text";
        private readonly string COIN_TEXT = "coinPanel/text";
        private readonly string BUCKS_TEXT = "bucksPanel/text";
        private readonly string MINERAL_TEXT = "mineralPanel/text";
        private readonly string FOOD_TEXT = "foodPanel/text";
        private readonly string WOOD_TEXT = "woodPanel/text";
        private readonly string FABRIC_TEXT = "fabricPanel/text";

        private readonly string POPULATION_ICON = "populationPanel/ResourceIcon";
        private readonly string COIN_ICON = "coinPanel/ResourceIcon";
        private readonly string BUCKS_ICON = "bucksPanel/ResourceIcon";
        private readonly string MINERAL_ICON = "mineralPanel/ResourceIcon";
        private readonly string FOOD_ICON = "foodPanel/ResourceIcon";
        private readonly string WOOD_ICON = "woodPanel/ResourceIcon";
        private readonly string FABRIC_ICON = "fabricPanel/ResourceIcon";

        private GameObject _view;

        private Text _populationText;
        public Text populationText { get { return _populationText; } }

        private Text _coinText;
        public Text coinText { get { return _coinText; } }

        private Text _bucksText;
        public Text bucksText { get { return _bucksText; } }

        private Text _mineralText;
        public Text mineralText { get { return _mineralText; } }

        private Text _foodText;
        public Text foodText { get { return _foodText; } }

        private Text _woodText;
        public Text woodText { get { return _woodText; } }

        private Text _fabricText;
        public Text fabricText { get { return _fabricText; } }

        private ResourceIconUI _populationIcon;
        private ResourceIconUI _coinIcon;
        private ResourceIconUI _bucksIcon;
        private ResourceIconUI _mineralIcon;
        private ResourceIconUI _foodIcon;
        private ResourceIconUI _woodIcon;
        private ResourceIconUI _fabricIcon;

        public PlayerReadoutView(GameObject view)
        {
            _view = view;
            _populationText = _view.transform.Find(POPULATION_TEXT).gameObject.GetComponent<Text>();
            _coinText = _view.transform.Find(COIN_TEXT).gameObject.GetComponent<Text>();
            _bucksText = _view.transform.Find(BUCKS_TEXT).gameObject.GetComponent<Text>();
            _mineralText = _view.transform.Find(MINERAL_TEXT).gameObject.GetComponent<Text>();
            _foodText = _view.transform.Find(FOOD_TEXT).gameObject.GetComponent<Text>();
            _woodText = _view.transform.Find(WOOD_TEXT).gameObject.GetComponent<Text>();
            _fabricText = _view.transform.Find(FABRIC_TEXT).gameObject.GetComponent<Text>();

            _populationIcon = _view.transform.Find(POPULATION_ICON).GetComponent<ResourceIconUI>();
            _coinIcon = _view.transform.Find(COIN_ICON).GetComponent<ResourceIconUI>();
            _bucksIcon = _view.transform.Find(BUCKS_ICON).GetComponent<ResourceIconUI>();
            _mineralIcon = _view.transform.Find(MINERAL_ICON).GetComponent<ResourceIconUI>();
            _foodIcon = _view.transform.Find(FOOD_ICON).GetComponent<ResourceIconUI>();
            _woodIcon = _view.transform.Find(WOOD_ICON).GetComponent<ResourceIconUI>();
            _fabricIcon = _view.transform.Find(FABRIC_ICON).GetComponent<ResourceIconUI>();

            _populationIcon.setIcon(ResourceEnum.POPULATION);
            _coinIcon.setIcon(ResourceEnum.COIN);
            _bucksIcon.setIcon(ResourceEnum.BUCKS);
            _mineralIcon.setIcon(ResourceEnum.MINERALS);
            _foodIcon.setIcon(ResourceEnum.FOOD);
            _woodIcon.setIcon(ResourceEnum.WOOD);
            _fabricIcon.setIcon(ResourceEnum.FABRIC);
        }
    }
}
