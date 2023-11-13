using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using com.super2games.idle.utilities;
using com.super2games.idle.enums;
using com.super2games.idle.config;
using com.super2games.idle.manager;
using com.super2games.idle.goals;

namespace com.super2games.idle.ui
{

    //TODO: This class should be refactored along with the BriefcaseRevealPanelUI into common functionality.

    public class RewardClaimPanelUI : MonoBehaviour
    {
        private readonly Vector3 REWARD_ICON_POSITION = new Vector3(0, 108, 0);
        private readonly Vector3 REWARD_ICON_RESOURCES_SCALE = new Vector3(3, 3, 3);

        private readonly string COLLECT_BTN = "collectBtn";
        private readonly string REWARD_ANIM = "rewardAnim";
        private readonly string REWARD_NAME = "rewardName";
        private readonly string RANK_TITLE_NAME = "rankTitleName";

        private Button _collectBtn ;
        private Text _rewardNameText;
        private Text _rankTitleNameText;

        private GameObject _rewardPanel;
        private GameObject _currentRewardIcon;
        private GameObject _rewardAnim;

        public ItemConfig currentItemConfig;
        public BoostConfig currentBoostConfig;
        public Goal currentGoal;

        private QuestManager _questManager;
        private ItemsManager _itemsManager;

        public void initialize()
        {
            GameObject managers = GameObject.Find("Managers");
            _questManager = managers.GetComponent<QuestManager>();
            _itemsManager = managers.GetComponent<ItemsManager>();

            _rewardAnim = gameObject.transform.Find(REWARD_ANIM).gameObject;
            _rewardNameText = _rewardAnim.transform.Find(REWARD_NAME).gameObject.GetComponent<Text>();
            _collectBtn = _rewardAnim.transform.Find(COLLECT_BTN).gameObject.GetComponent<Button>();
            _rankTitleNameText = _rewardAnim.transform.Find(RANK_TITLE_NAME).gameObject.GetComponent<Text>();

            _collectBtn.onClick.AddListener(onCollectBtnClick);
        }

        public void startRewardAnim()
        {
            GameObject rewardIcon = null;

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
                iTween.ShakeScale(_rankTitleNameText.gameObject, iTween.Hash("x", 1.1f, "y", 1.1f, "z", 1.1f, "time", .66f));
                iTween.ShakeRotation(_rankTitleNameText.gameObject, iTween.Hash("z", 20f, "time", .66f));
            }

            _currentRewardIcon = rewardIcon;
            _rewardPanel = gameObject;

            _rewardPanel.SetActive(true);
            _rewardAnim.SetActive(true);
            _collectBtn.gameObject.SetActive(false);
            _rewardNameText.gameObject.SetActive(true);
            _rankTitleNameText.gameObject.SetActive(true);

            rewardIcon.transform.SetParent(_rewardAnim.transform, false);
            rewardIcon.transform.localPosition = REWARD_ICON_POSITION;
            
            _rewardNameText.gameObject.GetComponent<CanvasRenderer>().SetAlpha(0);
            _rewardNameText.gameObject.GetComponent<Text>().CrossFadeAlpha(255f, 100f, false);
            
            _rankTitleNameText.gameObject.GetComponent<CanvasRenderer>().SetAlpha(0);
            _rankTitleNameText.gameObject.GetComponent<Text>().CrossFadeAlpha(255f, 100f, false);

            if (currentItemConfig.type == ItemTypeEnum.BOOST)
            {
                _currentRewardIcon.GetComponent<BoostItemIconUI>().iconImage.CrossFadeAlpha(255f, 250f, false);
            }
            else
            {
                _currentRewardIcon.GetComponent<Image>().CrossFadeAlpha(255f, 250f, false);
            }

            _currentRewardIcon.GetComponent<CanvasRenderer>().SetAlpha(0);
            iTween.PunchPosition(_currentRewardIcon, iTween.Hash("y", -12, "time", 2));//"oncomplete", "showCollectBtn", "oncompletetarget", gameObject));
            SoundManager.instance.playSound(SoundManager.CHA_CHING);
            showCollectBtn();
        }

        private void showCollectBtn()
        {
            _collectBtn.gameObject.SetActive(true);
        }
        
        private void onCollectBtnClick()
        {
            iTween.Stop(_currentRewardIcon);

            SoundManager.instance.playSound(SoundManager.SOUND_BUTTON_CLICK);
            _currentRewardIcon.transform.parent = null;
            GameObject.Destroy(_currentRewardIcon);

            _collectBtn.gameObject.SetActive(false);

            _rewardAnim.SetActive(false);
            _rewardPanel.SetActive(false);

            if (currentBoostConfig != null)
            {
                _itemsManager.giveBoost(currentBoostConfig.id);
            }
            else
            {
                _itemsManager.giveItem(currentItemConfig.id, true, AnalyticsEconomyTransactionEnum.REWARD);
            }
        }
    }
}
