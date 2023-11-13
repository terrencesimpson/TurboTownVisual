using com.super2games.idle.manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class PlayerBoostEntryUI
    {
        private readonly string BOOST_ITEM_ICON = "Prefabs/ui/BoostItemIcon";
        private readonly string BOOST_NAME_TEXT = "boostNameText";
        private readonly string BOOST_COUNT_TEXT = "boostCountText";

        private GameObject _view;
        public GameObject view { get { return _view; } }

        private GameObject _boostNameGO;
        public GameObject boostNameGO { get { return _boostNameGO; } }

        private Text _boostNameText;
        public Text boostNameText { get { return _boostNameText; } }

        private GameObject _boostCountGO;
        public GameObject boostCountGO { get { return _boostCountGO; } }

        private Text _boostCountText;
        public Text boostCountText { get { return _boostCountText; } }

        private PlayerBoostIconInteractive _interactive;
        public PlayerBoostIconInteractive interactive { get { return _interactive; } }

        public PlayerBoostEntryUI(PrefabManager prefabManager)
        {
            _view = prefabManager.getPrefab(BOOST_ITEM_ICON);
            _boostNameGO = _view.transform.Find(BOOST_NAME_TEXT).gameObject;
            _boostNameText = _boostNameGO.GetComponent<Text>();
            _boostCountGO = _view.transform.Find(BOOST_COUNT_TEXT).gameObject;
            _boostCountText = _boostCountGO.GetComponent<Text>();
            _interactive = _view.GetComponent<PlayerBoostIconInteractive>();
        }
    }
}
