using com.super2games.idle.manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.super2games.idle.view.player
{
    public class PlayerResourceEntryView
    {

        private readonly string PLAYER_RESOURCES_ENTRY = "Prefabs/ui/PlayerResourcesEntry";
        private readonly string RESOURCES_TEXT = "resourcesText";
        private readonly string RESOURCES_ICON = "ResourceIcon";

        private GameObject _view;
        public GameObject view { get { return _view; } }

        private GameObject _resourcesText;
        public GameObject resourcesText { get { return _resourcesText; } }

        private GameObject _resourcesIcon;
        public GameObject resourcesIcon { get { return _resourcesIcon; } }

        public PlayerResourceEntryView(PrefabManager prefabManager)
        {
            _view = prefabManager.getPrefab(PLAYER_RESOURCES_ENTRY);
            _resourcesText = _view.transform.Find(RESOURCES_TEXT).gameObject;
            _resourcesIcon = _view.transform.Find(RESOURCES_ICON).gameObject;
        }
    }
}
