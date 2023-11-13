using com.super2games.idle.factory;
using com.super2games.idle.manager;
using com.super2games.idle.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class ResourceEntryUI
    {
        private readonly string PLAYER_RESOURCES_ENTRY = "Prefabs/ui/PlayerResourcesEntry";

        private readonly string RESOURCE_ICON = "ResourceIcon";
        private readonly string RESOURCE_TEXT = "resourcesText";

        private GameObject _view;
        public GameObject view { get { return _view; } }

        private ResourceIconUI _resourceIcon;
        private Text _resourcesText;

        private Color _redColor = new Color(1f,.39f,0f);

        public ResourceEntryUI(PrefabManager prefabManager, string resourceID, double amount, bool isSpacer = false, bool showColor = false)
        {
            _view = prefabManager.getPrefab(PLAYER_RESOURCES_ENTRY);
            _resourceIcon = _view.transform.Find(RESOURCE_ICON).gameObject.GetComponent<ResourceIconUI>();
            _resourcesText = _view.transform.Find(RESOURCE_TEXT).gameObject.GetComponent<Text>();

            if (isSpacer)
            {
                _resourceIcon.setIcon("", "", 0, false, false, true);
                _resourcesText.text = "";
                return;
            }
            else
            {
                _resourceIcon.setIcon(resourceID);
            }

            string stringAmount = StringUtility.toNumString(Math.Abs(Math.Round(amount))); //If negative, we still want the correct formatting. Adding a "-" below. 

            _resourcesText.text = stringAmount; //If Spacer, we return out above

            if (amount < 0 && showColor)
            {
                _resourcesText.color = _redColor;//Color.red
            }
            else if (amount > 1 && showColor)
            {
                _resourcesText.color = Color.green;
            }
        }

        public void returnToPrefabManager()
        {
            GameObject.Destroy(_view);
        }

    }
}
