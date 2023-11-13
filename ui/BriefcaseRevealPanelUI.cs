using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;
using System.Collections;
using com.super2games.idle.utilities;
using com.super2games.idle.enums;
using com.super2games.idle.config;
using com.super2games.idle.manager;
using com.super2games.idle.factory;

namespace com.super2games.idle.ui
{
    public class BriefcaseRevealPanelUI : MonoBehaviour
    {
        private readonly string BRONZE_CLOSED_TEXTURE = "Textures/ui/briefcase_bronze_closed";
        private readonly string BRONZE_OPEN_TEXTURE = "Textures/ui/briefcase_bronze_open";
        private readonly string SILVER_CLOSED_TEXTURE = "Textures/ui/briefcase_silver_closed";
        private readonly string SILVER_OPEN_TEXTURE = "Textures/ui/briefcase_silver_open";
        private readonly string GOLD_CLOSED_TEXTURE = "Textures/ui/briefcase_gold_closed";
        private readonly string GOLD_OPEN_TEXTURE = "Textures/ui/briefcase_gold_open";

        private readonly string CHEST_OPEN_ANIM = "openChestAnim";
        private readonly string CHEST_CLOSED_ANIM = "closedChestAnim";
        private readonly string FLASH = "chestFlash";
        private readonly string OBTAINED_ICONS_PANEL = "obtainedIconsPanel";
        private readonly string REWARD_NAME = "rewardName";
        private readonly string COLLECT_BTN = "collectBtn";
        private readonly string RANK_TITLE_NAME = "rankTitleName";

        private readonly float CHEST_SHAKE_MAGNITUDE = 12.0f;
        private readonly int CHEST_NUM_SHAKES = 3;

        private readonly Vector3 REWARD_ICON_POSITION = new Vector3(0, 108, 0);
        private readonly Vector3 REWARD_ICON_RESOURCES_SCALE = new Vector3(3, 3, 3);

        public delegate void OnCompleteDeletgate(string chestType);
        public event OnCompleteDeletgate onComplete;

        private int currentChestShake = 0;

        private GameObject _currentRewardIcon;
        private int _currentIconNum = 0;
        private int _rewardIconTotal = 0;
        private List<GameObject> _rewardIconList = new List<GameObject>();

        private string _chestType = "bronze";

        private GameObject _briefcasePanel;
        private GameObject _openChestAnim;
        private GameObject _closedChestAnim;
        private GameObject _flash;
        private GameObject _obtainedIconsPanel;
        private Text _rewardNameText;
        private Text _rankTitleNameText;
        private Button _collectBtn;

        public ChestConfig currentChestConfig;
        public List<ItemConfig> currentItemConfigs;
        public List<BoostConfig> currentBoostConfigs;
        private int _currentConfigIndex = 0;

        public ItemsManager itemsManager;

        public bool isFree = false; //Should only be done on single items.

        private void setChestTexture(string state="closed", string type = "bronze")
        {
            //Sprite sprite = Resources.Load(SILVER_CLOSED_TEXTURE, typeof(Sprite)) as Sprite;
            Sprite sprite;
            string texturePath = "";

            if (state == "closed")
            {
                if (type == ChestTypeEnum.BRONZE)
                {
                    texturePath = BRONZE_CLOSED_TEXTURE;
                }
                if (type == ChestTypeEnum.SILVER)
                {
                    texturePath = SILVER_CLOSED_TEXTURE;
                }
                if (type == ChestTypeEnum.GOLD)
                {
                    texturePath = GOLD_CLOSED_TEXTURE;
                }

                sprite = Resources.Load(texturePath, typeof(Sprite)) as Sprite;
                ConsoleUtility.Log("texture path: " + texturePath);
                _closedChestAnim.GetComponent<Image>().sprite = sprite;
            }
            else if (state == "open")
            {
                if (type == ChestTypeEnum.BRONZE)
                {
                    texturePath = BRONZE_OPEN_TEXTURE;
                }
                if (type == ChestTypeEnum.SILVER)
                {
                    texturePath = SILVER_OPEN_TEXTURE;
                }
                if (type == ChestTypeEnum.GOLD)
                {
                    texturePath = GOLD_OPEN_TEXTURE;
                }

                sprite = Resources.Load(texturePath, typeof(Sprite)) as Sprite;
                ConsoleUtility.Log("texture path: " + texturePath);
                _openChestAnim.GetComponent<Image>().sprite = sprite;
            }
        }

