using com.super2games.idle.config;
using com.super2games.idle.enums;
using com.super2games.idle.manager;
using com.super2games.idle.utilities;
using com.super2games.idle.view.player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace com.super2games.idle.ui
{
    public class PlayerResourcesListUI
    {
        private readonly string PLAYER_RESOURCES_ENTRY = "PlayerResourcesEntry";

        private GameObject _playerResourcesPanel;

        private ModelManager _modelManager;
        private PrefabManager _prefabManager;

        private Dictionary<string, List<PlayerResourceEntryUI>> _entries = new Dictionary<string, List<PlayerResourceEntryUI>>();

        public PlayerResourcesListUI(GameObject playerResourcesPanel, ModelManager modelManager, PrefabManager prefabManager)
        {
            _playerResourcesPanel = playerResourcesPanel;
            _modelManager = modelManager;
            _prefabManager = prefabManager;
            buildListUI();
        }

        private void buildListUI()
        {
            foreach (KeyValuePair<string, IConfig> pair in _modelManager.resourcesModel.configs)
            {
                ResourcesConfig config = pair.Value as ResourcesConfig;
                if (isCoreResource(pair.Key) || !config.isInUI)
                {
                    continue; //This is a core resource. These are displayed from the top HUD always, skip.
                }

                string group = config.group;

                if (!_entries.ContainsKey(group))
                {
                    _entries[group] = new List<PlayerResourceEntryUI>();
                }

                string path = ("Scroll View/Viewport/Content/" + config.group + "/" + PLAYER_RESOURCES_ENTRY + (_entries[group].Count + 1)); //1 indexed not 0.
                GameObject entryGO = _playerResourcesPanel.transform.Find(path).gameObject;
                PlayerResourceEntryUI entry = new PlayerResourceEntryUI(entryGO, group, pair.Key);

                _entries[group].Add(entry);
                updateEntry(pair.Key, 0);
            }
        }

        public void updateEntry(string itemID, double amount)
        {
            PlayerResourceEntryUI entry = findEntry(itemID);

            if (entry == null) return; //Not in the _entries, so geeeeeet oooooouuuttt!
            
            entry.resourcesText.GetComponent<Text>().text = StringUtility.toNumString(amount);
            entry.resourcesIcon.GetComponent<ResourceIconUI>().setIcon(itemID);
        }

        private PlayerResourceEntryUI findEntry(string itemID)
        {
            foreach (KeyValuePair<string, List<PlayerResourceEntryUI>> pair in _entries)
            {
                for (int i = 0; i < pair.Value.Count; ++i)
                {
                    if (pair.Value[i].id == itemID) return pair.Value[i];
                }
            }
            return null;
        }

        private bool isCoreResource(string itemID)
        {
            if (ResourceEnum.RESOURCES.IndexOf(itemID) != -1)
            {
                return true;
            }
            return false;
        }

    }
}
