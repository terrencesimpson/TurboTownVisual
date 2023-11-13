using com.super2games.idle.enums;
using com.super2games.idle.component.possessor;
using com.super2games.idle.manager;
using com.super2games.idle.utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using com.super2games.idle.view.common;
using com.super2games.idle.ui;
using com.super2games.idle.factory;
using com.super2games.idle.config;
using com.super2games.idle.component.goods;
using com.super2games.idle.component.boosts.collection;
using com.super2games.idle.component.boosts;

namespace com.super2games.idle.ui
{
    public class PlayerLevelBoostsPanelUI
    {
        private static readonly string PLAYER_BOOST_ITEMS_CONTENT = "PlayerBoostItemsContent";

        private static readonly string WARNING_MESSAGE = "WarningMessage";

        private static readonly string ANY_JOB_TIME_REDUCTION_ICON_PATH = "player/jobTimeReduction_any";
        private static readonly string ANY_JOB_REWARD_INCREASE_ICON_PATH = "player/jobRewardUp_any";
        private static readonly string ANY_BUILDING_COST_REDUCTION_ICON_PATH = "player/costReduction_any";

        private GameObject _view;
        public GameObject view { get { return _view; } }

        private GameObject _playerBoostItemsContent;

        private Player _player;
        private ModelManager _modelManager;

        // target :: ENTRY
        private Dictionary<string, PlayerSlotEntryUI> _entries = new Dictionary<string, PlayerSlotEntryUI>();

        private PlayerBoostCollectionComponent _playerBoostCollection;

        private GameObject _warningMessage;

        public PlayerLevelBoostsPanelUI(GameObject playerLevelBoostsPanel, Player player, ModelManager modelManager)
        {
            _view = playerLevelBoostsPanel;
            _modelManager = modelManager;
            _player = player;

            _playerBoostItemsContent = _view.transform.Find(PLAYER_BOOST_ITEMS_CONTENT).gameObject;
            _warningMessage = _view.transform.Find(WARNING_MESSAGE).gameObject;
        }

        public void postInitialize()
        {
            _playerBoostCollection = _player.findComponent(ComponentIDEnum.PLAYER_SLOT_BOOST_COLLECTION) as PlayerBoostCollectionComponent;
            buildReadout();
        }

        public void buildReadout()
        {
            foreach (KeyValuePair<string, IConfig> pair in _modelManager.boostsModel.configs)
            {
                BoostConfig boostConfig = pair.Value as BoostConfig;
                createEntry(boostConfig.id);
            }
        }

        private void createEntry(string boostID)
        {
            BoostConfig boostConfig = _modelManager.boostsModel.getConfig(boostID) as BoostConfig;

            if (boostConfig == null || boostConfig.level != BoostLevelEnum.PLAYER) return;

            if (boostConfig.timeOut > 0)
            {
                ConsoleUtility.Log("");
                return;
            }

            _warningMessage.SetActive(false);

            if (!_entries.ContainsKey(boostConfig.target))
            {
                PlayerSlotEntryUI entry = new PlayerSlotEntryUI();
                _entries.Add(boostConfig.target, entry);
                entry.view.transform.SetParent(_playerBoostItemsContent.transform, false);
            }

            populateEntry(boostID); 
        }

        private void populateEntry(string boostID)
        {
            BoostConfig boostConfig = _modelManager.boostsModel.getConfig(boostID) as BoostConfig;
            string boostTarget = boostConfig.target;
            string boostType = boostConfig.type;
            string boostImgPath = boostConfig.imgPath;
            PlayerSlotEntryUI entry = _entries[boostTarget];
            string percentage = BoostUtility.getBoostPercentStringBasedOnConfig(boostConfig, BoostLimitsEnum.PERMANENT, _playerBoostCollection, boostTarget);

            string buildingType = StringUtility.AddSpacesToSentence(boostTarget, false);
            buildingType = StringUtility.capitalizeFirstLetter(buildingType);

            entry.buildingTypeText.text = buildingType;

            if (boostType == BoostTypeEnum.INCREASE_JOB_REWARDS)
            {
                entry.rewardIcon.setIcon(boostID, boostImgPath);
                entry.rewardPercentage.text = percentage;
            }
            else if (boostType == BoostTypeEnum.REDUCE_BUILD_COST)
            {
                entry.costIcon.setIcon(boostID, boostImgPath);
                entry.costPercentage.text = percentage;
            }
            else if (boostType == BoostTypeEnum.REDUCE_JOB_TIME)
            {
                entry.timeIcon.setIcon(boostID, boostImgPath);
                entry.timePercentage.text = percentage;
            }
        }

        public void removeEntry(string boostID)
        {
            if (!_entries.ContainsKey(boostID))
            {
                return; //Must have already been removed.
            }

            PlayerSlotEntryUI entry = _entries[boostID];
            entry.view.transform.SetParent(null);
            GameObject.Destroy(entry.view);
            _entries.Remove(boostID);
        }

        public void updateEntry(string boostID)
        {
            populateEntry(boostID);
        }

    }
}