        public void startChestAnim(int totalNumRewards = 10, string type = "bronze")
        {
            _briefcasePanel = gameObject;
            _closedChestAnim = _briefcasePanel.transform.Find(CHEST_CLOSED_ANIM).gameObject;
            _closedChestAnim.SetActive(true);
            currentChestShake = 0;

            _currentConfigIndex = 0;

            _currentIconNum = 0;
            _rewardIconTotal = totalNumRewards;
            _chestType = type;

            setChestTexture("closed", _chestType);

            shakeChest();
        }

        public void shakeChest()
        {
            currentChestShake++;
            SoundManager.instance.playSound(SoundManager.CASE_POUND);
            iTween.ShakePosition(_closedChestAnim, iTween.Hash("x", (CHEST_SHAKE_MAGNITUDE/2 * gameObject.GetComponent<Image>().canvas.scaleFactor) * currentChestShake, "y", (CHEST_SHAKE_MAGNITUDE / 2 * gameObject.GetComponent<Image>().canvas.scaleFactor) * currentChestShake, "easeType", "linear", "delay", 0.2f, "time", .33f, "oncomplete", "onShakeEnd", "oncompletetarget", gameObject));
        }

        private void onShakeEnd()
        {
            if (currentChestShake >= CHEST_NUM_SHAKES)
            {
                showFlash();
            }
            else
            {
                shakeChest();
            }
        }

        public void showFlash()
        {
            _briefcasePanel = gameObject;
            _flash = _briefcasePanel.transform.Find(FLASH).gameObject;
            _flash.SetActive(true);
            SoundManager.instance.playSound(SoundManager.CHA_CHING);
            iTween.ScaleTo(_flash, iTween.Hash("x", 1.1, "oncomplete", "showOpenChest", "oncompletetarget", gameObject, "time", 0.1f));
            _flash.GetComponent<CanvasRenderer>().SetAlpha(0);
            _flash.GetComponent<Image>().CrossFadeAlpha(255f, 25f, false);
        }

        public void showOpenChest()
        {
            _briefcasePanel = gameObject;
            _openChestAnim = _briefcasePanel.transform.Find(CHEST_OPEN_ANIM).gameObject;
            _obtainedIconsPanel = _briefcasePanel.transform.Find(OBTAINED_ICONS_PANEL).gameObject;
            _collectBtn = _openChestAnim.transform.Find(COLLECT_BTN).gameObject.GetComponent<Button>();
            _rewardNameText = _openChestAnim.transform.Find(REWARD_NAME).gameObject.GetComponent<Text>();
            _rankTitleNameText = _openChestAnim.transform.Find(RANK_TITLE_NAME).gameObject.GetComponent<Text>();

            _flash.SetActive(false);
            _closedChestAnim.SetActive(false);
            _openChestAnim.SetActive(true);
            _collectBtn.gameObject.SetActive(false);

            setChestTexture("open", _chestType);

            createRewardIcon();
        }

        private void createRewardIcon()
        {
            if (_currentIconNum >= _rewardIconTotal)
            {
                _collectBtn.gameObject.SetActive(true);
                _collectBtn.onClick.AddListener(onCollectBtnClick);
                return;
            }

            GameObject rewardIcon = null;
            ItemConfig currentItemConfig = currentItemConfigs[_currentConfigIndex];

            if (currentItemConfig.type == ItemTypeEnum.RESOURCE)
            {
                rewardIcon = GameObjectUtility.instantiateGameObject(PrefabsEnum.RESOURCE_ICON_PREFAB);
                rewardIcon.transform.localScale = REWARD_ICON_RESOURCES_SCALE;
                rewardIcon.GetComponent<ResourceIconUI>().setIcon(currentItemConfig.itemID);
                _rewardNameText.text = currentItemConfig.itemID + " x" + currentItemConfig.amount;

                _rankTitleNameText.gameObject.SetActive(false);
            }
            else if (currentItemConfig.type == ItemTypeEnum.BOOST)
            {
                BoostConfig currentBoostConfig = currentBoostConfigs[_currentConfigIndex];
                rewardIcon = GameObjectUtility.instantiateGameObject(PrefabsEnum.BOOST_ITEM_ICON_DISPLAY_PREFAB);

                if (currentBoostConfig.level == BoostLevelEnum.PLAYER)
                {
                    rewardIcon.GetComponent<BoostItemIconUI>().setIcon(currentBoostConfig.id, currentBoostConfig.imgPath, currentBoostConfig.rank, true);
                }
                else
                {
                    rewardIcon.GetComponent<BoostItemIconUI>().setIcon(currentBoostConfig.id, currentBoostConfig.imgPath, currentBoostConfig.rank);
                }

                _rewardNameText.text = currentBoostConfig.name + " " + StringUtility.percentToString((float)currentBoostConfig.amount);

                _rankTitleNameText.gameObject.SetActive(true);
                _rankTitleNameText.text = StringUtility.determineRankTitle(currentBoostConfig.rank);
                _rankTitleNameText.gameObject.GetComponent<CanvasRenderer>().SetAlpha(0);
                _rankTitleNameText.CrossFadeAlpha(255f, 250f, false);
                iTween.ShakeScale(_rankTitleNameText.gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "z", 1.1f,"time", .66f));
                iTween.ShakeRotation(_rankTitleNameText.gameObject, iTween.Hash("z", 20f, "time", .66f));
            }

            rewardIcon.transform.SetParent(_openChestAnim.transform, false);
            rewardIcon.transform.localPosition = REWARD_ICON_POSITION;

            _rewardIconList.Add(rewardIcon);

            _rewardNameText.gameObject.SetActive(true);

            _currentRewardIcon = rewardIcon;
            _currentIconNum++;

            iTween.PunchPosition(_currentRewardIcon, iTween.Hash("y", -15, "time", 1, "oncomplete", "moveCurrentIcon", "oncompletetarget", gameObject));

            _currentRewardIcon.GetComponent<CanvasRenderer>().SetAlpha(0);

            if (currentItemConfig.type == ItemTypeEnum.BOOST)
            {
                _currentRewardIcon.GetComponent<BoostItemIconUI>().iconImage.CrossFadeAlpha(255f, 250f, false);
            }
            else
            {
                _currentRewardIcon.GetComponent<Image>().CrossFadeAlpha(255f, 250f, false);
            }

            _rewardNameText.gameObject.GetComponent<CanvasRenderer>().SetAlpha(0);
            _rewardNameText.CrossFadeAlpha(255f, 250f, false);

            _currentConfigIndex++;
        }

        private void moveCurrentIcon()
        {
            Transform slot = _obtainedIconsPanel.transform.GetChild(_currentIconNum-1);
            if (_rewardIconTotal > 1)
            {
                iTween.MoveTo(_currentRewardIcon, iTween.Hash("x", slot.position.x, "y", slot.position.y, "time", .35f, "easeType", "easeOutQuad", "oncomplete", "createRewardIcon", "oncompletetarget", gameObject));
            }
            else
            {
                iTween.MoveBy(_currentRewardIcon, iTween.Hash("y", 1, "time", .35f, "easeType", "easeOutQuad", "oncomplete", "createRewardIcon", "oncompletetarget", gameObject));
            }
            _rewardNameText.gameObject.GetComponent<CanvasRenderer>().SetAlpha(0);
            _rankTitleNameText.gameObject.GetComponent<CanvasRenderer>().SetAlpha(0);
        }

        private void onCollectBtnClick()
        {
            //iTween.Stop(_currentRewardIcon);
            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            for (int i=0;i< _rewardIconList.Count;i++)
            {
                GameObject icon = _rewardIconList[i];
                icon.transform.parent = null;
                GameObject.Destroy(icon);
            }

            _rewardIconList.Clear();
            _currentRewardIcon = null;

            isFree = false; //This will be set elsewhere, but want to set to false for safety after use.

            _collectBtn.gameObject.SetActive(false);
            _collectBtn.onClick.RemoveAllListeners();
            _openChestAnim.SetActive(false);
            _briefcasePanel.SetActive(false);

            JobFactory.recordsManager.uiClick(UIEnum.CASE_OPEN);

            if (onComplete != null)
            {
                onComplete(currentChestConfig.id);
            }
        }
    }
}
